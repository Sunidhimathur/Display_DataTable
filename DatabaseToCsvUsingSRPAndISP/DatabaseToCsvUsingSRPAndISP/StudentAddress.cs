using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproach
{
    class StudentAddress
    {
        [Key, ForeignKey("Student")]
        public int StudentId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [NotMapped]
        public string Pincode { get; set; }

        //navigation property
        public virtual Student Student { get; set; }
    }
}
