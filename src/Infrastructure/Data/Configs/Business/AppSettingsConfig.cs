

using Application.Common.AppSettingsConfiguration;

namespace Infrastructure.Data.Configs.Business;

internal sealed class AppSettingsConfig : IEntityTypeConfiguration<AppSettings>
{
    public void Configure(EntityTypeBuilder<AppSettings> builder)
    {

        builder.HasKey(x => x.Key)
               .IsClustered(false);

        builder.Property(x => x.Key)
               .HasColumnType("VARCHAR(100)")
               .ValueGeneratedNever();

        builder.Property(x => x.Value)
               .HasColumnType("NVARCHAR(1000)")
               .IsRequired();

        builder.ToTable("AppSettings");
    }
}
