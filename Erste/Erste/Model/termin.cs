namespace Erste
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("erste.termin")]
    public partial class termin
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Dan { get; set; }

        public TimeSpan Od { get; set; }

        public TimeSpan Do { get; set; }

        public int? GrupaId { get; set; }

        public virtual grupa grupa { get; set; }
    }
}
