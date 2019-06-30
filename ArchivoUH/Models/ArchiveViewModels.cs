using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArchivoUH.Domain;
using System.ComponentModel.DataAnnotations;

namespace ArchivoUH.Models
{
    public class KeyWordViewModel
    {
        public KeyWordViewModel()
        {
            IndexTable = new TableViewModel();
        }

        public KeyWordViewModel(KeyWord keyWord)
        {
            KeyWordId = keyWord.KeyWordId;
            Name = keyWord.Name;
            IndexTable = new TableViewModel();
        }

        [Required]
        public int KeyWordId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public TableViewModel IndexTable { get; set; }
    }
}