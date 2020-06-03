using HQ4A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HQ4A.Controllers
{
    public class UsuariosController : Controller
    {
        private HQ4AEntities db = new HQ4AEntities();

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Usuarios u)
        {
            Usuarios usuario = db.Usuarios.FirstOrDefault(x => x.Nombre == u.Nombre && x.Pwd == u.Pwd);
            if (usuario == null)
            {
                ViewBag.Error = "Usuario y/o contraseña incorrectas";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(usuario.Nombre, false);
                Session["IdUsuario"] = usuario.Id;
                Session["Usuario"] = usuario.Nombre;
                return RedirectToAction("Principal", "Productos");
            }
        }

        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(Usuarios usuario)
        {
            db.Usuarios.Add(usuario);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //Vista , Controlador
            return RedirectToAction("Login", "Usuarios");
        }
    }
}