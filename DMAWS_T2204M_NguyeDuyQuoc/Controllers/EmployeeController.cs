using AutoMapper;
using DMAWS_T2204M_NguyeDuyQuoc.DTOs;
using DMAWS_T2204M_NguyeDuyQuoc.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2204M_NguyeDuyQuoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // Lay danh sach employee
        [HttpGet]
        [Route("get_all_employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Index()
        {
            var employees = await _context.Employees.ToListAsync();


            if (employees == null)
            {
                return NotFound();
            }

            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);

            return Ok(employeeDTOs);
        }

        // tim theo id
        [HttpGet]
        [Route("get_by_id")]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {
            var employee = await _context.Employees
                .Include(b => b.ProjectEmployees)
                    .ThenInclude(c => c.Project)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            //Map
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDTO);
        }

        // tim theo ten
        [HttpGet]
        [Route("get_by_name")] 
        public async Task<ActionResult<EmployeeDTO>> GetByName(string name)
        {
            var employee = await _context.Employees
                .Include(b => b.ProjectEmployees)
                    .ThenInclude(c => c.Project)
                .Where(f => f.EmployeeName.Contains(name))
                .ToListAsync();

            if (employee == null)
            {
                return NotFound();
            }

            //Map
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDTO);
        }


        // tao moi
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<EmployeeDTO>> Create(EmployeeDTO data)
        {
            if (ModelState.IsValid)
            {
                //Check if Employee with the same name already exists
                if (_context.Employees.Any(c => c.EmployeeName == data.EmployeeName))
                {
                    return BadRequest("A Employee with the same name already exists.");
                }
                //Map
                var employee = _mapper.Map<Employee>(data);

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();


                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId }, employeeDTO);
            }
            return BadRequest();
        }

        // Xoa 
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // update
        [HttpPut]
        public IActionResult Update(EmployeeDTO data)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(data);
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return NoContent(); ;
            }
            return BadRequest();
        }
    }
}
