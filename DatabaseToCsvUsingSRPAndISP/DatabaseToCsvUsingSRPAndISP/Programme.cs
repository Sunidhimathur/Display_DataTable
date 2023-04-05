using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstApproach
{
    [Table("Course")]
    class Programme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } //PK

        [Required]
        [Column("CourseName", TypeName = "varchar")]
        [MaxLength(50)]
        public string Title { get; set; }

        public int Duration { get; set; }
        public float Fees { get; set; }

        //navigation property
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
