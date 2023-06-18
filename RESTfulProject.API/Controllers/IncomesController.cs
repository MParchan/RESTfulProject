using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RESTfulProject.API.ViewModels;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using RESTfulProject.Service.Services.IncomeService;

namespace RESTfulProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private LinkGenerator _linkGenerator;

        public IncomesController(IIncomeService incomeService, IAuthService authService, IMapper mapper, LinkGenerator linkGenerator)
        {
            _incomeService = incomeService;
            _authService = authService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<IncomeViewModel>> GetIncomes()
        {
            var results = _incomeService.GetIncomes();
            return _mapper.Map<List<IncomeViewModel>>(results);
        }

        [HttpGet("UserIncomes")]
        public ActionResult<IEnumerable<IncomeViewModel>> GetUserIncomes(string email)
        {
            if (!_authService.UserExists(email))
            {
                return BadRequest("User does not exist");
            }
            var results = _incomeService.GetUserIncomes(email);
            return _mapper.Map<List<IncomeViewModel>>(results);
        }

        [HttpGet("{id}")]
        public ActionResult<IncomeViewModel> GetIncome(int id)
        {
            var income = _incomeService.GetIncomeById(id);

            if (income == null)
            {
                return NotFound();
            }
            var incomeReturn = _mapper.Map<IncomeViewModel>(income);
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetIncome), values: new { id }),
                    "self",
                    "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteIncome), values: new { id }),
                    "delete_income",
                    "DELETE"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(PutIncome), values: new { id }),
                    "update_income",
                    "PUT")
            };
            incomeReturn.Links = links;
            return Ok(incomeReturn);
        }

        [HttpPut("{id}")]
        public IActionResult PutIncome(int id, IncomeViewModel income)
        {
            if (id != income.IncomeId)
            {
                return BadRequest("Wrong income id.");
            }
            try
            {
                _incomeService.UpdateIncome(_mapper.Map<IncomeDto>(income));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_incomeService.IncomeExists(id))
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
        public ActionResult<IncomeViewModel> PostIncome(string email, IncomeViewModel income)
        {
            Console.WriteLine(income.IncomeCategoryId);
            Console.WriteLine(income.Comment);
            _incomeService.AddIncome(email, _mapper.Map<IncomeDto>(income));

            return CreatedAtAction("GetIncome", new { id = income.IncomeId }, income);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIncome(int id)
        {
            var income = _incomeService.GetIncomeById(id);
            if (income == null)
            {
                return NotFound();
            }
            _incomeService.RemoveIncome(id);

            return NoContent();
        }
    }
}
