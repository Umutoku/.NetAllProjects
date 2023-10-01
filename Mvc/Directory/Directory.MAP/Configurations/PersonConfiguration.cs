using Directory.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Directory.MAP.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        //firstname kolonu zorunlu ve max 15 karakter
        //telefon kolonu zorunlu ve max 15 karakter
        //lastname max 15 karakter
        //company max 15 karakter
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(15);
            builder.Property(x => x.LastName).HasMaxLength(15);
            builder.Property(x=>x.PhoneNumber).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Company).HasMaxLength(15);
        }
    }
}
