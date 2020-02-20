namespace Erste
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("erste.osoba")]
    public partial class osoba
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Ime { get; set; }

        [Required]
        [StringLength(256)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(256)]
        public string BrojTelefona { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        public bool Vazeci { get; set; }

        public virtual administrator administrator { get; set; }

        public virtual profesor profesor { get; set; }

        public virtual polaznik polaznik { get; set; }

        public virtual sluzbenik sluzbenik { get; set; }
    }
}
