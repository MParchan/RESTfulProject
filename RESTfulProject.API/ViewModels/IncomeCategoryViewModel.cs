namespace RESTfulProject.API.ViewModels
{
    public class IncomeCategoryViewModel
    {
        public int IncomeCategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        public List<Link> Links { get; set; }
    }
}
