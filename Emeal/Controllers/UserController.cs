using Emeal.Common;
using Emeal.Model.ApiModels;
using Emeal.Model.ViewModel;
using Emeal.Model.ViewModels;
using Emeal.Models;
using Emeal.OrderApi;
using Emeal.Security;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;

namespace Emeal.Controllers
{
    public class UserController : SurfaceController
    {
        public UserController()
        {

        }
        // GET: User
        public ActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            registrationViewModel.CompanyId = 1; //ToDo = get companyID from Umbraco Setup.
            var apiUrl = "https://localhost:44324";
            var method = "/Access/registration";
            var dataModel = new RegistrationDto()
            {
                CompanyId = registrationViewModel.CompanyId,
                Email = registrationViewModel.Email,
                Name = registrationViewModel.Name,
                Password = registrationViewModel.Password
            };

            var apiClient = new ApiClient(apiUrl);
            var response = apiClient.Post<ApiResult, RegistrationDto>(method, dataModel);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                TempData["ErrorMessage"] = Constants.USER_EXISTS;
            }
            return CurrentUmbracoPage();
        }

        public ActionResult RegistrationForm()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            loginViewModel.CompanyId = 1; //ToDo = get companyID from Umbraco Setup.
            var apiUrl = "https://localhost:44324";
            var method = "/Access/login";
            var apiClient = new ApiClient(apiUrl);
            var response = apiClient.Post<LoggedUser, LoginViewModel>(method, loginViewModel);

            if (response != null)
            {
                AuthenticateTicket.AddAuthenticationTicket(response);
            }

            return null;
        }

        public ActionResult LoginForm()
        {
            return PartialView();
        }
        [Authorize]
        public ActionResult Test()
        {
            var user = AuthenticateTicket.GetCurrentUser();
            return Json(user,JsonRequestBehavior.AllowGet);
        }
    }
}