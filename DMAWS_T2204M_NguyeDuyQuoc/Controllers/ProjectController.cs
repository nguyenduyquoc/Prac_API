using AutoMapper;
using DMAWS_T2204M_NguyeDuyQuoc.DTOs;
using DMAWS_T2204M_NguyeDuyQuoc.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("get_all_brand")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> Index()
        {
            var projects = await _context.Project
                .ToListAsync();


            if (brands == null || brands.Count == 0)
            {
                return NotFound();
            }

            var brandDTOs = _mapper.Map<List<BrandDTO>>(brands);

            return Ok(brandDTOs);
        }
    }
}
