namespace Erste
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("erste.jezik")]
    public partial class jezik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public jezik()
        {
            kursevi = new HashSet<kurs>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Naziv { get; set; }

        public bool Vazeci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<kurs> kursevi { get; set; }

        protected bool Equals(jezik other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((jezik) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
