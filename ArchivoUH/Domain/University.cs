using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchivoUH.Domain
{ 
    public class Faculty
    {
        public Faculty()
        {
            Graduates = new HashSet<Graduated>();
            Leaves = new HashSet<Leaved>();
        }

        [Key]
        public int FacultyId { get; set; }

        [Required]
        [StringLength(100)]
        public string FacultyName { get; set; }

        public virtual ICollection<Graduated> Graduates { get; set; }

        public virtual ICollection<Leaved> Leaves { get; set; }
    }

    public class Course
    {
        public Course()
        {
            Graduates = new HashSet<Graduated>();
            Leaves = new HashSet<Leaved>();
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        public virtual ICollection<Graduated> Graduates { get; set; }

        public virtual ICollection<Leaved> Leaves { get; set; }
    }
}