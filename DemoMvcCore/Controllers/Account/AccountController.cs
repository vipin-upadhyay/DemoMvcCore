using DemoMvcCore.DataModel;
using DemoMvcCore.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Owin.Security;

namespace DemoMvcCore.Controllers.Account
{

    public class AccountController : Controller
    {
        public readonly AppDbContext _appDbContext;
        [BindProperty]
        public UserRegistration _registration { get; set; }
       // private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        public AccountController(AppDbContext appDbContext)
        {
                this._appDbContext = appDbContext;
            //this._registration = registration;  
        }
        //public UserManager<IdentityUser> _userManager;
        //public SignInManager<IdentityUser> signInManager;
        //public AccountController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        //{
        //    this.signInManager = signInManager;
        //    this._userManager = userManager;
        //}
        
        public IActionResult Index()
        {
            if (TempData["Success"] != null)
            {
                var getData = _appDbContext.Registrations.OrderBy(x => x.FirstName).ThenBy(x => x.Lastname).ToList();
                return View(getData);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ViewResult UserRegisteration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserRegisteration(RegisterVM registerVM)
        {
            try
            {
                if(registerVM!=null)
                { 
                var isEmail = _appDbContext.Registrations.Any(x => x.Email == registerVM.Email && x.Password == registerVM.Password);
                
                if (isEmail)
                {
                    ModelState.AddModelError("Email", "Email already exist ");
                }
                    if (ModelState.IsValid)
                    {
                        
                            _appDbContext.Registrations.Add(_registration);
                        _appDbContext.SaveChanges();
                        TempData["Success"] = "Registration successfull";
                        return RedirectToAction("Login");
                    }
                   
                    
                }

                //return Page();
            }
            catch (Exception ex)
            {
                
                //ex.Data.Add("FirstName", data);
                //ex.Data.Add("LastName", registration.LastName);
                //ex.Data.Add("Email", registration.Email);
                return RedirectToPage("Error");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login( LoginVM loginVM)
        {
            if(loginVM!=null)
            {
                var isSucceded = _appDbContext.Registrations.Any(r => r.Email == loginVM.Email && r.Password == loginVM. Password);

                if (isSucceded)
                {
                    var firstName = _appDbContext.Registrations.FirstOrDefault(x => x.Email == loginVM.Email && x.Password == loginVM.Password);
                    TempData["Success"] = "Login Successfull";
                    TempData["FirstName"] = firstName?.FirstName + " " + firstName?.Lastname;
                    return RedirectToAction("Index");
                }
                return View();
            }

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editedData = _appDbContext.Registrations.Find(id);
            var registerViewModel = new RegisterVM
            {
                Id = id,
                FirstName = editedData.FirstName,
                LastName = editedData.Lastname,
                Email = editedData.Email,
                Password = editedData.Password,
            };

            if (editedData != null)
                TempData["FirstName"] = editedData.FirstName + " " + editedData.Lastname;
            TempData["Success"] = "Edited Successfull";
            return View(registerViewModel);
        }
        [HttpPost]
        public IActionResult Edit(RegisterVM registerVM)
        {
            try
            {
                //registerVM.IsDataEdited
                if (ModelState.IsValid)
                {
                    //registerVM.IsDataEdited = true;
                    UserRegistration userRegisteration = new UserRegistration
                    {
                        Id = registerVM.Id,
                        FirstName = registerVM.FirstName,
                        Lastname = registerVM.LastName,
                        Email = registerVM.Email,
                        Password=registerVM.Password,
                        IsDataedited = true,
                    };
                    _appDbContext.Registrations.Update(userRegisteration);
                    _appDbContext.SaveChanges();
                    TempData["FirstName"] = userRegisteration.FirstName + " " + userRegisteration.Lastname;
                    TempData["Success"] = "Updated Successfull";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                //ex.Data.Add("FirstName", registration.FirstName);
                //ex.Data.Add("LastName", registration.LastName);
                //ex.Data.Add("Email", registration.Email);
                return RedirectToPage("Error");
            }

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var editedData = _appDbContext.Registrations.Find(id);
            var registerViewModel = new RegisterVM
            {
                Id = id,
                FirstName = editedData.FirstName,
                LastName = editedData.Lastname,
                Email = editedData.Email,
                Password = editedData.Password,
            };

            if (editedData != null)
                TempData["FirstName"] = editedData.FirstName + " " + editedData.Lastname;
            //TempData["Success"] = "Edited Successfull";
            return View(registerViewModel);
        }
        [HttpPost]
        public IActionResult Delete(RegisterVM registerVM)
        {
            try
            {
                ModelState.Remove("Password");
                if (ModelState.IsValid)
                {
                    registerVM.IsDataEdited = true;
                    UserRegistration userRegisteration = new UserRegistration
                    {
                        Id = registerVM.Id,
                        FirstName = registerVM.FirstName,
                        Lastname = registerVM.LastName,
                        Email = registerVM.Email,
                        Password = registerVM.Password,
                    };
                    _appDbContext.Registrations.Remove(userRegisteration);
                    _appDbContext.SaveChanges();
                    TempData["FirstName"] = userRegisteration.FirstName + " " + userRegisteration.Lastname;
                    TempData["Success"] = "Deleted Successfull";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                //ex.Data.Add("FirstName", registration.FirstName);
                //ex.Data.Add("LastName", registration.LastName);
                //ex.Data.Add("Email", registration.Email);
                return RedirectToPage("Error");
            }

        }
        [HttpGet]
        public IActionResult ExternalLogin()
        {
            //var properties = AuthenticationManager.get
            TempData["Success"] = "Login successfull";
            return View(); //Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [HttpPost]
        public IActionResult ExternalLogin( string provider=null,string returnUrl=null)
        {
            return View();  
        }

    }
    }

