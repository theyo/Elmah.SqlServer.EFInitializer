namespace Elmah.SqlServer.EFInitializer
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Configuration;
    using System.Collections;

    public class ElmahContext : DbContext
    {
        private ElmahContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        // ReSharper disable once InconsistentNaming
        public virtual DbSet<ELMAH_Error> ELMAH_Errors { get; set; }

        public static void Initialize()
        {
            var errorLogSection = ConfigurationManager.GetSection("elmah/errorLog") as IDictionary;

            if (errorLogSection == null)
                throw new NullReferenceException("elmah/errorLog section in web.config is missing");


            if (!errorLogSection.Contains("connectionStringName"))
                throw new NullReferenceException("elmah/errorLog section in web.config is missing \"connectionStringName\" property");


            using (var context = new ElmahContext(errorLogSection["connectionStringName"].ToString()))
            {
                context.Database.Initialize(true);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }

    // ReSharper disable once InconsistentNaming
    public class ELMAH_Error
    {
        [Key]
        public Guid ErrorId { get; set; }

        [Required]
        [StringLength(60)]
        public string Application { get; set; }

        [Required]
        [StringLength(50)]
        public string Host { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [StringLength(60)]
        public string Source { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [Required]
        [StringLength(50)]
        public string User { get; set; }

        public int StatusCode { get; set; }

        public DateTime TimeUtc { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sequence { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string AllXml { get; set; }
    }
}