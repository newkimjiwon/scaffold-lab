using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreScaffoldLab.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                type: "TEXT",
                nullable: true);

            //migrationBuilder.Sql("UPDATE Users SET Gender = 'Unknown' WHERE Gender IS NULL");

            // 추가된 gender 컬럼에 기본값을 설정하는 SQL 쿼리를 실행하여 기존 데이터에 대해 gender 값을 채워줍니다.
             migrationBuilder.Sql("UPDATE Users SET Gender = 'Unknown' WHERE Gender IS NULL");

            // gender 컬럼을 not null 변경
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");
        }
    }
}
