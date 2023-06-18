using Microsoft.EntityFrameworkCore;

namespace RESTfulProject.API.ViewModels
{
    public class ExpenditureViewModel
    {
        public int ExpenditureId { get; set; }
        public int ExpenditureCategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public virtual ExpenditureCategoryViewModel ExpenditureCategory { get; set; }

        public List<Link> Links { get; set; }
    }
}
