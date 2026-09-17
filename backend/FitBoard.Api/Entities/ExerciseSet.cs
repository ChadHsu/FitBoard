namespace FitBoard.Api.Entities;

/// <summary>
/// 組數明細（單一 WorkoutExercise 下的每一組）
/// </summary>
public class ExerciseSet
{
    /// <summary>主鍵</summary>
    public int Id { get; set; }

    /// <summary>所屬訓練動作 FK → WorkoutExercises.Id</summary>
    public int WorkoutExerciseId { get; set; }

    /// <summary>組序，對應 CSV set_index，同動作內從 0 起算</summary>
    public int SetIndex { get; set; }

    /// <summary>組類型，對應 CSV set_type：Warmup / Normal</summary>
    public SetType SetType { get; set; }

    /// <summary>重量（公斤），對應 CSV weight_kg，可為空</summary>
    public decimal? WeightKg { get; set; }

    /// <summary>次數，對應 CSV reps，可為空</summary>
    public int? Reps { get; set; }

    /// <summary>距離（公里），對應 CSV distance_km，有氧等用途，可為空</summary>
    public decimal? DistanceKm { get; set; }

    /// <summary>持續秒數，對應 CSV duration_seconds，可為空</summary>
    public int? DurationSeconds { get; set; }

    /// <summary>自覺強度 RPE（約 1–10），對應 CSV rpe，可為空</summary>
    public decimal? Rpe { get; set; }

    /// <summary>所屬訓練動作（導覽，非 DB 欄位）</summary>
    public WorkoutExercise WorkoutExercise { get; set; } = null!;
}
