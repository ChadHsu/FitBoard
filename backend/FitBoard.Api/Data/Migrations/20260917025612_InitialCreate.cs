using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitBoard.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false, comment: "主鍵")
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "動作名稱，對應 CSV exercise_title，唯一，例如 Squat (Barbell)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                },
                comment: "動作目錄（跨訓練共用的動作主檔）");

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false, comment: "主鍵")
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "訓練標題，對應 CSV title，例如 Upper、Arm/Shoulder"),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "訓練開始時間，對應 CSV start_time"),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "訓練結束時間，對應 CSV end_time"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true, comment: "訓練備註，對應 CSV description，可為空")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                },
                comment: "訓練紀錄（一次完整訓練 session）");

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false, comment: "主鍵")
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkoutId = table.Column<int>(type: "INTEGER", nullable: false, comment: "所屬訓練 FK → Workouts.Id"),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false, comment: "動作主檔 FK → Exercises.Id"),
                    SupersetId = table.Column<int>(type: "INTEGER", nullable: true, comment: "超組編號，對應 CSV superset_id；同組動作共用同一值，可為空"),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true, comment: "此動作在該次訓練的備註，對應 CSV exercise_notes，可為空"),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false, comment: "此動作在該次訓練中的順序（由匯入順序或手動指定）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "單次訓練中的動作項目（Workout 與 Exercise 的中介）");

            migrationBuilder.CreateTable(
                name: "ExerciseSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false, comment: "主鍵")
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkoutExerciseId = table.Column<int>(type: "INTEGER", nullable: false, comment: "所屬訓練動作 FK → WorkoutExercises.Id"),
                    SetIndex = table.Column<int>(type: "INTEGER", nullable: false, comment: "組序，對應 CSV set_index，同動作內從 0 起算"),
                    SetType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, comment: "組類型，對應 CSV set_type：Warmup / Normal"),
                    WeightKg = table.Column<decimal>(type: "TEXT", precision: 8, scale: 2, nullable: true, comment: "重量（公斤），對應 CSV weight_kg，可為空"),
                    Reps = table.Column<int>(type: "INTEGER", nullable: true, comment: "次數，對應 CSV reps，可為空"),
                    DistanceKm = table.Column<decimal>(type: "TEXT", precision: 8, scale: 3, nullable: true, comment: "距離（公里），對應 CSV distance_km，有氧等用途，可為空"),
                    DurationSeconds = table.Column<int>(type: "INTEGER", nullable: true, comment: "持續秒數，對應 CSV duration_seconds，可為空"),
                    Rpe = table.Column<decimal>(type: "TEXT", precision: 4, scale: 1, nullable: true, comment: "自覺強度 RPE（約 1–10），對應 CSV rpe，可為空")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSets_WorkoutExercises_WorkoutExerciseId",
                        column: x => x.WorkoutExerciseId,
                        principalTable: "WorkoutExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "組數明細（單一 WorkoutExercise 下的每一組）");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_Title",
                table: "Exercises",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSets_WorkoutExerciseId_SetIndex",
                table: "ExerciseSets",
                columns: new[] { "WorkoutExerciseId", "SetIndex" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutId_SortOrder",
                table: "WorkoutExercises",
                columns: new[] { "WorkoutId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_StartTime",
                table: "Workouts",
                column: "StartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSets");

            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Workouts");
        }
    }
}
