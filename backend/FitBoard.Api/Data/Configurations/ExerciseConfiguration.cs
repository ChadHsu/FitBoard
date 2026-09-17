using FitBoard.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitBoard.Api.Data.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises", t => t.HasComment("動作目錄（跨訓練共用的動作主檔）"));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasComment("主鍵");

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("動作名稱，對應 CSV exercise_title，唯一，例如 Squat (Barbell)");

        builder.HasIndex(x => x.Title)
            .IsUnique();
    }
}
