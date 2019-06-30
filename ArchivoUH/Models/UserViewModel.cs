using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ArchivoUH.Domain.Auth;

namespace ArchivoUH.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Password = "";
        }

        public UserViewModel()
        {
            
        }

        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        [Display(Name = "User Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Solamente se admiten letras en el nombre de usuario")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "El nombre usuario debe tener como mínimo 4 letras y 12 como máximo ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo Nombre(s) es obligatorio")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener un largo comprendido entre 2 y 15 letras")]
        [RegularExpression("[a-zA-Z]+( [a-zA-Z]+)?", ErrorMessage = "El nombre insertado no es válido, solamente debe contener letras")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener un largo comprendido entre 2 y 15 letras")]
        [RegularExpression("[a-zA-Z]+( [a-zA-Z]+)?", ErrorMessage = "El apellido insertado no es válido, solamente debe contener letras")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Inserte una dirección de correo correcta")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "La contraseña debe tener más de 4 caracteres de largo", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}