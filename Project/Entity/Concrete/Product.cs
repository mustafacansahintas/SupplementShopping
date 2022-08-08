
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product:BaseEntities
    {
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int LikeCount { get; set; }

        public int SalesAmount { get; set; }

        [Required]
        public int Stock { get; set; }

        public string ImageAdress { get; set; }

        public string ImageDetailAdress { get; set; }

        public int Discounted { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category category { get; set; }
        public virtual Brand brand { get; set; }
        public virtual List<Like> like { get; set; }


    }

   

   
}
