using Kondongpu.Application.Features.Labs.Commands.Delete;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Labs.Commands;

public class DeleteLabTests : BaseTestFixture
{
    /// ทดสอบ: ลบผลแลปที่มีอยู่ ควรลบออกจากฐานข้อมูลสำเร็จ
    [Test]
    public async Task Delete_ExistingLab_ShouldRemoveFromDatabase()
    {
        RunAsDefaultUser();
        var prereqs = await TestDataFactory.SeedFullCheckupPrerequisitesAsync();
        var id = await TestDataFactory.CreateTestLabAsync(prereqs.CheckupId, prereqs.VisitNumber, prereqs.CheckupItemId);

        await SendAsync(new DeleteLabCommand { Id = id });

        var deleted = await FindAsync<Lab>(id);
        deleted.Should().BeNull();
    }

    /// ทดสอบ: ลบผลแลปที่ไม่มีในระบบ ควร throw NotFoundException
    [Test]
    public async Task Delete_NonExisting_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new DeleteLabCommand { Id = 99999 }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
