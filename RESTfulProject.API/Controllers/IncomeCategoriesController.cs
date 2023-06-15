using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulProject.API.ViewModels;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using RESTfulProject.Service.Services.IncomeCategoryService;

namespace RESTfulProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeCategoriesController : Controller
    {
        private readonly IIncomeCategoryService _incomeCategoryService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public IncomeCategoriesController(IIncomeCategoryService incomeCategoryService, IAuthService authService, IMapper mapper)
        {
            _incomeCategoryService = incomeCategoryService;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<IncomeCategoryViewModel>> GetIncomeCategories()
        {
            var results = _incomeCategoryService.GetIncomeCategories();
            return _mapper.Map<List<IncomeCategoryViewModel>>(results);
        }

        [HttpGet("UserIncomeCategories")]
        public ActionResult<IEnumerable<IncomeCategoryViewModel>> GetUserIncomeCategories(string email)
        {
            if (!_authService.UserExists(email))
            {
                return BadRequest("User does not exist");
            }
            var results = _incomeCategoryService.GetUserIncomeCategories(email);
            return _mapper.Map<List<IncomeCategoryViewModel>>(results);
        }

        [HttpGet("{id}")]
        public ActionResult<IncomeCategoryViewModel> GetIncomeCategory(int id)
        {
            var incomeCategory = _incomeCategoryService.GetIncomeCategoryById(id);

            if (incomeCategory == null)
            {
                return NotFound();
            }

            return _mapper.Map<IncomeCategoryViewModel>(incomeCategory);
        }

        [HttpPut("{id}")]
        public IActionResult PutIncomeCategory(int id, IncomeCategoryViewModel incomeCategory)
        {
            if (id != incomeCategory.IncomeCategoryId)
            {
                return BadRequest("Wrong income category id.");
            }
            try
            {
                _incomeCategoryService.UpdateIncomeCategory(_mapper.Map<IncomeCategoryDto>(incomeCategory));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_incomeCategoryService.IncomeCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public ActionResult<IncomeCategoryViewModel> PostIncomeCategory(string email, IncomeCategoryViewModel incomeCategory)
        {
            _incomeCategoryService.AddIncomeCategory(email, _mapper.Map<IncomeCategoryDto>(incomeCategory));

            return CreatedAtAction("GetIncomeCategory", new { id = incomeCategory.IncomeCategoryId }, incomeCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIncomeCategory(int id)
        {
            var incomeCategory = _incomeCategoryService.GetIncomeCategoryById(id);
            if (incomeCategory == null)
            {
                return NotFound();
            }
            _incomeCategoryService.RemoveIncomeCategory(id);

            return NoContent();
        }
    }
}
