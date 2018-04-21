using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class UserInfoConfiguration : EntityCommonConfiguration<UserInfo>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<UserInfo> entity)
        {
            entity.ToTable("UserInfos");

            entity.HasKey(user => user.Id);

            entity.HasIndex(user => user.UserName)
                .IsUnique();

            entity.Property(user => user.Avatar)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(user => user.UserName)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(user => user.Role)
                .IsRequired();

            entity.HasOne(user => user.Staff)
                 .WithOne(staff => staff.UserInfo)
                 .HasForeignKey<Staff>(staff => staff.UserInfoId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
