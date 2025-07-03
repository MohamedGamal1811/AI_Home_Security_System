using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;

 
    public class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            // Seed Houses
            builder.HasData(
                new House { HouseId = Guid.Parse("d3b8a8f1-4c3b-4b8a-9c3b-4b8a9c3b4b8a"), Name = "Villa Oasis", Address = "123 Palm Street", City = "Cairo", State = "Cairo Governorate", ZipCode = "12345", Country = "Egypt", Description = "Luxurious villa with pool" },
                new House { HouseId = Guid.Parse("e4c9b9f2-5d4c-5c9b-8d4c-5c9b8d4c5c9b"), Name = "Sunset Bungalow", Address = "456 Beach Avenue", City = "Alexandria", State = "Alexandria Governorate", ZipCode = "54321", Country = "Egypt", Description = "Cozy bungalow near the sea" },
                new House { HouseId = Guid.Parse("2fa0c930-3bdf-40a4-8d47-9ef458520c76"), Name = "Urban Apartment", Address = "789 City Center", City = "Giza", State = "Giza Governorate", ZipCode = "67890", Country = "Egypt", Description = "Modern apartment in downtown" },
                new House { HouseId = Guid.Parse("95374554-afab-467f-b689-d53dc1ea92a5"), Name = "Country Cottage", Address = "101 Farm Road", City = "Fayoum", State = "Fayoum Governorate", ZipCode = "11223", Country = "Egypt", Description = "Peaceful retreat in the countryside" },
                new House { HouseId = Guid.Parse("a05d91e7-556c-480b-88ab-d9c5da38bea0"), Name = "Mountain Lodge", Address = "202 Highland Blvd", City = "Aswan", State = "Aswan Governorate", ZipCode = "44556", Country = "Egypt", Description = "Rustic lodge with scenic views" },
                new House { HouseId = Guid.Parse("16a86fdb-2c84-4302-824e-a49c00e4369e"), Name = "Desert Villa", Address = "303 Sand Dunes Way", City = "Siwa", State = "Matrouh Governorate", ZipCode = "99887", Country = "Egypt", Description = "Elegant villa in the desert oasis" },
                new House { HouseId = Guid.Parse("57445249-2a18-4552-a929-d9e5949a7fb4"), Name = "Lake House", Address = "404 Shoreline Ave", City = "Luxor", State = "Luxor Governorate", ZipCode = "77665", Country = "Egypt", Description = "Charming house on the lake" },
                new House { HouseId = Guid.Parse("6963f81e-16b2-4168-84aa-49c65c7ec2fc"), Name = "Historic Manor", Address = "505 Heritage St", City = "Minya", State = "Minya Governorate", ZipCode = "33445", Country = "Egypt", Description = "Grand manor with rich history" },
                new House { HouseId = Guid.Parse("648c36e6-cf4a-4686-a26e-a5b2605491c4"), Name = "Seaside Villa", Address = "606 Oceanview Dr", City = "Hurghada", State = "Red Sea Governorate", ZipCode = "22111", Country = "Egypt", Description = "Spacious villa by the sea" },
                new House { HouseId = Guid.Parse("0762f142-6ccb-4166-a049-f6c289447e6d"), Name = "Cliffside Cabin", Address = "707 Rocky Path", City = "Marsa Alam", State = "Red Sea Governorate", ZipCode = "99200", Country = "Egypt", Description = "Secluded cabin on a cliff" }
            );

            // Custom constraints
            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Address)
                .IsRequired()
                .HasMaxLength(200);
        }
    }


 
