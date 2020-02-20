namespace Erste
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ErsteModel : DbContext
    {
        public ErsteModel()
            : base("name=Model")
        {
        }

        public virtual DbSet<administrator> administratori { get; set; }
        public virtual DbSet<grupa> grupe { get; set; }
        public virtual DbSet<jezik> jezici { get; set; }
        public virtual DbSet<kurs> kursevi { get; set; }
        public virtual DbSet<osoba> osobe { get; set; }
        public virtual DbSet<polaznik> polaznici { get; set; }
        public virtual DbSet<polaznik_na_cekanju> polaznici_na_cekanju { get; set; }
        public virtual DbSet<profesor> profesori { get; set; }
        public virtual DbSet<sluzbenik> sluzbenici { get; set; }
        public virtual DbSet<termin> termini { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<administrator>()
                .Property(e => e.KorisnickoIme)
                .IsUnicode(false);

            modelBuilder.Entity<administrator>()
                .Property(e => e.LozinkaHash)
                .IsUnicode(false);

            modelBuilder.Entity<grupa>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<grupa>()
                .HasMany(e => e.termini)
                .WithOptional(e => e.grupa)
                .HasForeignKey(e => e.IdGrupe);

            modelBuilder.Entity<grupa>()
                .HasMany(e => e.polaznici)
                .WithMany(e => e.grupe)
                .Map(m => m.ToTable("polaznik_grupa", "erste").MapLeftKey("GrupaId").MapRightKey("PolaznikId"));

            modelBuilder.Entity<grupa>()
                .HasMany(e => e.profesori)
                .WithMany(e => e.grupe)
                .Map(m => m.ToTable("profesor_grupa", "erste").MapLeftKey("GrupaId").MapRightKey("ProfesorId"));

            modelBuilder.Entity<jezik>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<jezik>()
                .HasMany(e => e.kursevi)
                .WithRequired(e => e.jezik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<kurs>()
                .Property(e => e.Nivo)
                .IsUnicode(false);

            modelBuilder.Entity<kurs>()
                .HasMany(e => e.grupe)
                .WithRequired(e => e.kurs)
                .HasForeignKey(e => e.KursId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<kurs>()
                .HasMany(e => e.profesori)
                .WithMany(e => e.kursevi)
                .Map(m => m.ToTable("kurs_profesor", "erste").MapLeftKey("KursId").MapRightKey("ProfesorId"));

            modelBuilder.Entity<osoba>()
                .Property(e => e.Ime)
                .IsUnicode(false);

            modelBuilder.Entity<osoba>()
                .Property(e => e.Prezime)
                .IsUnicode(false);

            modelBuilder.Entity<osoba>()
                .Property(e => e.BrojTelefona)
                .IsUnicode(false);

            modelBuilder.Entity<osoba>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<osoba>()
                .HasOptional(e => e.administrator)
                .WithRequired(e => e.osoba);

            modelBuilder.Entity<osoba>()
                .HasOptional(e => e.profesor)
                .WithRequired(e => e.osoba);

            modelBuilder.Entity<osoba>()
                .HasOptional(e => e.polaznik)
                .WithRequired(e => e.osoba);

            modelBuilder.Entity<osoba>()
                .HasOptional(e => e.sluzbenik)
                .WithRequired(e => e.osoba);

            modelBuilder.Entity<polaznik>()
                .HasOptional(e => e.polaznik_na_cekanju)
                .WithRequired(e => e.polaznik);

            modelBuilder.Entity<polaznik_na_cekanju>()
                .HasMany(e => e.kursevi)
                .WithMany(e => e.polaznici_na_cekanju)
                .Map(m => m.ToTable("kurs_polaznik_na_cekanju", "erste").MapLeftKey("PolaznikNaCekanjuId").MapRightKey("KursId"));

            modelBuilder.Entity<sluzbenik>()
                .Property(e => e.KorisnickoIme)
                .IsUnicode(false);

            modelBuilder.Entity<sluzbenik>()
                .Property(e => e.LozinkaHash)
                .IsUnicode(false);

            modelBuilder.Entity<termin>()
                .Property(e => e.Dan)
                .IsUnicode(false);
        }
    }
}
