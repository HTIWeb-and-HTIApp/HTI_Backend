using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HTI.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace HTI.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext, UserManager<IdentityUser> userManager)
        {

            if (_dbcontext.Departments.Count() == 0)
            {
                var departmentsData = File.ReadAllText("../HTI.Repository/Data/DataSeed/departments.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());

                var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData, options);

                if (departments?.Count > 0)
                {
                    foreach (var department in departments)
                    {

                        await _dbcontext.Set<Department>().AddAsync(department);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }


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

                    foreach (var course in courses)
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

            if (_dbcontext.Doctors.Count() == 0)
            {
                var doctorsData = File.ReadAllText("../HTI.Repository/Data/DataSeed/doctors.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());

                var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorsData, options);

                if (doctors?.Count > 0)
                {
                    foreach (var doctor in doctors)
                    {

                        await _dbcontext.Set<Doctor>().AddAsync(doctor);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.TeachingAssistants.Count() == 0)
            {
                var t_AData = File.ReadAllText("../HTI.Repository/Data/DataSeed/TA.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());

                var t_a = JsonSerializer.Deserialize<List<TeachingAssistant>>(t_AData, options);

                if (t_a?.Count > 0)
                {
                    foreach (var doctor in t_a)
                    {

                        await _dbcontext.Set<TeachingAssistant>().AddAsync(doctor);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.Groups.Count() == 0)
            {
                var groupssData = File.ReadAllText("../HTI.Repository/Data/DataSeed/groups.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());
                options.Converters.Add(new NullableIntConverter());
                options.Converters.Add(new BooleanConverter());

                var groups = JsonSerializer.Deserialize<List<Core.Entities.Group>>(groupssData, options);

                if (groups?.Count > 0)
                {
                    foreach (var group in groups)
                    {

                        await _dbcontext.Set<Core.Entities.Group>().AddAsync(group);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.Registrations.Count() == 0)
            {
                var registrationsData = File.ReadAllText("../HTI.Repository/Data/DataSeed/registrations.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());
                options.Converters.Add(new NullableIntConverter());
                options.Converters.Add(new BooleanConverter());

                var registrations = JsonSerializer.Deserialize<List<Registration>>(registrationsData, options);

                if (registrations?.Count > 0)
                {
                    foreach (var registration in registrations)
                    {

                        await _dbcontext.Set<Registration>().AddAsync(registration);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (_dbcontext.StudentCourseHistories.Count() == 0)
            {
                var StudentCourseHistoriesData = File.ReadAllText("../HTI.Repository/Data/DataSeed/student_course_histories.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringToDateConverter());
                options.Converters.Add(new NullableIntConverter());
                options.Converters.Add(new BooleanConverter());
                options.Converters.Add(new NullConverter());


                var studentCourseHistories = JsonSerializer.Deserialize<List<StudentCourseHistory>>(StudentCourseHistoriesData, options);

                if (studentCourseHistories?.Count > 0)
                {
                    foreach (var studentCourseHistory in studentCourseHistories)
                    {

                        await _dbcontext.Set<StudentCourseHistory>().AddAsync(studentCourseHistory);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }


            if (_dbcontext.Users.Count() == 0)
            {
                var userData = File.ReadAllText("../HTI.Repository/Data/DataSeed/roles_data.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var users = JsonSerializer.Deserialize<List<UserSeedData>>(userData, options);

                if (users?.Count > 0)
                {
                    foreach (var user in users)
                    {
                        var email = user.Email;
                        var username = email.Split('@')[0];
                        var phoneNumber = user.PhoneNumber;
                        var password = user.Password;
                        var role = user.Role;

                        var newUser = new IdentityUser { Email = email, UserName = username, PhoneNumber = phoneNumber };

                        var passwordHasher = new PasswordHasher<IdentityUser>();
                        var hashedPassword = passwordHasher.HashPassword(newUser, password);
                        newUser.PasswordHash = hashedPassword;

                        await userManager.CreateAsync(newUser);
                        await userManager.AddToRoleAsync(newUser, role);
                    }
                    await _dbcontext.SaveChangesAsync();
                }

            }
        }
        public class UserSeedData
        {
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
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
        return DateTime.Parse(dateString);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss"));
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
public class NullConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return 0;
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrEmpty(stringValue))
            {
                return 0;
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
            writer.WriteNullValue();
        }
    }
}





