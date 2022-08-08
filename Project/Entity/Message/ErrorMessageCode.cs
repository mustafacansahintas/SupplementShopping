using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Message
{
    public enum ErrorMessageCode
    {
        UsernameAlreadyExists=101,
        EmailAlreadyExists=102,
        UserIsNotActive=151,
        EmailOrPassWrong=152,
        CheckYourEmail = 153,
        UsernameAlreadyActive = 154,
        ActivateIdDoesNotExists = 155,
        UserNotFound = 156,
        ProfileCouldNotUpdated = 157,
        UserCouldNotRemove = 158,
        UserCouldNotFind = 159,
        UserCouldNotInserted = 160
    }
}
