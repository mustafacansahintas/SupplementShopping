
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class _User:BaseEntities
    {
        [Required, StringLength(20)]

        public string Name { get; set; }
        [Required, StringLength(20)]
        public string Surname { get; set; }
        [Required, StringLength(20)]
        public string UserName { get; set; }

        [Required,StringLength(20)]
        public string Password { get; set; }

        [Required, StringLength(40)]
        public string Email { get; set; }
      
        public String Gender { get; set; }

        public bool isActive { get; set; }

        [Required]
        public bool isAdmin { get; set; }

        [Required]
        public Guid ActivateGuid { get; set; }

        public virtual List<Like> like { get; set; }

    }

}
