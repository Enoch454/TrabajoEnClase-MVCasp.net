using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HQ4A.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Porfavor ingrese su usuario")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Porfavor ingrese su contraseña")]
        public string Password { get; set; }
    }
}