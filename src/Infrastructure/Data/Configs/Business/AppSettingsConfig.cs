
using Domain.AppSettings;

namespace Infrastructure.Data.Configs.Business;

internal sealed class AppSettingsConfig : BaseEntityConfig<AppSettings>
{
    public override void Configure(EntityTypeBuilder<AppSettings> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Key);

        builder.HasKey(x => x.Id).HasName("Key");

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new AppSettingsId(value))
               .HasColumnType("VARCHAR(100)")
               .ValueGeneratedNever();



        builder.Property(x => x.Value)
               .HasColumnType("NVARCHAR(1000)")
               .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("NVARCHAR(1000)")
            .IsRequired();

        builder.ToTable("AppSettings");

    }
}
