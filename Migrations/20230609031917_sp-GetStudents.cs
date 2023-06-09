using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDOperationsEFCore.Migrations
{
    /// <inheritdoc />
    public partial class spGetStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetStudents]
                    @Id int
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select * from Students where Id = @Id
                END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
