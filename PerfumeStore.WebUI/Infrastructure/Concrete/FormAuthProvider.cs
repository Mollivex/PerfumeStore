using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using PerfumeStore.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace PerfumeStore.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        [Obsolete]
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}