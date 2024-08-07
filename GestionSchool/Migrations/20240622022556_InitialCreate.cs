using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionSchool.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CodigoCurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Jornada = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cursos__BB0F2318123E0EA9", x => x.CodigoCurso);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    DocumentoIdentidad = table.Column<int>(type: "int", nullable: false),
                    Nombres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    ImagenUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estudian__049E81A881993628", x => x.DocumentoIdentidad);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    DocumentoIdentidad = table.Column<int>(type: "int", nullable: false),
                    Nombres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Profesion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ImagenUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Profesor__049E81A816F39792", x => x.DocumentoIdentidad);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    CodigoMatricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoIdentidadEstudiante = table.Column<int>(type: "int", nullable: true),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Matricul__4DF06615C0D739A8", x => x.CodigoMatricula);
                    table.ForeignKey(
                        name: "FK__Matricula__Docum__3E52440B",
                        column: x => x.DocumentoIdentidadEstudiante,
                        principalTable: "Estudiantes",
                        principalColumn: "DocumentoIdentidad");
                });

            migrationBuilder.CreateTable(
                name: "ProfesorCursos",
                columns: table => new
                {
                    DocumentoIdentidadProfesor = table.Column<int>(type: "int", nullable: false),
                    CodigoCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Profesor__12F1DDC77F77F806", x => new { x.DocumentoIdentidadProfesor, x.CodigoCurso });
                    table.ForeignKey(
                        name: "FK__Profesore__Codig__4222D4EF",
                        column: x => x.CodigoCurso,
                        principalTable: "Cursos",
                        principalColumn: "CodigoCurso");
                    table.ForeignKey(
                        name: "FK__Profesore__Docum__412EB0B6",
                        column: x => x.DocumentoIdentidadProfesor,
                        principalTable: "Profesores",
                        principalColumn: "DocumentoIdentidad");
                });

            migrationBuilder.CreateTable(
                name: "MatriculaCursos",
                columns: table => new
                {
                    CodigoMatricula = table.Column<int>(type: "int", nullable: false),
                    CodigoCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Matricul__D6409424BB8FA408", x => new { x.CodigoMatricula, x.CodigoCurso });
                    table.ForeignKey(
                        name: "FK__Matricula__Codig__44FF419A",
                        column: x => x.CodigoMatricula,
                        principalTable: "Matriculas",
                        principalColumn: "CodigoMatricula");
                    table.ForeignKey(
                        name: "FK__Matricula__Codig__45F365D3",
                        column: x => x.CodigoCurso,
                        principalTable: "Cursos",
                        principalColumn: "CodigoCurso");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatriculasCursos_CodigoCurso",
                table: "MatriculaCursos",
                column: "CodigoCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_DocumentoIdentidadEstudiante",
                table: "Matriculas",
                column: "DocumentoIdentidadEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoresCursos_CodigoCurso",
                table: "ProfesorCursos",
                column: "CodigoCurso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "MatriculaCursos");

            migrationBuilder.DropTable(
                name: "ProfesorCursos");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Estudiantes");
        }
    }
}
