using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.DTOs
{
    public class ExpenditureDto
    {
        public int ExpenditureId { get; set; }
        public int ExpenditureCategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public virtual ExpenditureCategoryDto ExpenditureCategory { get; set; }
    }
}
