using System;
using System.Collections.Generic;

namespace ProjectManagementApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientCompany { get; set; }
        public string ContractorCompany { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public int ProjectManagerId { get; set; }
        public Employee ProjectManager { get; set; }

        // Связь "многие ко многим"
        public List<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }
}
