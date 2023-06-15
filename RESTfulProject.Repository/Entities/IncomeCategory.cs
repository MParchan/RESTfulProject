using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Entities
{
    public class IncomeCategory
    {
        [Key]
        public int IncomeCategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
