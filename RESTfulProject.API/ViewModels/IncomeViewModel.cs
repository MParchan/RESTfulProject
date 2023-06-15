namespace RESTfulProject.API.ViewModels
{
    public class IncomeViewModel
    {
        public int IncomeId { get; set; }
        public int IncomeCategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public virtual IncomeCategoryViewModel IncomeCategory { get; set; }
    }
}
