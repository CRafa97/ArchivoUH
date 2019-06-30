using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchivoUH.Domain
{
    public class KeyWord
    {
        public KeyWord()
        {
            Administratives = new HashSet<Administrative>();
        }

        [Key]
        public int KeyWordId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Administrative> Administratives { get; set; }
    }
}