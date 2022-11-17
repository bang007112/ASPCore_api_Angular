using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPCore_API.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_ServiceDevices_serviceDevice_id",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Devices_device_id",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceDevices_serviceDevice_id",
                table: "Services");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ServiceDevices");

            migrationBuilder.DropTable(
                name: "AppointmentsService");

            migrationBuilder.DropTable(
                name: "FeedBacks");

            migrationBuilder.DropTable(
                name: "MessagesMedia");

            migrationBuilder.DropIndex(
                name: "IX_Services_serviceDevice_id",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_device_id",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Devices_serviceDevice_id",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "appointmentservice_id",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "serviceDevice_id",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "serviceDevice_id",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "Rooms",
                newName: "RoomType");

            migrationBuilder.CreateTable(
                name: "DeviceService",
                columns: table => new
                {
                    DevicesId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceService", x => new { x.DevicesId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_DeviceService_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_room_id",
                table: "Devices",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceService_ServicesId",
                table: "DeviceService",
                column: "ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Rooms_room_id",
                table: "Devices",
                column: "room_id",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Rooms_room_id",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceService");

            migrationBuilder.DropIndex(
                name: "IX_Devices_room_id",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "Rooms",
                newName: "device_id");

            migrationBuilder.AddColumn<int>(
                name: "appointmentservice_id",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "serviceDevice_id",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "serviceDevice_id",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppointmentsService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointment_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentsService_Services_service_id",
                        column: x => x.service_id,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedBacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AeedbackPoint = table.Column<int>(type: "int", nullable: false),
                    appointment_id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedbackContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessagesMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    device_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    appointmentservice_id = table.Column<int>(type: "int", nullable: false),
                    AppointmentServiceId1 = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedBackId = table.Column<int>(type: "int", nullable: true),
                    feedback_id = table.Column<int>(type: "int", nullable: false),
                    SlotNo = table.Column<int>(type: "int", nullable: false),
                    TotalBill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentsService_AppointmentServiceId1",
                        column: x => x.AppointmentServiceId1,
                        principalTable: "AppointmentsService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_FeedBacks_FeedBackId",
                        column: x => x.FeedBackId,
                        principalTable: "FeedBacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: true),
                    messageMedia_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_MessagesMedia_MediaId",
                        column: x => x.MediaId,
                        principalTable: "MessagesMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_serviceDevice_id",
                table: "Services",
                column: "serviceDevice_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_device_id",
                table: "Rooms",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_serviceDevice_id",
                table: "Devices",
                column: "serviceDevice_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentServiceId1",
                table: "Appointments",
                column: "AppointmentServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_FeedBackId",
                table: "Appointments",
                column: "FeedBackId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsService_service_id",
                table: "AppointmentsService",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MediaId",
                table: "Messages",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_ServiceDevices_serviceDevice_id",
                table: "Devices",
                column: "serviceDevice_id",
                principalTable: "ServiceDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Devices_device_id",
                table: "Rooms",
                column: "device_id",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceDevices_serviceDevice_id",
                table: "Services",
                column: "serviceDevice_id",
                principalTable: "ServiceDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
