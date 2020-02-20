namespace Erste
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("erste.grupa")]
    public partial class grupa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public grupa()
        {
            termini = new HashSet<termin>();
            polaznici = new HashSet<polaznik>();
            profesori = new HashSet<profesor>();
        }

        public int Id { get; set; }

        public int KursId { get; set; }

        public int BrojClanova { get; set; }

        [Required]
        [StringLength(256)]
        public string Naziv { get; set; }

        [Column(TypeName = "date")]
        public DateTime DatumOd { get; set; }

        [Column(TypeName = "date")]
        public DateTime DatumDo { get; set; }

        public bool Vazeca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<termin> termini { get; set; }

        public virtual kurs kurs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<polaznik> polaznici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<profesor> profesori { get; set; }
    }
}
