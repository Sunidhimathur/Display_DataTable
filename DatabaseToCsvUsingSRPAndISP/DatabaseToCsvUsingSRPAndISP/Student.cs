using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstApproach
{
    class Student
    {
        [Key]
        public int Rollno { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsBonafied { get; set; }
        public int ContactNo { get; set; }

        //[ForeignKey("Programme")]
        public int ProgrammeId { get; set; }

        //navigation property
        [ForeignKey("ProgrammeId")]
        public virtual Programme Programme { get; set; }
        public virtual StudentAddress StudentAddress { get; set; }
    }
}
