using Kondongpu.Application.Features.CheckupItems.Queries.Get;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.CheckupItems.Queries;

public class GetCheckupItemTests : BaseTestFixture
{
    /// ทดสอบ: ค้นหารายการตรวจด้วย Id ที่มีอยู่ ควรคืนข้อมูลที่ถูกต้อง
    [Test]
    public async Task Get_ExistingId_ShouldReturnViewModel()
    {
        RunAsDefaultUser();
        var classId = await TestDataFactory.CreateTestCheckupClassAsync();
        var groupId = await TestDataFactory.CreateTestCheckupGroupAsync(classId);
        var itemId = await TestDataFactory.CreateTestCheckupItemAsync(groupId);

        var result = await SendAsync(new GetCheckupItemQuery { Id = itemId });

        result.Should().NotBeNull();
        result.Id.Should().Be(itemId);
    }

    /// ทดสอบ: ค้นหารายการตรวจด้วย Id ที่ไม่มี ควร throw NotFoundException
    [Test]
    public async Task Get_NonExisting_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new GetCheckupItemQuery { Id = 99999 }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
