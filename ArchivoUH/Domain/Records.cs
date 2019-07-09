using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchivoUH.Domain
{
    public enum SerialType
    {
        NEW,
        OLD,
        ADM,
    }

    public abstract class Record
    {
        //[Required]
        [StringLength(3, MinimumLength = 3)]
        public string Serial1 { get; set; }

        //[Required]
        [StringLength(3, MinimumLength = 3)]
        public string Serial2 { get; set; }

        [Required]
        public virtual SerialType SerialType { get; set; }
    }

    public class Graduated : Record
    {
        [Key]
        public int GraduatedId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public int? LocalityId { get; set; }

        [ForeignKey("LocalityId")]
        public virtual Locality Locality { get; set; }

        public int? ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [Required]
        public int FacultyId { get; set; } 

        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }

        [Required]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        //UH Data
        [Required]
        public int TomeUH { get; set; }

        [Required]
        public int FolioUH { get; set; }

        [Required]
        public int NumberUH { get; set; }

        // Faculty Data
        [Required]
        public int FacultyTome { get; set; }

        [Required]
        public int FacultyFolio { get; set; }

        [Required]
        public int FacultyNumber { get; set; }

        //Dates
        [Required]
        [DataType(DataType.Date)]
        public DateTime FinishTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpeditionTime { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string ExpeditionCauses { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string Observations { get; set; }

        public bool GoldTitle { get; set; }

        public bool ScientistCredit { get; set; }
    }

    public class Leaved : Record
    {
        [Key]
        public int LeavedId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public int FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }

        [Required]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LeavedDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Causes { get; set; }
    }

    public class Administrative : Record
    {
        public Administrative()
        {
            SerialType = SerialType.ADM;
        }

        [Key]
        public int AdministrativeId { get; set; }

        [Required]
        [StringLength(50)]
        public string AdministrativeName { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public int KeyWordId { get; set; }

        [ForeignKey("KeyWordId")]
        public virtual KeyWord KeyWord { get; set; }

        public sealed override SerialType SerialType => SerialType.ADM;
    }
}