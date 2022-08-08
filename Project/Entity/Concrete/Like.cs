using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Like:BaseEntities
    {
        public bool likes { get; set; }


        public virtual Product product { get; set; }
        public virtual _User LikedUser { get; set; }
    }
}
