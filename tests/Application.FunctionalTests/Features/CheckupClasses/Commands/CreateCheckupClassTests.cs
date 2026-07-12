using Kondongpu.Application.Features.CheckupClasses.Commands.Create;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;
using ValidationException = Kondongpu.Application.Exceptions.ValidationException;

namespace Kondongpu.Application.FunctionalTests.Features.CheckupClasses.Commands;

public class CreateCheckupClassTests : BaseTestFixture
{
    /// ทดสอบ: สร้างหมวดตรวจสุขภาพใหม่ด้วยข้อมูลที่ถูกต้อง ควรคืนค่า Id
    [Test]
    public async Task Create_WithValidData_ShouldReturnId()
    {
        RunAsDefaultUser();

        var command = new CreateCheckupClassCommand
        {
            Id = 9001,
            Code = "LAB",
            Name = "ผลตรวจแลป",
            Sort = 1
        };

        var result = await SendAsync(command);
        result.Should().Be(9001);
    }

    /// ทดสอบ: สร้างหมวดตรวจสุขภาพแล้วตรวจสอบข้อมูลที่บันทึก
    [Test]
    public async Task Create_WithValidData_ShouldPersistEntity()
    {
        RunAsDefaultUser();

        var id = await SendAsync(new CreateCheckupClassCommand
        {
            Id = 9002,
            Code = "XRAY",
            Name = "เอกซเรย์",
            Sort = 2
        });

        var entity = await FindAsync<CheckupClass>(id);
        entity.Should().NotBeNull();
        entity!.Code.Should().Be("XRAY");
        entity.Name.Should().Be("เอกซเรย์");
    }

    /// ทดสอบ: สร้างหมวดตรวจโดยไม่ระบุรหัส ควร throw ValidationException
    [Test]
    public async Task Create_WithEmptyCode_ShouldThrowValidationException()
    {
        RunAsDefaultUser();

        var command = new CreateCheckupClassCommand
        {
            Id = 9003,
            Code = "",
            Name = "ทดสอบ",
            Sort = 1
        };

        var act = () => SendAsync(command);
        (await act.Should().ThrowAsync<ValidationException>())
            .Which.Errors.Should().ContainKey("Code")
            .WhoseValue.Should().Contain("Code ต้องไม่ว่าง");
    }
}
