using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HQ4A.Models
{
    public class UsuariosMetaData
    {
        public int Id { get; set; }
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Porfavor ingrese su Usuario")]
        public string Nombre { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Porfavor ingrese su Contraseña")]
        public string Pwd { get; set; }
        public Nullable<int> IdRol { get; set; }
    }

    [MetadataType(typeof(UsuariosMetaData))]
    public partial class Usuarios
    {

    }
}