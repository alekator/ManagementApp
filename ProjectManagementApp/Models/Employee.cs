using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProjectManagementApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Связь многие-ко-многим с проектами
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
