
using Domain.ReturnItemRequests;
using Domain.ReturnItemRequests.Attachments;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ReturnItemRequestAttachmentConfig : BaseEntityConfig<ReturnItemRequestAttachment>
{
    public override void Configure(EntityTypeBuilder<ReturnItemRequestAttachment> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new ReturnItemRequestAttachmentId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.FileName)
               .HasColumnType("VARCHAR(40)")
               .IsRequired();

        builder.HasOne<ReturnItemRequest>()
               .WithMany(x => x.Attachments)
               .HasForeignKey(x => x.ReturnItemRequestId)
               .IsRequired();

        builder.ToTable("ReturnItemRequestAttachments");
    }
}
