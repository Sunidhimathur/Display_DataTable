using System.Collections.Generic;

namespace CodeFirstApproach
{
    class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }

        //navigation property
        public virtual ICollection<Programme> Programmes { get; set; }
    }
}