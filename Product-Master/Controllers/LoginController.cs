using Product_Master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Product_Master.Controllers
{
    public class LoginController : Controller
        
    {

        // GET: Login
        public ActionResult Login(String usertype)
        {
            TempData["UserType"] = usertype;
            return View();

        }
        [HttpPost]
        public ActionResult Login(Logins login)
        {

            //ProductMasterEntities1 dbentities = new ProductMasterEntities1();
            //userlogin ulogin = new userlogin();
            //ulogin = dbentities.userlogin.Where(x => x.username == login.username && x.password == login.password).FirstOrDefault();
            //if (ulogin==null)
            //{

            //}
            //dbentities.userlogin.Add(ulogin);
            //dbentities.SaveChanges();
            return View();

        }
        public ActionResult InternalRegister()
        {
            return View();

        }

        [HttpPost]
        public ActionResult InternalRegister(RegisterInternal interuser)
        {
            ProductMasterEntities dbentities = new ProductMasterEntities();


            userlogin ulogin = new userlogin();
            ulogin.username = interuser.email_id;
            ulogin.password = interuser.password;
            ulogin.usertype = "Internal";
            dbentities.userlogin.Add(ulogin);
            dbentities.SaveChanges();

            int UserId = dbentities.userlogin.Max(x => x.id);
            internaluser internuser1 = new internaluser();
            internuser1.first_name = interuser.first_name;
            internuser1.last_name = interuser.last_name;
            internuser1.address = interuser.address;
            internuser1.designation = interuser.designation;
            internuser1.employee_id = interuser.employee_id;
            internuser1.email_id = interuser.email_id;
            internuser1.userid = UserId;
            dbentities.internaluser.Add(internuser1);
            dbentities.SaveChanges();
            return View();
        }

        public ActionResult ExternalUser()
        {
            return View();

        }

    }
    
}
