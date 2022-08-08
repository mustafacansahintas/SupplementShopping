using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OrderViewModel
    {
        [DisplayName("Adres Başlığı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Adresbasligi { get; set; }
        [DisplayName("Adres"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Adres { get; set; }
        [DisplayName("Şehir"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Sehir { get; set; }
        [DisplayName("Semt"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Semt { get; set; }
        [DisplayName("Mahalle"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Mahalle { get; set; }

    }
}
