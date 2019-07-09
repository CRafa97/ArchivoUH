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
            IndexTable = new TableViewModel();
        }

        [Key]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "El nombre de la facultad es obligatorio")]
        [StringLength(100)]
        public string FacultyName { get; set; }

        public TableViewModel IndexTable { get; set; }
    }
}