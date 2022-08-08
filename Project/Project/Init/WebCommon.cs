using Common;
using Entities.Concrete;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Init
{
    public class WebCommon:Icommon
    {
        public string GetUsername()
        {
            _User user = CurrentSession.user;
            if (user != null)
            {
                return user.UserName;
            }

            return "system";
        }
    }
}