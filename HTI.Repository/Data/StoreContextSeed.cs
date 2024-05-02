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


            if (_dbcontext.Courses.Count() == 0)
            {
                var coursesData = File.ReadAllText("../HTI.Repository/Data/DataSeed/courses.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());
                options.Converters.Add(new NullableIntConverter());
                options.Converters.Add(new BooleanConverter());


                var courses = JsonSerializer.Deserialize<List<Course>>(coursesData, options);







                if (courses?.Count > 0)
                {
                    var sortedCourses = courses.OrderBy(c => c.PrerequisiteId).ToList();

                    foreach (var course in sortedCourses)
                    {
                        if (course.PrerequisiteId == 0 || string.IsNullOrEmpty(course.PrerequisiteId.ToString())) // or some other default value
                        {
                            course.PrerequisiteId = null; // set to null when there is no prerequisite
                        }

                        await _dbcontext.Set<Course>().AddAsync(course);
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


    public class BooleanConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32() != 0;
            }
            else
            {
                return reader.GetBoolean();
            }
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }
    public class NullableIntConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (string.IsNullOrEmpty(stringValue))
                {
                    return null; // Return null when the JSON value is an empty string
                }
                else
                {
                    return int.Parse(stringValue);
                }
            }
            else
            {
                return reader.GetInt32();
            }
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteStringValue(string.Empty);
            }
        }
    }
}
