using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ArchivoUH.Domain;

namespace ArchivoUH.Models
{
    public class CountryViewModel
    {
        public CountryViewModel(Country country)
        {
            CountryId = country.CountryId;
            CountryName = country.CountryName;
            IndexTable = new TableViewModel();
        }

        public CountryViewModel()
        {
            IndexTable = new TableViewModel();
        }

        [Required]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "El nombre del pais es obligatorio")]
        [StringLength(120, ErrorMessage = "La longitud maxima del nombre es 120 letras")]
        public string CountryName { get; set; }

        public TableViewModel IndexTable { get; set; }
    }

    public class ProvinceViewModel
    {
        public ProvinceViewModel(Province province)
        {
            ProvinceId = province.ProvinceId;
            CountryId = province.CountryId;
            ProvinceName = province.ProvinceName;
            IndexTable = new TableViewModel();
        }

        public ProvinceViewModel()
        {
            IndexTable = new TableViewModel();
        }

        [Required]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "El nombre de la provincia es obligatorio")]
        [StringLength(50, ErrorMessage = "El maximo de caracteres admitidos es 50")]
        public string ProvinceName { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un pais, si no existe ninguno, agregelo primero")]
        public int CountryId { get; set; }

        public TableViewModel IndexTable { get; set; }
    }

    public class LocalityViewModel
    {
        public LocalityViewModel(Locality locality)
        {
            LocalityId = locality.LocalityId;
            LocalityName = locality.LocalityName;
            ProvinceId = locality.ProvinceId;
            IndexTable = new TableViewModel();
        }

        public LocalityViewModel()
        {
            IndexTable = new TableViewModel();
        }

        [Required]
        public int LocalityId { get; set; }

        [Required(ErrorMessage = "El nombre de la localidad es obligatorio")]
        [StringLength(50)]
        public string LocalityName { get; set; }

        [Required(ErrorMessage = "Seleccione una provincia, si no existe ninguna agregela primero")]
        public int ProvinceId { get; set; }

        public TableViewModel IndexTable { get; set; }
    }
}