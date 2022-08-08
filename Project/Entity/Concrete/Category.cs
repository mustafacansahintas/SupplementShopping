
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category:BaseEntities
    {
        [Required]
        public string CategoryName { get; set; }


        public List<Product> product { get; set; }
        


    }
}
