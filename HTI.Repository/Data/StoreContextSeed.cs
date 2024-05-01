using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HTI.Core.Entities;

namespace HTI.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            if (_dbcontext.Students.Count() == 0)
            {
                var studentsData = File.ReadAllText("../HTI.Repository/Data/DataSeed/students.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());

                var students = JsonSerializer.Deserialize<List<Student>>(studentsData, options);

                if (students?.Count > 0)
                {
                    foreach (var student in students)
                    {
                        await _dbcontext.Set<Student>().AddAsync(student);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
        }
    }

    public class JsonStringToDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var dateString = reader.GetString();
            return DateTime.ParseExact(dateString, "M/d/yyyy", null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("M/d/yyyy"));
        }
    }
}
