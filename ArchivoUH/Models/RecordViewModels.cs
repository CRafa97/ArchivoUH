using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ArchivoUH.Domain;
using ArchivoUH.Validations;

namespace ArchivoUH.Models
{
    public class AdministrativeViewModel
    {
        public AdministrativeViewModel()
        {
            IndexTable = new TableViewModel();
        }

        public AdministrativeViewModel(Administrative administrative)
        {
            AdministrativeId = administrative.AdministrativeId;
            AdministrativeName = administrative.AdministrativeName;
            Description = administrative.Description;
            KeyWordId = administrative.KeyWordId;
            Serial1 = administrative.Serial1;
            Serial2 = administrative.Serial2;
            CurrentKW = administrative.KeyWord.Name;
            IndexTable = new TableViewModel();
        }

        //[Required(ErrorMessage = "El campo Serial1 es obligatorio")]
        [RegularExpression("[0-9]+", ErrorMessage = "El campo debe ser un numero")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Debe ser un numero de 3 digitos exactamente")]
        public string Serial1 { get; set; }

        //[Required(ErrorMessage = "El campo Serial2 es obligatorio")]
        [RegularExpression("[0-9]+", ErrorMessage = "El campo debe ser un numero")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Debe ser un numero de 3 digitos exactamente")]
        public string Serial2 { get; set; }

        [Required]
        public int AdministrativeId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public string AdministrativeName { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una palabra clave, en caso que no existe creela")]
        public int KeyWordId { get; set; }

        public SerialType SerialType => SerialType.ADM;

        public TableViewModel IndexTable { get; set; }

        public string Formatter => $"{Serial1}-{Serial2}-ADM";

        public string CurrentKW { get; set; }

    }

    public class LeavedViewModel
    {
        public LeavedViewModel()
        {
            IndexTable = new TableViewModel();
        }

        public LeavedViewModel(Leaved leaved)
        {
            LeavedId = leaved.LeavedId;
            Serial1 = leaved.Serial1;
            Serial2 = leaved.Serial2;
            SerialType = leaved.SerialType;
            FirstName = leaved.FirstName;
            LastName = leaved.LastName;
            FacultyId = leaved.FacultyId;
            CourseId = leaved.CourseId;
            LeavedDate = leaved.LeavedDate;
            Causes = leaved.Causes;
            CurrentFaculty = leaved.Faculty.FacultyName;
            CurrentCourse = leaved.Course.CourseName;
            IndexTable = new TableViewModel();
        }

        //[Required(ErrorMessage = "El campo Serial1 es obligatorio")]
        [RegularExpression("[0-9]+", ErrorMessage = "El campo Serial1 debe ser un numero")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El campo Serial1 debe ser un numero de 3 digitos exactamente")]
        public string Serial1 { get; set; }

        //[Required(ErrorMessage = "El campo Serial2 es obligatorio")]
        [RegularExpression("[0-9]+", ErrorMessage = "El campo Serial2 debe ser un numero")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El campo Serial2 debe ser un numero de 3 digitos exactamente")]
        public string Serial2 { get; set; }

        [Required]
        public SerialType SerialType { get; set; }

        [Required]
        public int LeavedId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe ser más largo que dos caracteres")]
        [RegularExpression("[a-zA-Z]+( [a-zA-Z]+)?", ErrorMessage = "El nombre insertado no es válido, solamente debe contener letras y dos nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe ser más largo que dos caracteres")]
        [RegularExpression("[a-zA-Z]+( [a-zA-Z]+)?", ErrorMessage = "El apellido insertado no es válido, solamente debe contener letras y dos apellidos")]
        public string LastName { get; set; }

        [Required]
        public int FacultyId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "La fecha de baja es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Range(typeof(DateTime), "1800-01-01", "2100-01-01", ErrorMessage = "La fecha debe estar entre 01-01-1800 y 2100-01-01")]
        public DateTime LeavedDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Causes { get; set; }

        public string Formatter => $"{Serial1}-{Serial2}-{SerialType}";

        public string CurrentFaculty { get; set; }

        public string CurrentCourse { get; set; }

        public TableViewModel IndexTable { get; set; }
    }

    public class GraduatedViewModel
    {
        public GraduatedViewModel()
        {
            IndexTable = new TableViewModel();
        }

        public GraduatedViewModel(Graduated graduated)
        {
            GraduatedId = graduated.GraduatedId;
            Serial1 = graduated.Serial1?? "000";
            Serial2 = graduated.Serial2 ?? "000"; 
            SerialType = graduated.SerialType;
            FirstName = graduated.FirstName;
            LastName = graduated.LastName;
            FacultyId = graduated.FacultyId;
            CourseId = graduated.CourseId;
            LocalityId = graduated.LocalityId;
            ProvinceId = graduated.ProvinceId;
            CountryId = graduated.CountryId;
            CurrentCountry = graduated.Country.CountryName;
            CurrentLocality = graduated.Locality != null ? graduated.Locality.LocalityName : "";
            CurrentProvince = graduated.Province != null ? graduated.Province.ProvinceName : "";
            CurrentFaculty = graduated.Faculty.FacultyName;
            CurrentCourse = graduated.Course.CourseName;
            FacultyFormatter = $"Tomo: {graduated.FacultyTome}  Folio: {graduated.FacultyFolio}  Número: {graduated.FacultyNumber}";
            TomeUH = graduated.TomeUH;
            FolioUH = graduated.FolioUH;
            NumberUH = graduated.NumberUH;
            FinishTime = graduated.FinishTime;
            ExpeditionTime = graduated.ExpeditionTime;
            Observations = graduated.Observations;
            ScientistCredit = graduated.ScientistCredit;
            ExpeditionCauses = graduated.ExpeditionCauses;
            GoldTitle = graduated.GoldTitle;
            IndexTable = new TableViewModel(); 
        }

        //[Required(ErrorMessage = "El campo Serial1 es obligatorio")]
        [RegularExpression("[0-9]+", ErrorMessage = "El campo Serial1 debe ser un numero")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El campo Serial1 debe ser un numero de 3 digitos exactamente")]
        public string Serial1 { get; set; }

        //[Required(ErrorMessage = "El campo Serial2 es obligatorio")]
        [RegularExpression("[0-9]+", ErrorMessage = "El campo Serial2 debe ser un numero")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El campo Serial2 debe ser un numero de 3 digitos exactamente")]
        public string Serial2 { get; set; }
         
        [Required]
        public SerialType SerialType { get; set; }

        [Required]
        public int GraduatedId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe ser más largo que dos caracteres")]
        //[RegularExpression("[a-zA-Z]+( [a-zA-Z]+)?", ErrorMessage = "El nombre insertado no es válido, solamente debe contener letras y dos nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe ser más largo que dos caracteres")]
        //[RegularExpression("[a-zA-Z]+( [a-zA-Z]+)?", ErrorMessage = "El apellido insertado no es válido, solamente debe contener letras y dos apellidos")]
        public string LastName { get; set; }

        public int? LocalityId { get; set; }

        public int? ProvinceId { get; set; }

        [Required(ErrorMessage = "Expecificar la nacionalidad del graduado es obligatorio")]
        public int? CountryId { get; set; }

        public string CurrentFaculty { get; set; }
        public string CurrentCourse { get; set; }
        public string CurrentCountry { get; set; }
        public string CurrentProvince { get; set; }
        public string CurrentLocality { get; set; }
        public string FacultyFormatter { get; }

        [Required]
        public int FacultyId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "El tomo UH es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El tomo UH debe ser un entero positivo")]
        public int TomeUH { get; set; }

        [Required(ErrorMessage = "El folio UH es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El folio UH debe ser un entero positivo")]
        public int FolioUH { get; set; }

        [Required(ErrorMessage = "El número UH es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El número UH debe ser un entero positivo")]
        public int NumberUH { get; set; }

        [Required(ErrorMessage = "El tomo de la facultad es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El tomo de la facultad debe ser un entero positivo")]
        public int FacultyTome { get; set; }

        [Required(ErrorMessage = "El folio de la facultad es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El folio de la facultad debe ser un entero positivo")]
        public int FacultyFolio { get; set; }

        [Required(ErrorMessage = "El número de la facultad es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El número de la facultad debe ser un entero positivo")]
        public int FacultyNumber { get; set; }

        //Dates
        [Required(ErrorMessage = "La fecha de finalización es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Range(typeof(DateTime), "1800-01-01", "2100-01-01", ErrorMessage = "La fecha de finalizacion debe estar entre 01-01-1800 y 01-01-2100")]
        public DateTime FinishTime { get; set; }

        [Required(ErrorMessage = "La fecha de expedición es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Range(typeof(DateTime), "1800-01-01", "2100-01-01", ErrorMessage = "La fecha de expedición debe estar entre 01-01-1800 y 01-01-2100")]
        [GreaterThanOrEqual("FinishTime", ErrorMessage = "La fecha de expedicion debe ser igual o posterior a la fecha de finalizacion")]
        public DateTime ExpeditionTime { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string ExpeditionCauses { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string Observations { get; set; }

        public bool GoldTitle { get; set; }

        public bool ScientistCredit { get; set; }

        public string Formatter => $"{Serial1}-{Serial2}-{SerialType}";

        public string UHFormatter => $"Tomo: {TomeUH}  Folio: {FolioUH}  Número: {NumberUH}";

        public TableViewModel IndexTable { get; set; }
    }
}