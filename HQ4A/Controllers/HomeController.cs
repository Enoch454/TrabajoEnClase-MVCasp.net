using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HQ4A.Models;

namespace HQ4A.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            string nombre = "Braiam T.";
            ViewBag.NombrePerron = nombre;
            string pregunta = "¿Qué Show?";
            ViewBag.Pregunta = pregunta;
            return View();
        }

        public ActionResult AcercaDe()
        {
            TempData["Mensaje"] = "Yo llegué de la vista Acerca De...";
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtUsuario, string txtPassword)
        {
            if(txtUsuario == "Braiam T." && txtPassword == "puebla123")
            {
                return RedirectToAction("Index");
            } else
            {
                ViewBag.Error = "Usuario y/o password incorrectos";
                return View();
            }
        }

        public ActionResult LoginModelo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginModelo(Login login)
        {
            if(login.Usuario == "Braiam T." && login.Password == "puebla123")
            {
                return RedirectToAction("Index");
            } else
            {
                ViewBag.Error = "Usuario y/o contraseña incorrectas";
            }
            return View();
        }
    }
}