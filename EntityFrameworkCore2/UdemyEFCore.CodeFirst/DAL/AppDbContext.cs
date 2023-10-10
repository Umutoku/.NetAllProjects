using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlServer"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("ProductTBB", "productstbb"); // zincir olabilir, configuration
            //modelBuilder.Entity<Product>().HasKey(p => p.Id); //primary key belirleme
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired(); // zorunlu alan
            //modelBuilder.Entity<Product>().Property(x=>x.Name).HasMaxLength(50); // max karakter uzunluğu
            //modelBuilder.Entity<Product>().Property(x=>x.Name).HasMaxLength(50).IsFixedLength(); //max ve min 50 karakter olabilir

            //Has ile başlanacak daha sonra with // Category birden fazla producta sahip olabilir. ForeignKey ile bağladık
            //modelBuilder.Entity<Category>().HasMany(x=>x.Products).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId);

            // bire bir ilişki foreignKey elle vermek durumundayız
            //modelBuilder.Entity<Product>().HasOne(x => x.ProductFeature).WithOne(x => x.Product).HasForeignKey<ProductFeature>(x => x.ProductId);

            //One to one ilişkilerde best practices olarak productfeature foreign key olarak Product tablosunda id alıyor
            //modelBuilder.Entity<Product>().HasOne(x => x.ProductFeature).WithOne(x => x.Product).HasForeignKey<ProductFeature>(x => x.Id);

            //Çoka çok ilişki elle yapmak için
            //modelBuilder.Entity<Student>().HasMany(x => x.Teachers).WithMany(x => x.Students)
            //    .UsingEntity<Dictionary<string, object>>("StudentTeacherManyToMany",
            //    ip => ip.HasOne<Teacher>().WithMany().HasForeignKey(nameof(Teacher.Id)).HasConstraintName("FKTeacherId"),
            //    x => x.HasOne<Student>().WithMany().HasForeignKey(nameof(Student.Id)).HasConstraintName("FKStudentId"));


            //Eğer bağlı tablolar var ise onlarında içeriği temizlenir
            //modelBuilder.Entity<Category>().HasMany(x=>x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);


            //Eğer bağlı tablolar var ise onlarında içeriği Silemezsin
            //modelBuilder.Entity<Category>().HasMany(x=>x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //Eğer bağlı tablolar var ise onlarında içeriği ben halledeceğim
            //modelBuilder.Entity<Category>().HasMany(x=>x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.NoAction);


            //Eğer bağlı tablolar var ise onlarında içeriği null olarak ayarla
            //modelBuilder.Entity<Category>().HasMany(x=>x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Product>().Property(x=>x.Price).HasComputedColumnSql("Price*1.18"); // sql tarafında oluşturulacak
            //modelBuilder.Entity<Product>().Property(x=>x.Price).ValueGeneratedOnAdd // identity
            //modelBuilder.Entity<Product>().Property(x => x.Price).ValueGeneratedOnAddOrUpdate // computed
            //modelBuilder.Entity<Product>().Property(x => x.Price).ValueGeneratedNever// none



            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            //ChangeTracker.Entries().ToList().ForEach(e =>  //Memoryde track edilen yani saklanan veriyi döner
            //{
            //    if (e.Entity is Product product)
            //    {
            //        if (e.State == EntityState.Added) // Memoryde state add durumunda ise date ekle
            //        {
            //            product.CreatedDate = DateTime.Now;
            //        }
            //    }
            //});

            return base.SaveChanges();
        }
    }
}
