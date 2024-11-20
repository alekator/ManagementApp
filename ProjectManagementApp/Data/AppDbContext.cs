using Bogus; // Установите через NuGet: dotnet add package Bogus
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Отключаем предупреждение о несоответствии модели и миграций
        //    optionsBuilder.ConfigureWarnings(warnings =>
        //        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи "многие ко многим"
            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);

            // Заполнение данных для сотрудников
            var faker = new Bogus.Faker("en");
            var employees = new List<Employee>();
            for (int i = 1; i <= 50; i++)
            {
                employees.Add(new Employee
                {
                    Id = i,
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    MiddleName = faker.Name.FirstName(),
                    Email = faker.Internet.Email()
                });
            }

            // Заполнение данных для проектов
            var projects = new List<Project>();
            for (int i = 1; i <= 20; i++)
            {
                projects.Add(new Project
                {
                    Id = i,
                    Name = $"Project {i}",
                    ClientCompany = faker.Company.CompanyName(),
                    ContractorCompany = faker.Company.CompanyName(),
                    StartDate = faker.Date.Past(2),
                    EndDate = faker.Date.Future(1),
                    Priority = faker.Random.Int(1, 5),
                    ProjectManagerId = faker.Random.Int(1, 50)
                });
            }

            // Заполнение данных для связей между проектами и сотрудниками
            var projectEmployees = new List<ProjectEmployee>();
            var rnd = new Random();
            foreach (var project in projects)
            {
                var employeeIds = Enumerable.Range(1, 50).OrderBy(_ => rnd.Next()).Take(5).ToList();
                foreach (var employeeId in employeeIds)
                {
                    projectEmployees.Add(new ProjectEmployee
                    {
                        ProjectId = project.Id,
                        EmployeeId = employeeId
                    });
                }
            }

            // Добавляем данные в контекст
            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<ProjectEmployee>().HasData(projectEmployees.Select((pe, index) => new ProjectEmployee
            {
                ProjectId = pe.ProjectId,
                EmployeeId = pe.EmployeeId
            }));
        }

    }
}
