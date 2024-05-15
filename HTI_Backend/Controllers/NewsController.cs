using Azure.Storage.Blobs;
using HTI.Core.Entities;
using HTI.Repository.Data;
using HTI_Backend.Controllers;
using HTI_Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs.Models;


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

        var newsItem = new NewsItem { Title = model.Title, Description = model.Description };
        if (model.Files != null && model.Files.Count > 0)
        {
            foreach (var file in model.Files)
            {
                var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var blobClient = _blobServiceClient.GetBlobContainerClient("news").GetBlobClient(blobName);

                // Use your pre-generated SAS URL
                var sasUrl = "https://htinews.blob.core.windows.net/news?sv=2023-01-03&si=news-18F742B79C2&sr=c&sig=O5%2FKBdBjqLVaCs9hrMQSA2WOGQzWH8ZTSX4aLfuMtBE%3D";

                await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });

                newsItem.Files.Add(new NewsItemFile
                {
                    OriginalFileName = file.FileName,
                    BlobName = blobName,
                    ContentType = file.ContentType,
                    SasUrl = sasUrl // Store the complete SAS URL
                });
            }
        }

        _dbContext.NewsItems.Add(newsItem);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNewsItem), new { id = newsItem.Id }, newsItem);
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

        // Map to DTOs to avoid circular references
        var newsItemDtos = newsItems.Select(ni => new NewsItem
        {
            Id = ni.Id,
            Title = ni.Title,
            Description = ni.Description,
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
