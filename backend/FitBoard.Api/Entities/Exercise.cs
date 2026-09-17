namespace FitBoard.Api.Entities;

/// <summary>
/// 動作目錄（跨訓練共用的動作主檔）
/// </summary>
public class Exercise
{
    /// <summary>主鍵</summary>
    public int Id { get; set; }

    /// <summary>動作名稱，對應 CSV exercise_title，唯一，例如 Squat (Barbell)</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>引用此動作的訓練項目（導覽，非 DB 欄位）</summary>
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = [];
}
