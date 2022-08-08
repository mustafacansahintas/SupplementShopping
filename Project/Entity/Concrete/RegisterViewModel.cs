using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{

    public class RegisterViewModel
    {
        [DisplayName("İsim"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Name { get; set; }

        [DisplayName("Soyisim"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} max {1} karakter olmalı.")]
        public string Username { get; set; }

        [DisplayName("Email Adresi"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(70, ErrorMessage = "{0} max {1} karakter olmalı."),
            EmailAddress(ErrorMessage = "{0} alanı için geçerli bir email adresi giriniz.")]
        public string Email { get; set; }


        [DisplayName("Cinsiyet"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Gender { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} max {1} karakter olmalı.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),
           Required(ErrorMessage = "{0} alanı boş geçilemez."),
           DataType(DataType.Password),
           StringLength(25, ErrorMessage = "{0} max {1} karakter olmalı."),
            Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor.")]
        public string RePassword { get; set; }
    }

  









}
