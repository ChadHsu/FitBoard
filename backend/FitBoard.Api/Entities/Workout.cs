namespace FitBoard.Api.Entities;

/// <summary>
/// 訓練紀錄（一次完整訓練 session）
/// </summary>
public class Workout
{
    /// <summary>主鍵</summary>
    public int Id { get; set; }

    /// <summary>訓練標題，對應 CSV title，例如 Upper、Arm/Shoulder</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>訓練開始時間，對應 CSV start_time</summary>
    public DateTime StartTime { get; set; }

    /// <summary>訓練結束時間，對應 CSV end_time</summary>
    public DateTime EndTime { get; set; }

    /// <summary>訓練備註，對應 CSV description，可為空</summary>
    public string? Description { get; set; }

    /// <summary>此訓練包含的動作項目（導覽，非 DB 欄位）</summary>
    public ICollection<WorkoutExercise> Exercises { get; set; } = [];
}
