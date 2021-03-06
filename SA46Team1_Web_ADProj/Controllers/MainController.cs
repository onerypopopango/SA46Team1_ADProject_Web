﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SA46Team1_Web_ADProj.DAL;
using SA46Team1_Web_ADProj.Models;

//-----------------------------------------------------------------
//   Author: Chirag Shetty
//-----------------------------------------------------------------

namespace SA46Team1_Web_ADProj.Controllers
{    
    public class MainController : Controller
    {
        IEmployeeRepository emplRepo;
        BearerTokenModel token;
        
        public MainController()
        {
            emplRepo = new EmployeeRepositoryImpl(new SSISdbEntities());           
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Login(UserModel user)
        {
            if (new AppUserManager(new UserStore<Employee>(new SSISdbEntities())).IsValid(user.Username, user.Password))
            {
                //Auth success
                Employee employee = emplRepo.FindEmployeeEmailId(user.Username);
                var identity = new ClaimsIdentity(
                new[] {                   
                    new Claim(ClaimTypes.NameIdentifier, employee.EmployeeEmail),
                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                    new Claim(ClaimTypes.Name,employee.EmployeeName),
                      new Claim(ClaimTypes.Role, employee.Designation),
                      },
                DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(
                new AuthenticationProperties { IsPersistent = true }, identity);
                using (HttpClient client = new HttpClient())
                {
                    var tokenRequest = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>("grant_type", "password"),
                                new KeyValuePair<string, string>("username", user.Username),
                                new KeyValuePair<string, string>("password", user.Password)
                            };
                    HttpContent encodedRequest = new FormUrlEncodedContent(tokenRequest);
                    HttpResponseMessage response = client.PostAsync("http://localhost/lu/token", encodedRequest).Result;
                    token = response.Content.ReadAsAsync<BearerTokenModel>().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Session["access-token"] = token.AccessToken;
                    }
                }

                Session["LoginEmployeeID"] = employee.EmployeeID;
                Session["UserId"] = employee.EmployeeID;
                Session["Role"] = employee.Designation;
                Session["DepartmentCode"] = employee.DepartmentCode;
                Session["EmpName"] = employee.EmployeeName;

                if (employee.Designation == "Department Head"
                    || employee.Designation == "Employee"
                    || employee.Designation == "Employee Representative")
                {
                    if(employee.Approver == 1)
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Approver"));
                    return RedirectToAction("Home", "Dept", new { area = "" });
                }
                else if (employee.Designation == "Store Clerk"
                    || employee.Designation == "Store Supervisor"
                    || employee.Designation == "Store Manager")
                {
                    return RedirectToAction("Home", "Store", new { area = "" });
                }
                else
                {
                    return View("Login");
                }
            }
            else
            {
                //auth failed
                ViewBag.IsValid = false;
                return View("Login");
            }
        }

        [CustomAuthorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Abandon(); 
            return RedirectToAction("Login", "Main", new { area = "" });
        }
    }
}
