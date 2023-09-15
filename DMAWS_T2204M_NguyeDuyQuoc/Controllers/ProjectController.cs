using AutoMapper;
using DMAWS_T2204M_NguyeDuyQuoc.DTOs;
using DMAWS_T2204M_NguyeDuyQuoc.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2204M_NguyeDuyQuoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProjectController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // Lay danh sach project
        [HttpGet]
        [Route("get_all_projects")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> Index()
        {
            var projects = await _context.Projects.ToListAsync();


            if (projects == null)
            {
                return NotFound();
            }

            var projectDTOs = _mapper.Map<List<ProjectDTO>>(projects);

            return Ok(projectDTOs);
        }

        // tim theo id
        [HttpGet]
        [Route("get_by_id")]
        public async Task<ActionResult<ProjectDTO>> Get(int id)
        {
            var project = await _context.Projects
                .Include(b => b.ProjectEmployees)
                    .ThenInclude(c => c.Employee)
                .FirstOrDefaultAsync(e => e.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            //Map
            var projectDTO = _mapper.Map<ProjectDTO>(project);

            return Ok(projectDTO);
        }

        // tim theo ten
        [HttpGet]
        [Route("get_by_name")]
        public async Task<ActionResult<ProjectDTO>> GetByName(string name)
        {
            var project = await _context.Projects
                .Include(b => b.ProjectEmployees)
                    .ThenInclude(c => c.Employee)
                .Where(f => f.ProjectName.Contains(name))
                .ToListAsync();

            if (project == null)
            {
                return NotFound();
            }

            //Map
            var projectDTO = _mapper.Map<ProjectDTO>(project);

            return Ok(projectDTO);
        }

        // tao moi
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ProjectDTO>> Create(ProjectDTO data)
        {
            if (ModelState.IsValid)
            {
                //Check if Project with the same name already exists
                if (_context.Projects.Any(c => c.ProjectName == data.ProjectName))
                {
                    return BadRequest("A Project with the same name already exists.");
                }
                //Map
                var project = _mapper.Map<Project>(data);

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                
                var projectDTO = _mapper.Map<ProjectDTO>(project);
                return CreatedAtAction(nameof(GetByName), new { id = project.ProjectId }, projectDTO);
            }
            return BadRequest();
        }

        // Xoa 
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // update
        [HttpPut]
        public IActionResult Update(ProjectDTO data)
        {
            if (ModelState.IsValid)
            {
                var project = _mapper.Map<Project>(data);
                _context.Projects.Update(project);
                _context.SaveChanges();
                return NoContent(); ;
            }
            return BadRequest();
        }
    }
}
