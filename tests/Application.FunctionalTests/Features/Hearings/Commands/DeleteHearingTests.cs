using Kondongpu.Application.Features.Hearings.Commands.Delete;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Hearings.Commands;

public class DeleteHearingTests : BaseTestFixture
{
    /// ทดสอบ: ลบข้อมูลการตรวจการได้ยินที่มีอยู่ ควรลบสำเร็จ
    [Test]
    public async Task Delete_Existing_ShouldRemoveFromDatabase()
    {
        RunAsDefaultUser();
        var prereqs = await TestDataFactory.SeedFullCheckupPrerequisitesAsync();
        var audiogramId = await TestDataFactory.CreateTestAudiogramAsync(prereqs.CheckupId, prereqs.CheckupItemId, prereqs.VisitNumber);
        var id = await TestDataFactory.CreateTestHearingAsync(audiogramId, 500);

        await SendAsync(new DeleteHearingCommand { Id = id });

        var deleted = await FindAsync<Hearing>(id);
        deleted.Should().BeNull();
    }

    /// ทดสอบ: ลบข้อมูลการตรวจการได้ยินที่ไม่มี ควร throw NotFoundException
    [Test]
    public async Task Delete_NonExisting_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new DeleteHearingCommand { Id = 99999 }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
