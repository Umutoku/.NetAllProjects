using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyEFCore.CodeFirst.Models;

namespace UdemyEFCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Person> People { get; set; }

        //Eğer buraya miras alınan sınıfı eklersek bütün alt sınıflarını kendi tablosunda toplar
        //public DbSet<BasePerson> People { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        //alt taraf migrationa eklenmeyecek
        public DbSet<ProductEssential> ProductEssentials { get; set; }
        public DbSet<ProductFull> ProductFulls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            //laszy loading kontrolü, information logları kontrolü // console yazdırma
            optionsBuilder.LogTo(Console.WriteLine,Microsoft.Extensions.Logging.LogLevel.Information).UseLazyLoadingProxies().UseSqlServer(Initializer.Configuration.GetConnectionString("SqlServer"));
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

            //modelBuilder.Entity<Product>().Property(x=>x.Price).HasPrecision(18,2); //toplam 18 karakter, virgülden sonra iki karakter


            //TPT tipe göre tablo oluşturma
            //modelBuilder.Entity<BasePerson>().ToTable("People");
            //modelBuilder.Entity<Manager>().ToTable("Managers");
            //modelBuilder.Entity<Employee>().ToTable("Employees");

            //fluent api ile owned type oluşturma
            //modelBuilder.Entity<Employee>().OwnsOne(x => x.BasePerson);

            //fluent api ile keyless type oluşturma
            //modelBuilder.Entity<ProductFull>().HasNoKey();

            //fluent api ile notmapped
            //modelBuilder.Entity<Product>().Ignore(x => x.Barcode);

            //fluent api ile unicode
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsUnicode(false); // varchar

            //fluent api columntype
            //modelBuilder.Entity<Product>().Property(x => x.Url).HasColumnType("varchar(100)");

            //fluent api ile index oluşturma
            //modelBuilder.Entity<Product>().HasIndex(Product => new { Product.Name, Product.Price }).IsUnique();

            //fluent api ile property içeren index oluşturma
            //eğer name ile sorgu yaptığımda en çok price kullanıyorsam
            //modelBuilder.Entity<Product>().HasIndex(Product => new { Product.Name}).IncludeProperties(x=>x.Price);

            //fluent api ile unique index oluşturma// sql tarafında kurak ile kayıt
            //modelBuilder.Entity<Product>().HasCheckConstraint("PriceDiscountCheck", "[Price]>[DiscountPrice]");


            //custom sql sorgusu ile veri çekme
            //modelBuilder.Entity<Product>().HasNoKey().ToSqlQuery("Select Name, Price From Products");

            //eğer sql üzerindeki view çekmek istiyorsak
            modelBuilder.Entity<ProductFull>().HasNoKey().ToView("Productwithfeature");

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
