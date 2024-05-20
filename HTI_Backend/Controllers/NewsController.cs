﻿using Azure.Storage.Blobs;
using HTI.Core.Entities;
using HTI.Repository.Data;
using HTI_Backend.Controllers;
using HTI_Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Sas;

public class NewsController : ApiBaseController
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly StoreContext _dbContext;

    private readonly IConfiguration _configuration;
    public NewsController(BlobServiceClient blobServiceClient, StoreContext dbContext, IConfiguration configuration)
    {
        _blobServiceClient = blobServiceClient;
        _dbContext = dbContext;
        _configuration = configuration;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateNewsItem([FromForm] NewsItemCreateModel model)
    {
        // ... (Validation and error handling)

        var newsItem = new NewsItem
        {
            Title = model.Title,
            Description = model.Description,
            CoverPhotoUrl = model.CoverPhoto != null ? await UploadCoverPhotoAsync(model.CoverPhoto) : await UploadDefaultCoverPhotoAsync()
        };

        if (model.Files != null && model.Files.Count > 0)
        {
            foreach (var file in model.Files)
            {
                var fileUrl = await UploadBlobAsync(file, "news");
                newsItem.Files.Add(new NewsItemFile
                {
                    OriginalFileName = file.FileName,
                    BlobName = Path.GetFileName(fileUrl),  // Extract blob name from URL
                    ContentType = file.ContentType,
                    SasUrl = fileUrl
                });
            }
        }

        _dbContext.NewsItems.Add(newsItem);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNewsItem), new { id = newsItem.Id }, newsItem);
    }

    private async Task<string> UploadCoverPhotoAsync(IFormFile coverPhoto)
    {
        return await UploadBlobAsync(coverPhoto, "cover-photos");
    }

    private async Task<string> UploadDefaultCoverPhotoAsync()
    {
        var defaultCoverPhotoPath = @"D:\News_photo.png";
        var blobName = "default-cover-photo.png";
        var containerClient = _blobServiceClient.GetBlobContainerClient("cover-photos"); // Use "cover-photos" container

        // Check if the default image exists and only upload if it doesn't
        var blobClient = containerClient.GetBlobClient(blobName);

        if (!await blobClient.ExistsAsync())
        {
            await blobClient.UploadAsync(defaultCoverPhotoPath);
        }
        return await GenerateSasUrlAsync(blobClient);
    }

    private async Task<string> UploadBlobAsync(IFormFile file, string containerName)
    {
        var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var blobClient = _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);

        await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });

        return await GenerateSasUrlAsync(blobClient);
    }

    // SAS URL Generation Method
    private async Task<string> GenerateSasUrlAsync(BlobClient blobClient)
    {
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = blobClient.BlobContainerName,
            BlobName = blobClient.Name,
            Resource = "b",  // Resource type: blob
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMonths(6) // SAS valid for 24 hours
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
        return sasUri.ToString();
    }




    [HttpGet("{id}")]
    public async Task<ActionResult<NewsItem>> GetNewsItem(int id)
    {
        var newsItem = await _dbContext.NewsItems
            .Include(ni => ni.Files)
            .FirstOrDefaultAsync(ni => ni.Id == id);

        if (newsItem == null)
            return NotFound();

        return newsItem; // Files already have SAS URLs
    }

    [HttpGet]
    public async Task<ActionResult<List<NewsItem>>> GetAllNewsItems()
    {
        var newsItems = await _dbContext.NewsItems
            .Include(ni => ni.Files)
            .ToListAsync();

        var newsItemDtos = newsItems.Select(ni => new NewsItem
        {
            Id = ni.Id,
            Title = ni.Title,
            Description = ni.Description,
            CoverPhotoUrl = ni.CoverPhotoUrl,
            Files = ni.Files.Select(f => new NewsItemFile
            {
                Id = f.Id,
                OriginalFileName = f.OriginalFileName,
                SasUrl = f.SasUrl
            }).ToList()
        }).ToList();

        return Ok(newsItemDtos);
    }



    [HttpGet("{id}/files/{fileId}")]
    public async Task<IActionResult> GetNewsItemFile(int id, int fileId)
    {
        var newsItem = await _dbContext.NewsItems
            .Include(ni => ni.Files)
            .FirstOrDefaultAsync(ni => ni.Id == id);

        if (newsItem == null)
            return NotFound();

        var file = newsItem.Files.FirstOrDefault(f => f.Id == fileId);
        if (file == null)
            return NotFound();

        var blobClient = _blobServiceClient
            .GetBlobContainerClient(_configuration["AzureStorage:ContainerName"])
            .GetBlobClient(file.BlobName);

        // Ensure the blob exists
        if (!await blobClient.ExistsAsync())
            return NotFound();

        // Download blob content into a memory stream
        using var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        memoryStream.Position = 0; // Reset stream position

        // Construct a FileContentResult with the correct content type
        return new FileContentResult(memoryStream.ToArray(), file.ContentType)
        {
            FileDownloadName = file.OriginalFileName
        };
    }

}
