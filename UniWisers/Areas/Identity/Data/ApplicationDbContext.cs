using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniWisers.Areas.Identity.Data;
using UniWisers.Models;
using static System.Formats.Asn1.AsnWriter;

namespace UniWisers.Data;

public class ApplicationDbContext : IdentityDbContext<UniWisersUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<UserPost> UserPosts { get; set; } = default!;
    public DbSet<ReportedPost> ReportedPosts { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<UniWisersUser>
{
    public void Configure(EntityTypeBuilder<UniWisersUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
        builder.Property(u => u.Job).HasMaxLength(255);
        builder.Property(u => u.Country).HasMaxLength(255);
        builder.Property(u => u.ProfilePic).HasMaxLength(255);
        builder.Property(u => u.Status).HasMaxLength(255);
    }
}