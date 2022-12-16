using ImbizoFoundation.Classes;
using ImbizoFoundation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImbizoFoundation.Controllers
{
    public class AdminController : Controller
    {
        FireBaseWorker f = new FireBaseWorker();
        List<User> AdminList = new List<User>();
            // GET: AdminController
        public ActionResult Index()
        {
            AdminList = f.getAllAdmin();

            return View(AdminList);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string username,string password)
        {
            
            string hashPassword = Worker.hashPassword(password);

            if (username != null && hashPassword != null)
            {
                User U = new User(username, hashPassword);

                f.writeAdmin(U);

                return RedirectToAction("Login", "Admin");
            }

            return View();
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                string hashPass = Worker.hashPassword(password);
                AdminList = f.getAllAdmin();

                AdminList = AdminList.Where(x => x.username.Equals(username) && x.password.Equals(hashPass)).ToList();

                if (AdminList[0].username.Equals(username) && AdminList[0].password.Equals(hashPass))
                {
                    return RedirectToAction("Main", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Messsage = "Authentication Error , Please Enter the correct password or username";
            }

            return View();
            
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(string id)
        {
            AdminList = f.getAllAdmin();
            User u = new User();
            u = AdminList.Where(x => x.username.Equals(id)).FirstOrDefault();
            return View(u);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, string username)
        {

            f.deleteAdmin(username);

            return View();
        }
    }
}
