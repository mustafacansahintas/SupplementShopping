
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Brand:BaseEntities
    {
        [Required,StringLength(20)]
        public string BrandName { get; set; }

    
        public virtual List<Product> product { get; set; }
    }
}
