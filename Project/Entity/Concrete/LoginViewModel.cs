using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LoginViewModel
    {
        [DisplayName("Email Adresi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
