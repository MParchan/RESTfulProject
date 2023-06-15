using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.DTOs
{
    public class ExpenditureCategoryDto
    {
        public int ExpenditureCategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
