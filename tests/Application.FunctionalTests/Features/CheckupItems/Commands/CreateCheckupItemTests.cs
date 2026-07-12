using Kondongpu.Application.Features.CheckupItems.Commands.Create;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.CheckupItems.Commands;

public class CreateCheckupItemTests : BaseTestFixture
{
    /// ทดสอบ: สร้างรายการตรวจใหม่ด้วยข้อมูลที่ถูกต้อง ควรคืนค่า Id
    [Test]
    public async Task Create_WithValidData_ShouldReturnId()
    {
        RunAsDefaultUser();
        var classId = await TestDataFactory.CreateTestCheckupClassAsync();
        var groupId = await TestDataFactory.CreateTestCheckupGroupAsync(classId);

        var command = new CreateCheckupItemCommand
        {
            Id = 9001,
            Code = "FBS",
            DisplayName = "Fasting Blood Sugar",
            CheckupGroupId = groupId,
            Sort = 1,
            IsDisplayPrint = true
        };

        var result = await SendAsync(command);
        result.Should().Be(9001);
    }

    /// ทดสอบ: สร้างรายการตรวจแล้วตรวจสอบข้อมูลที่บันทึก
    [Test]
    public async Task Create_WithValidData_ShouldPersistEntity()
    {
        RunAsDefaultUser();
        var classId = await TestDataFactory.CreateTestCheckupClassAsync();
        var groupId = await TestDataFactory.CreateTestCheckupGroupAsync(classId);

        var id = await SendAsync(new CreateCheckupItemCommand
        {
            Id = 9002,
            Code = "HB",
            DisplayName = "Hemoglobin",
            CheckupGroupId = groupId,
            Sort = 2,
            IsDisplayPrint = true
        });

        var entity = await FindAsync<CheckupItem>(id);
        entity.Should().NotBeNull();
        entity!.Code.Should().Be("HB");
        entity.DisplayName.Should().Be("Hemoglobin");
    }
}
