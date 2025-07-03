using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;

public  class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        // Seed Devices
          builder.HasData(
            new Device { DeviceId = Guid.Parse("f0e831cf-1223-4344-a8ff-2505a905c2b7"), DeviceName = "Thermostat", DeviceType = "Temperature", Status = "Active", HouseId = Guid.Parse("d3b8a8f1-4c3b-4b8a-9c3b-4b8a9c3b4b8a") },
            new Device { DeviceId = Guid.Parse("d017b91b-f958-46ba-be9b-c95a3769e217"), DeviceName = "Security Camera", DeviceType = "Camera", Status = "Active", HouseId = Guid.Parse("e4c9b9f2-5d4c-5c9b-8d4c-5c9b8d4c5c9b") },
            new Device { DeviceId = Guid.Parse("dd7c82c9-8b51-45a3-b470-d48a24096f34"), DeviceName = "Smart Lock", DeviceType = "Access Control", Status = "Inactive", HouseId = Guid.Parse("2fa0c930-3bdf-40a4-8d47-9ef458520c76") },
            new Device { DeviceId = Guid.Parse("7ba4c562-ab18-4b66-99d2-57cd0831a532"), DeviceName = "Smoke Detector", DeviceType = "Safety", Status = "Active", HouseId = Guid.Parse("95374554-afab-467f-b689-d53dc1ea92a5") },
            new Device { DeviceId = Guid.Parse("1511169f-8e5f-4847-b218-96bb8ee19ab5"), DeviceName = "Light Bulb", DeviceType = "Lighting", Status = "Active", HouseId = Guid.Parse("a05d91e7-556c-480b-88ab-d9c5da38bea0") },
            new Device { DeviceId = Guid.Parse("4a2d73b3-e951-403a-9149-4dd97db6cd2c"), DeviceName = "Water Leak Sensor", DeviceType = "Safety", Status = "Inactive", HouseId = Guid.Parse("16a86fdb-2c84-4302-824e-a49c00e4369e") },
            new Device { DeviceId = Guid.Parse("cb5bda8f-a6c4-4fbc-91b1-28a7bbb927f8"), DeviceName = "Motion Sensor", DeviceType = "Security", Status = "Active", HouseId = Guid.Parse("57445249-2a18-4552-a929-d9e5949a7fb4") },
            new Device { DeviceId = Guid.Parse("aff910da-bd8c-4dc5-9d04-a289a6c8e036"), DeviceName = "Smart Doorbell", DeviceType = "Access Control", Status = "Active", HouseId = Guid.Parse("6963f81e-16b2-4168-84aa-49c65c7ec2fc") },
            new Device { DeviceId = Guid.Parse("20d87e50-c4b7-4a48-982c-1b6c5c869c2c"), DeviceName = "Garage Door Opener", DeviceType = "Convenience", Status = "Inactive", HouseId = Guid.Parse("648c36e6-cf4a-4686-a26e-a5b2605491c4") },
            new Device { DeviceId = Guid.Parse("6270a75e-865d-4983-9f77-910aff4a7f57"), DeviceName = "Solar Panel Monitor", DeviceType = "Energy", Status = "Active", HouseId = Guid.Parse("0762f142-6ccb-4166-a049-f6c289447e6d") }
        );


        // Custom constraints
        builder.Property(d => d.DeviceName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.DeviceType)
            .IsRequired()
            .HasMaxLength(50);
    }
}
