using Kondongpu.Application.Features.HearingHertzs.Commands.Delete;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.HearingHertzs.Commands;

public class DeleteHearingHertzTests : BaseTestFixture
{
    /// ทดสอบ: ลบความถี่การได้ยินที่มีอยู่ ควรลบออกจากฐานข้อมูลสำเร็จ
    [Test]
    public async Task Delete_ExistingHertz_ShouldRemoveFromDatabase()
    {
        RunAsDefaultUser();
        var id = await TestDataFactory.CreateTestHearingHertzAsync(2000);

        await SendAsync(new DeleteHearingHertzCommand { Id = id });

        var deleted = await FindAsync<HearingHertz>(id);
        deleted.Should().BeNull();
    }

    /// ทดสอบ: ลบความถี่การได้ยินที่ไม่มีในระบบ ควร throw NotFoundException
    [Test]
    public async Task Delete_NonExisting_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new DeleteHearingHertzCommand { Id = 99999 }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
