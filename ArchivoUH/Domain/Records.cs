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
        ADM,
        NEW,
        OLD
    }

    public abstract class Record
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Serial1 { get; set; }

        [Required]
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

        [Required]
        public int LocalityId { get; set; }

        [ForeignKey("LocalityId")]
        public virtual Locality Locality { get; set; }

        //Make relationships
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
        [Range(0,200)]
        public int TomeUH { get; set; }

        [Required]
        [Range(0,200)]
        public int FolioUH { get; set; }

        [Required]
        [Range(0,200)]
        public int NumberUH { get; set; }

        //Dates
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //[Range(typeof(DateTime), "1800-01-01", "2100-01-01")]
        public DateTime FinishTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //[Range(typeof(DateTime), "1800-01-01", "2100-01-01")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Range(typeof(DateTime), "1800-01-01", "2100-01-01")]
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