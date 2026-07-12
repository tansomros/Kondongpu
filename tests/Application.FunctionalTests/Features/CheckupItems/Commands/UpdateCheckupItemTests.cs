using Kondongpu.Application.Features.CheckupItems.Commands.Update;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;
using ValidationException = Kondongpu.Application.Exceptions.ValidationException;

namespace Kondongpu.Application.FunctionalTests.Features.CheckupItems.Commands;

public class UpdateCheckupItemTests : BaseTestFixture
{
    /// ทดสอบ: อัปเดตรายการตรวจที่มีอยู่ ควรบันทึกข้อมูลใหม่สำเร็จ
    [Test]
    public async Task Update_ExistingItem_ShouldUpdateFields()
    {
        RunAsDefaultUser();
        var classId = await TestDataFactory.CreateTestCheckupClassAsync();
        var groupId = await TestDataFactory.CreateTestCheckupGroupAsync(classId);
        var itemId = await TestDataFactory.CreateTestCheckupItemAsync(groupId);

        await SendAsync(new UpdateCheckupItemCommand
        {
            Id = itemId,
            Code = "UPDATED",
            DisplayName = "Updated Item",
            CheckupGroupId = groupId,
            Sort = 10
        });

        var updated = await FindAsync<CheckupItem>(itemId);
        updated.Should().NotBeNull();
        updated!.Code.Should().Be("UPDATED");
        updated.DisplayName.Should().Be("Updated Item");
    }

    /// ทดสอบ: อัปเดตรายการตรวจที่ไม่มีในระบบ ควร throw ValidationException เนื่องจาก validator ตรวจสอบการมีอยู่ของข้อมูล
    [Test]
    public async Task Update_NonExisting_ShouldThrowValidationException()
    {
        RunAsDefaultUser();

        var act = () => SendAsync(new UpdateCheckupItemCommand
        {
            Id = 99999,
            Code = "X",
            DisplayName = "X",
            CheckupGroupId = 1,
            Sort = 1
        });

        (await act.Should().ThrowAsync<ValidationException>())
            .Which.Errors.Should().ContainKey("Id")
            .WhoseValue.Should().Contain("ไม่พบข้อมูล");
    }
}
