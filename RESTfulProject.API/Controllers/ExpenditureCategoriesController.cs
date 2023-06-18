using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulProject.API.ViewModels;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using RESTfulProject.Service.Services.ExpenditureCategoryService;

namespace RESTfulProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenditureCategoriesController : ControllerBase
    {
        private readonly IExpenditureCategoryService _expenditureCategoryService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private LinkGenerator _linkGenerator;

        public ExpenditureCategoriesController(IExpenditureCategoryService expenditureCategoryService, IAuthService authService, IMapper mapper, LinkGenerator linkGenerator)
        {
            _expenditureCategoryService = expenditureCategoryService;
            _authService = authService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExpenditureCategoryViewModel>> GetExpenditureCategories()
        {
            var results = _expenditureCategoryService.GetExpenditureCategories();
            return _mapper.Map<List<ExpenditureCategoryViewModel>>(results);
        }

        [HttpGet("UserExpenditureCategories")]
        public ActionResult<IEnumerable<ExpenditureCategoryViewModel>> GetUserExpenditureCategories(string email)
        {
            if (!_authService.UserExists(email))
            {
                return BadRequest("User does not exist");
            }
            var results = _expenditureCategoryService.GetUserExpenditureCategories(email);
            return _mapper.Map<List<ExpenditureCategoryViewModel>>(results);
        }

        [HttpGet("{id}")]
        public ActionResult<ExpenditureCategoryViewModel> GetExpenditureCategory(int id)
        {
            var expenditureCategory = _expenditureCategoryService.GetExpenditureCategoryById(id);

            if (expenditureCategory == null)
            {
                return NotFound();
            }

            var expenditureCategoryReturn = _mapper.Map<ExpenditureCategoryViewModel>(expenditureCategory);
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetExpenditureCategory), values: new { id }),
                    "self",
                    "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteExpenditureCategory), values: new { id }),
                    "delete_expenditure_category",
                    "DELETE"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(PutExpenditureCategory), values: new { id }),
                    "update_expenditure_category",
                    "PUT")
            };
            expenditureCategoryReturn.Links = links;
            return Ok(expenditureCategoryReturn);
        }

        [HttpPut("{id}")]
        public IActionResult PutExpenditureCategory(int id, ExpenditureCategoryViewModel expenditureCategory)
        {
            if (id != expenditureCategory.ExpenditureCategoryId)
            {
                return BadRequest("Wrong expenditure category id.");
            }
            try
            {
                _expenditureCategoryService.UpdateExpenditureCategory(_mapper.Map<ExpenditureCategoryDto>(expenditureCategory));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_expenditureCategoryService.ExpenditureCategoryExists(id))
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
        public ActionResult<ExpenditureCategoryViewModel> PostExpenditureCategory(ExpenditureCategoryViewModel expenditureCategory, string email)
        {
            _expenditureCategoryService.AddExpenditureCategory(email, _mapper.Map<ExpenditureCategoryDto>(expenditureCategory));

            return CreatedAtAction("GetExpenditureCategory", new { id = expenditureCategory.ExpenditureCategoryId }, expenditureCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExpenditureCategory(int id)
        {
            var expenditureCategory = _expenditureCategoryService.GetExpenditureCategoryById(id);
            if (expenditureCategory == null)
            {
                return NotFound();
            }
            _expenditureCategoryService.RemoveExpenditureCategory(id);

            return NoContent();
        }
    }
}
