namespace Erste
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("erste.kurs")]
    public partial class kurs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kurs()
        {
            grupe = new HashSet<grupa>();
            polaznici_na_cekanju = new HashSet<polaznik_na_cekanju>();
            profesori = new HashSet<profesor>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Nivo { get; set; }

        public int JezikId { get; set; }

        public bool Vazeci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<grupa> grupe { get; set; }

        public virtual jezik jezik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<polaznik_na_cekanju> polaznici_na_cekanju { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<profesor> profesori { get; set; }

        protected bool Equals(kurs other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((kurs) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
