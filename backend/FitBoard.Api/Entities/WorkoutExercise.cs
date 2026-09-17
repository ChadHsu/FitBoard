namespace FitBoard.Api.Entities;

/// <summary>
/// 單次訓練中的動作項目（Workout 與 Exercise 的中介）
/// </summary>
public class WorkoutExercise
{
    /// <summary>主鍵</summary>
    public int Id { get; set; }

    /// <summary>所屬訓練 FK → Workouts.Id</summary>
    public int WorkoutId { get; set; }

    /// <summary>動作主檔 FK → Exercises.Id</summary>
    public int ExerciseId { get; set; }

    /// <summary>超組編號，對應 CSV superset_id；同組動作共用同一值，可為空</summary>
    public int? SupersetId { get; set; }

    /// <summary>此動作在該次訓練的備註，對應 CSV exercise_notes，可為空</summary>
    public string? Notes { get; set; }

    /// <summary>此動作在該次訓練中的順序</summary>
    public int SortOrder { get; set; }

    /// <summary>所屬訓練（導覽，非 DB 欄位）</summary>
    public Workout Workout { get; set; } = null!;

    /// <summary>動作主檔（導覽，非 DB 欄位）</summary>
    public Exercise Exercise { get; set; } = null!;

    /// <summary>此動作的組數明細（導覽，非 DB 欄位）</summary>
    public ICollection<ExerciseSet> Sets { get; set; } = [];
}
