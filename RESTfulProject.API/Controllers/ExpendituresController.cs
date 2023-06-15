using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulProject.API.ViewModels;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using RESTfulProject.Service.Services.ExpenditureService;

namespace RESTfulProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpendituresController : ControllerBase
    {
        private readonly IExpenditureService _expenditureService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public ExpendituresController(IExpenditureService expenditureService, IAuthService authService, IMapper mapper)
        {
            _expenditureService = expenditureService;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExpenditureViewModel>> GetExpenditures()
        {
            var results = _expenditureService.GetExpenditures();
            return _mapper.Map<List<ExpenditureViewModel>>(results);
        }

        [HttpGet("UserExpenditures")]
        public ActionResult<IEnumerable<ExpenditureViewModel>> GetUserExpenditures(string email)
        {
            if (!_authService.UserExists(email))
            {
                return BadRequest("User does not exist");
            }
            var results = _expenditureService.GetUserExpenditures(email);
            return _mapper.Map<List<ExpenditureViewModel>>(results);
        }

        [HttpGet("{id}")]
        public ActionResult<ExpenditureViewModel> GetExpenditure(int id)
        {
            var expenditure = _expenditureService.GetExpenditureById(id);

            if (expenditure == null)
            {
                return NotFound();
            }

            return _mapper.Map<ExpenditureViewModel>(expenditure);
        }

        [HttpPut("{id}")]
        public IActionResult PutExpenditure(int id, ExpenditureViewModel expenditure)
        {
            if (id != expenditure.ExpenditureId)
            {
                return BadRequest("Wrong expenditure id.");
            }
            try
            {
                _expenditureService.UpdateExpenditure(_mapper.Map<ExpenditureDto>(expenditure));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_expenditureService.ExpenditureExists(id))
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
        public ActionResult<ExpenditureViewModel> PostExpenditure(string email, ExpenditureViewModel expenditure)
        {
            _expenditureService.AddExpenditure(email, _mapper.Map<ExpenditureDto>(expenditure));

            return CreatedAtAction("GetExpenditure", new { id = expenditure.ExpenditureId }, expenditure);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExpenditure(int id)
        {
            var expenditure = _expenditureService.GetExpenditureById(id);
            if (expenditure == null)
            {
                return NotFound();
            }
            _expenditureService.RemoveExpenditure(id);

            return NoContent();
        }
    }
}
