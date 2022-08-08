using BLL.Abstract;
using BLL.Result;
using Common.Helper;
using Entities.Concrete;
using Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserManager:ManagerBase<_User>
    {

        public BusinessLayerResult<_User> RegisterUser(RegisterViewModel data)
        {

            _User user = Find(x => x.UserName == data.Username || x.Email == data.Email);

            BusinessLayerResult<_User> res = new BusinessLayerResult<_User>();

            if (user != null)
            {
                if (user.UserName == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "Email adresi kayıtlı.");
                }
            }

            else
            {
                int dbResult = base.Insert(new _User()
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    UserName = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    ActivateGuid = Guid.NewGuid(),
                    isActive = false,
                    isAdmin = false,

                });

                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Email == data.Email && x.UserName == data.Username);

                    string siteUrl = ConfigHelper.Get<string>("SiteRootUrl");
                    string activeUrl = $"{siteUrl}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.UserName};<br><br>Hesabınızı aktifleştirmek için <a href='{activeUrl}' target='_blank'> tıklayınız </a>";
                    MailHelper.SendMail(body, res.Result.Email, "NutriShop Hesap Aktifleştirme");
                }
            }
            return res;
        }



        public BusinessLayerResult<_User> LoginUser(LoginViewModel data)
        {
           
            BusinessLayerResult<_User> res = new BusinessLayerResult<_User>();
            res.Result = Find(e=> e.Email == data.Email && e.Password==data.Password );

            if (res.Result != null)
            {
                if (!res.Result.isActive) 
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen email adresinizi kontrol ediniz.");
                }
            }

            else
            {
                res.AddError(ErrorMessageCode.EmailOrPassWrong, "Email adresi veya şifre uyuşmuyor.");
            }

            return res;
        }

        public BusinessLayerResult<_User> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<_User> res = new BusinessLayerResult<_User>();
            res.Result = Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.isActive)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }

                res.Result.isActive = true;
                Update(res.Result);

            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }
            return res;
        }

    }
}
