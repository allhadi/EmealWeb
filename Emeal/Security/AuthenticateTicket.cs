using Emeal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Emeal.Security
{
    public class AuthenticateTicket
    {
        public static void AddAuthenticationTicket(LoggedUser user)
        {
            var userData = JsonConvert.SerializeObject(user);
            FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1,user.Email,DateTime.Now,DateTime.Now.AddDays(1),true,userData);
            var encTicket = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            httpCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }

        public static LoggedUser GetCurrentUser()
        {
            var user = HttpContext.Current.User;
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }
            FormsIdentity formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
            var userData = formsIdentity.Ticket.UserData;
            return JsonConvert.DeserializeObject<LoggedUser>(userData);
        }

        internal static void AddAuthenticationTicket(object userData)
        {
            throw new NotImplementedException();
        }
    }
}