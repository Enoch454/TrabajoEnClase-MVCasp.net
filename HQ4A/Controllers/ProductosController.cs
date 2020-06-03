using HQ4A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HQ4A.Controllers
{
    public class ProductosController : Controller
    {
        private HQ4AEntities db = new HQ4AEntities();

        [Authorize(Roles = "Administrador, Normal")]
        public ActionResult Principal()
        {
            //select * from Productos
            return View(db.Productos.ToList());
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult AgregarProducto()
        {
            ViewBag.IdProveedor = new SelectList(db.Proveedores.ToList(), "Id", "NombreProveedor");
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(Productos p)
        {
            db.Productos.Add(p);
            db.SaveChanges();
            return RedirectToAction("Principal");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Actualizar(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Error");
            }
            Productos p = db.Productos.Find(Id);
            if (p == null)
            {
                return RedirectToAction("Error");
            }
            ViewBag.IdProveedor = new SelectList(db.Proveedores.ToList(), "Id", "NombreProveedor", p.Id);
            return View(p);
        }

        [HttpPost]
        public ActionResult Actualizar(Productos p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Principal");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Eliminar(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Error");
            }
            Productos p = db.Productos.Find(Id);
            if (p == null)
            {
                return RedirectToAction("Error");
            }
            db.Productos.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Principal");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteProduct(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Principal");
            }
            Productos p = db.Productos.Find(Id);
            if (p == null)
            {
                return RedirectToAction("Error");
            }
            return View(p);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Productos p)
        {
            Productos paux = db.Productos.Find(p.Id);
            db.Productos.Remove(paux);
            db.SaveChanges();
            return RedirectToAction("Principal");
        }

        public ActionResult Error()
        {
            return View();
        }


        public ActionResult Detalles(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Error");
            }
            Productos p = db.Productos.Find(Id);
            if (p == null)
            {
                return RedirectToAction("Error");
            }
            return View(p);
        }

        public ActionResult AddToCart(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Error");
            }
            Productos p = db.Productos.Find(Id);
            if (p == null)
            {
                return RedirectToAction("Error");
            }
            OrdenVenta c = new OrdenVenta();
            c.IdProducto = p.Id;
            c.Cantidad = 0;
            ViewBag.IdProducto = Id;
            return View(c);
        }

        [HttpPost]
        public ActionResult AddToCart(OrdenVenta c)
        {
            Productos p = db.Productos.Find(c.IdProducto);
            if (c.Cantidad > p.Stock)
            {
                ViewBag.Error = "No tenemos el stock que requiere :c";
                return View(c);
            }
            else
            {
                if (Session["Carrito"] == null)
                {
                    List<OrdenVenta> ListaCarrito = new List<OrdenVenta>();
                    ListaCarrito.Add(c);
                    Session["Carrito"] = ListaCarrito;
                }
                else
                {
                    //List<Carrito> ListaCarrito = (List<Carrito>)Session["Carrito"];
                    List<OrdenVenta> ListaCarrito = Session["Carrito"] as List<OrdenVenta>;
                    bool existeEnCarrito = false;
                    for(int i=0; i<ListaCarrito.Count; i++)
                    {
                        if(c.IdProducto == ListaCarrito[i].IdProducto)
                        {
                            ListaCarrito[i].Cantidad += c.Cantidad;
                            existeEnCarrito = true;
                            break;
                        }
                    }
                    if (!existeEnCarrito)
                    {
                        ListaCarrito.Add(c);
                    }
                    Session["Carrito"] = ListaCarrito;
                }
                return RedirectToAction("Principal");
            }
        }

        public ActionResult Cart()
        {
            List<OrdenVenta> carritos;
            if (Session["Carrito"] == null)
            {
                carritos = new List<OrdenVenta>();
            }
            else
            {
                decimal total = 0;
                carritos = Session["Carrito"] as List<OrdenVenta>;
                for(int i = 0; i < carritos.Count; i++)
                {
                    total = total + carritos[i].SubTotal;
                }
                TempData["Total"] = total;
            }
            return View(carritos);
        }

        public ActionResult PayCart()
        {
            if(Session["Carrito"] == null)
            {
                return RedirectToAction("Principal");
            }
            else
            {
                List<OrdenVenta> carritos = Session["Carrito"] as List<OrdenVenta>;
                decimal? monto = 0;

                //aqui devería ingresar en base de datos 
                //1. La venta
                Ventas v = new Ventas();
                v.Fecha = DateTime.Now;
                v.Id_cliente = (int)Session["IdUsuario"];
                List<DetalleVentas> list = new List<DetalleVentas>();

                foreach (var item in carritos)
                {
                    //2. Los detalles de la venta
                    DetalleVentas dv = new DetalleVentas();
                    dv.Nombre = item.Nombre;
                    dv.Precio = item.Precio;
                    dv.Cantidad = item.Cantidad;
                    dv.Subtotal = item.SubTotal;
                    list.Add(dv);

                    monto = monto + item.SubTotal;
                    Productos p = db.Productos.Find(item.IdProducto);
                    p.Stock -= item.Cantidad;
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                v.monto = monto;
                v.DetalleVentas = list;
                db.Ventas.Add(v);
                db.SaveChanges();
                Session["Carrito"] = null;
                return RedirectToAction("Principal");
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult consultarVentas()
        {
            return View(db.Ventas.ToList());
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult DetalleVenta(int id)
        {
            return View(db.DetalleVentas.Where(i => i.Id_venta == id).ToList());
        }
    }
}