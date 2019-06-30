using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchivoUH.Domain
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [StringLength(120)]
        public string CountryName { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }

    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProvinceName { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual ICollection<Locality> Localities { get; set; }
    }

    public class Locality
    {
        public Locality()
        {
            Graduates = new HashSet<Graduated>();
        }

        [Key]
        public int LocalityId { get; set; }

        [Required]
        [StringLength(50)]
        public string LocalityName { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }

        public virtual ICollection<Graduated> Graduates { get; set; }
    }
}