using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.DTOs
{
    public class IncomeDto
    {
        public int IncomeId { get; set; }
        public int IncomeCategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public virtual IncomeCategoryDto IncomeCategory { get; set; }
    }
}
