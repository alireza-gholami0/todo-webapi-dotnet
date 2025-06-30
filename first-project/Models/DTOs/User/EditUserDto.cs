using System.Text.Json.Serialization;

namespace first_project.Models.DTOs.User
{
    public class EditUserDto
    {
        public required string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required UserRole Role { get; set; }
    }
}
