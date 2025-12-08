using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VladimirNyaninKT_31_22.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "degrees",
                columns: table => new
                {
                    degree_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор учёной степени")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    degree_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название учёной степени"),
                    is_deleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_degrees_degree_id", x => x.degree_id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    department_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор кафедры")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    department_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название кафедры"),
                    is_deleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments_department_id", x => x.department_id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    position_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор должности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название должности"),
                    is_deleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions_position_id", x => x.position_id);
                });

            migrationBuilder.CreateTable(
                name: "workloads",
                columns: table => new
                {
                    workload_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор загруженности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hours_quantity = table.Column<int>(type: "int4", nullable: false, comment: "Загруженность в часах"),
                    is_deleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workloads_workload_id", x => x.workload_id);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    teacher_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор преподавателя")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    teacher_last_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия преподавателя"),
                    teacher_first_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя преподавателя"),
                    teacher_patronymic = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Отчество преподавателя"),
                    is_head = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак заведующего кафедрой"),
                    is_deleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак удаления"),
                    DegreeId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teachers_teacher_id", x => x.teacher_id);
                    table.ForeignKey(
                        name: "fk_teachers_degree_id",
                        column: x => x.DegreeId,
                        principalTable: "degrees",
                        principalColumn: "degree_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teachers_department_id",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teachers_position_id",
                        column: x => x.PositionId,
                        principalTable: "positions",
                        principalColumn: "position_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    subject_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название дисциплины"),
                    is_deleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак удаления"),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    WorkloadId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subjects_subject_id", x => x.subject_id);
                    table.ForeignKey(
                        name: "fk_subjects_teacher_id",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subjects_workload_id",
                        column: x => x.WorkloadId,
                        principalTable: "workloads",
                        principalColumn: "workload_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_degrees_is_deleted",
                table: "degrees",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "uk_degrees_degree_name",
                table: "degrees",
                column: "degree_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_departments_is_deleted",
                table: "departments",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "uk_departments_department_name",
                table: "departments",
                column: "department_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_positions_is_deleted",
                table: "positions",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "uk_positions_position_name",
                table: "positions",
                column: "position_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_subjects_is_deleted",
                table: "subjects",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_TeacherId",
                table: "subjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_WorkloadId",
                table: "subjects",
                column: "WorkloadId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_DegreeId",
                table: "teachers",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_DepartmentId",
                table: "teachers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "ix_teachers_is_deleted",
                table: "teachers",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_teachers_is_head",
                table: "teachers",
                column: "is_head");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_PositionId",
                table: "teachers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "ix_teachers_teacher_first_name",
                table: "teachers",
                column: "teacher_first_name");

            migrationBuilder.CreateIndex(
                name: "ix_teachers_teacher_last_name",
                table: "teachers",
                column: "teacher_last_name");

            migrationBuilder.CreateIndex(
                name: "ix_teachers_teacher_patronymic",
                table: "teachers",
                column: "teacher_patronymic");

            migrationBuilder.CreateIndex(
                name: "ix_workloads_is_deleted",
                table: "workloads",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "uk_workloads_hours_quantity",
                table: "workloads",
                column: "hours_quantity",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "workloads");

            migrationBuilder.DropTable(
                name: "degrees");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "positions");
        }
    }
}
