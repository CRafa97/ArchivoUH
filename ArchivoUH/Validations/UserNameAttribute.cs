using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchivoUH.Validations
{
    public class UserNameAttribute : ValidationAttribute
    {
        public UserNameAttribute() { }

        public override bool IsValid(object value)
        {
            return (!(value is string user)) ? false : user.Length >= 3 && !user.All(x => char.IsDigit(x)); 
        }
    }
}