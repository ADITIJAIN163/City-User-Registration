using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using MiniProject.Models;
using MiniProject.ViewModel;
using System.Security.Claims;

namespace MiniProject.Controllers
{
    public class RegisteredUserController : Controller
    {
        // GET: RegisteredUserController

        private IMemoryCache cache;
        
        public RegisteredUserController(IMemoryCache memoryCache)
        {   
            cache = memoryCache;
            List<SelectListItem> lst = ViewRegisteredUser.DisplayAllCitiesdd();
            cache.Set("key1", lst);           

        }
        public ActionResult ViewAll()
        {
            Dictionary<string, List<ViewRegisteredUser>> lst = ViewRegisteredUser.ViewAll1();
            return View(lst);
        }
        public ActionResult HomePage()
        {
            string FN = HttpContext.Session.GetString("FN");

            if (FN == null)
            {
                ViewRegisteredUser r = new ViewRegisteredUser();
                r.FullName = Request.Cookies["mycookie2"];
                string cF = r.FullName;
                ViewBag.cF = cF;
            }

            ViewBag.FN = FN;
            return View();
        }


        // GET: RegisteredUserController/Create
        public ActionResult Register()
        {
            try
            {
                ViewRegisteredUser v = new ViewRegisteredUser();
               //v.Citydropdown = ViewRegisteredUser.DisplayAllCitiesdd();
                List<SelectListItem> keyvalue;
               //with cache
                if (cache.TryGetValue("key1", out keyvalue))
                {
                    v.Citydropdown = keyvalue;
                }

                string message = "";
                ViewBag.message = message;
                return View(v);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // POST: RegisteredUserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisteredUser v)
        {
            string message = "";
            try
            {
                RegisteredUser.Insert(v);
                message = "Successfully Registered";
                ViewBag.message = message;
                ViewRegisteredUser v1 = new ViewRegisteredUser();
                v1.Citydropdown = ViewRegisteredUser.DisplayAllCitiesdd();
                return View(v1);
            }
            catch (Exception ex)
            {
                message = "Not Registered due to" + ex.Message;
                ViewBag.message = message;
                ViewRegisteredUser v1 = new ViewRegisteredUser();
                v1.Citydropdown = ViewRegisteredUser.DisplayAllCitiesdd();
                return View(v1);
            }
        }
        public ActionResult Login()
        {
            ViewRegisteredUser r = new ViewRegisteredUser();
            r.LoginName = Request.Cookies["mycookie1"];
            r.FullName = Request.Cookies["mycookie2"];
            r.Password = Request.Cookies["mycookie3"];
            if (r.LoginName != null)
            {
                string FN = HttpContext.Session.GetString("FN");
                RegisteredUser r1 = ViewRegisteredUser.IsValidUserV(r.LoginName, r.Password);
                if (r1 != null)
                {
                    return RedirectToAction(nameof(HomePage));
                }
                else
                    return View(r);
            }
            else
                return View(r);
        }


        // POST: RegisteredUserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(RegisteredUser r)
        {
            try
            {
                string message = "";
                if (ViewRegisteredUser.IsValidUser(r) == true)
                {
                    HttpContext.Session.SetString("FN", r.FullName);
                    HttpContext.Session.SetString("LN", r.LoginName);

                    if (r.isActive == true)
                    {

                        CookieOptions co = new CookieOptions();
                        co.Expires = DateTime.Now.AddMinutes(2);
                        Response.Cookies.Append("mycookie1", r.LoginName, co);
                        Response.Cookies.Append("mycookie2", r.FullName, co);
                        Response.Cookies.Append("mycookie3", r.Password, co);
                        return RedirectToAction(nameof(HomePage));
                    }

                    return RedirectToAction(nameof(HomePage));
                }


                else
                {
                    message = "Incorrect LoginName and Password";
                    ViewBag.message = message;
                    return View();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult LogOut()
        {
            Response.Cookies.Delete("mycookie1");
            Response.Cookies.Delete("mycookie2");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        //GET: RegisteredUserController/Edit/5
        public ActionResult UpdateProfile()
        {

            try
            {

                string LN = HttpContext.Session.GetString("LN");
                ViewRegisteredUser r = new ViewRegisteredUser();               
                r.LoginName = Request.Cookies["mycookie1"];
                string s = r.LoginName;
                if (s!= null)
                {
                    r = ViewRegisteredUser.GetSingleUserV(s);
                }
                if (s == null)
                {
                    ViewRegisteredUser.GetSingleUserV(LN);
                }
                string message = "";
                ViewBag.message = message;

                return View(r);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //POST: RegisteredUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string LoginName, ViewRegisteredUser v1)
        {
            try
            {
                v1.Citydropdown = ViewRegisteredUser.DisplayAllCitiesdd();
                ViewRegisteredUser.UpdateV(LoginName, v1);

                string message = "Updated";
                ViewBag.message = message;
                return View(v1);
            }
            catch
            {
                return View();
            }
        }


    }
}