using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HQ4A.Models
{
    public class OrdenVenta
    {
        public int IdProducto { get; set; }
        public string Nombre { 
            get
            { 
                using (HQ4AEntities db = new HQ4AEntities())
                {
                    return db.Productos.Find(IdProducto).NombreProducto;
                }
            }
        }
        public decimal Precio {
            get
            {
                using (HQ4AEntities db = new HQ4AEntities())
                {
                    return (decimal)(db.Productos.Find(IdProducto).Precio);
                }
            }
        }
        public int Cantidad { get; set; }
        public decimal SubTotal {
            get
            {
                return Cantidad * Precio;
            }
        }

    }
}