namespace Erste
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("erste.administrator")]
    public partial class administrator
    {
        [Required]
        [StringLength(256)]
        public string KorisnickoIme { get; set; }

        [Required]
        [StringLength(256)]
        public string LozinkaHash { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public virtual osoba osoba { get; set; }
    }
}
