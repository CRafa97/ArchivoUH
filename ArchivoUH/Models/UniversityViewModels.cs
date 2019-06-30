using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArchivoUH.Domain;
using System.ComponentModel.DataAnnotations;

namespace ArchivoUH.Models
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            IndexTable = new TableViewModel();
        }

        public CourseViewModel(Course course)
        {
            CourseId = course.CourseId;
            CourseName = course.CourseName;
            IndexTable = new TableViewModel();
        }

        [Required]
        public int CourseId { get; set; }

        [Required (ErrorMessage = "El nombre de la carrera es obligatorio")]
        [StringLength(100)]
        public string CourseName { get; set; }

        public TableViewModel IndexTable { get; set; }
    }

    public class FacultyViewModel
    {
        public FacultyViewModel()
        {
            IndexTable = new TableViewModel();
        }

        public FacultyViewModel(Faculty faculty)
        {
            FacultyId = faculty.FacultyId;
            FacultyName = faculty.FacultyName;
            FacultyTome = faculty.FacultyTome;
            FacultyNumber = faculty.FacultyNumber;
            FacultyFolio = faculty.FacultyFolio;
            IndexTable = new TableViewModel();
        }

        [Key]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "El nombre de la facultad es obligatorio")]
        [StringLength(100)]
        public string FacultyName { get; set; }

        [Required (ErrorMessage = "El tomo de la facultad es requerido")]
        [Range(0, 200, ErrorMessage = "Debe ser un entero en el rango 0 - 200")]
        public int FacultyTome { get; set; }

        [Required(ErrorMessage = "El folio de la facultad es requerido")]
        [Range(0, 200, ErrorMessage = "Debe ser un entero en el rango 0 - 200")]
        public int FacultyFolio { get; set; }

        [Required(ErrorMessage = "El numero de la facultad es requerido")]
        [Range(0, 200, ErrorMessage = "Debe ser un entero en el rango 0 - 200")]
        public int FacultyNumber { get; set; }

        public TableViewModel IndexTable { get; set; }
    }
}