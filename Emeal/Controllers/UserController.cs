using Emeal.Common;
using Emeal.Model.ViewModel;
using Emeal.Model.ViewModels;
using Newtonsoft.Json;
using RestSharp;
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
            var apiUrl = "https://localhost:44324";
            var method = "/Access/registration";
            var client = new RestClient(apiUrl);
            var request = new RestRequest(method, Method.POST);
            var dataModel = new RegistrationDto()
            {
                CompanyId = registrationViewModel.CompanyId,
                Email = registrationViewModel.Email,
                Name = registrationViewModel.Name,
                Password = registrationViewModel.Password
            };
            var data = JsonConvert.SerializeObject(dataModel);
            request.AddParameter("application/json; charset=utf-8", data, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try
            {
                client.ExecuteAsync(request, response =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // OK
                        var accesscode = response.Content;
                    }
                    else
                    {
                        var result = "Not Allowed";
                        TempData["ErrorMessage"] = Constants.USER_EXISTS;
                    }
                });
            }
            catch (Exception error)
            {
                // Log
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
            var apiUrl = "https://localhost:44324";
            var method = "/Access/login";
            var client = new RestClient(apiUrl);
            var request = new RestRequest(method, Method.POST);
            var data = JsonConvert.SerializeObject(loginViewModel);
            request.AddParameter("application/json; charset=utf-8", data, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // OK
                    var accesscode = response.Content;
                    FormsAuthentication.SetAuthCookie(loginViewModel.UserName, true);
                }
                else
                {
                    var loginError = "LoginError";
                }


                //    client.ExecuteAsync(request, response =>
                //    {
                //        if (response.StatusCode == HttpStatusCode.OK)
                //        {
                //            // OK
                //            var accesscode = response.Content;
                //            FormsAuthentication.SetAuthCookie(loginViewModel.Email, true);
                //        }
                //        else
                //        {
                //            var loginError = "LoginError";
                //        }
                //    });
            }
            catch (Exception error)
            {
                // Log
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
            var user = User.Identity;
            return null;
        }
    }
}