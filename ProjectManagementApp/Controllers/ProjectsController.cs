using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{projectId}/upload-documents")]
        public async Task<IActionResult> UploadDocuments(int projectId, List<IFormFile> files)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound("Project not found");
            }

            if (files == null || files.Count == 0)
            {
                return BadRequest("No files provided for upload");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedDocuments");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                var filePath = Path.Combine(uploadPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { Status = "Success", Message = "Files uploaded successfully" });
        }


        [HttpPost("{projectId}/add-employee/{employeeId}")]
        public async Task<IActionResult> AddEmployeeToProject(int projectId, int employeeId)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectEmployees)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null) return NotFound("Project not found");

            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return NotFound("Employee not found");

            if (project.ProjectEmployees.Any(pe => pe.EmployeeId == employeeId))
                return BadRequest("Employee is already assigned to this project");

            var projectEmployee = new ProjectEmployee
            {
                ProjectId = projectId,
                EmployeeId = employeeId
            };

            project.ProjectEmployees.Add(projectEmployee);
            await _context.SaveChangesAsync();

            return Ok("Employee added to project");
        }

        [HttpDelete("{projectId}/remove-employee/{employeeId}")]
        public async Task<IActionResult> RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            var projectEmployee = await _context.ProjectEmployees
                .FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);

            if (projectEmployee == null) return NotFound("Employee not found in this project");

            _context.ProjectEmployees.Remove(projectEmployee);
            await _context.SaveChangesAsync();

            return Ok("Employee removed from project");
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredProjects(
            [FromQuery] DateTime? startDateFrom,
            [FromQuery] DateTime? startDateTo,
            [FromQuery] int? priority,
            [FromQuery] string? clientCompany,
            [FromQuery] string? orderBy)
        {
            var query = _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.ProjectEmployees)
                .ThenInclude(pe => pe.Employee)
                .AsQueryable();

            // Фильтрация по диапазону дат
            if (startDateFrom.HasValue)
            {
                query = query.Where(p => p.StartDate >= startDateFrom.Value);
            }

            if (startDateTo.HasValue)
            {
                query = query.Where(p => p.StartDate <= startDateTo.Value);
            }

            // Фильтрация по приоритету
            if (priority.HasValue)
            {
                query = query.Where(p => p.Priority == priority.Value);
            }

            // Фильтрация по компании-заказчику
            if (!string.IsNullOrWhiteSpace(clientCompany))
            {
                query = query.Where(p => p.ClientCompany.Contains(clientCompany));
            }

            // Сортировка
            query = orderBy?.ToLower() switch
            {
                "name" => query.OrderBy(p => p.Name),
                "startdate" => query.OrderBy(p => p.StartDate),
                "priority" => query.OrderBy(p => p.Priority),
                _ => query.OrderBy(p => p.Id)
            };

            var projects = await query.ToListAsync();
            return Ok(projects);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.ProjectEmployees)
                .ThenInclude(pe => pe.Employee)
                .ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectManager)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                ClientCompany = project.ClientCompany,
                ContractorCompany = project.ContractorCompany,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority
            };

            return Ok(projectDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project updatedProject)
        {
            if (id != updatedProject.Id) return BadRequest();

            _context.Entry(updatedProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(p => p.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
