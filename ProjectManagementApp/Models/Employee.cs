using System.Collections.Generic;

namespace ProjectManagementApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }

        // Связь "многие ко многим"
        public List<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }
}
