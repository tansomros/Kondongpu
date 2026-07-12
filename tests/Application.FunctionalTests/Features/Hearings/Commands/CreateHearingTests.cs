using Kondongpu.Application.Features.Hearings.Commands.Create;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Hearings.Commands;

public class CreateHearingTests : BaseTestFixture
{
    /// ทดสอบ: สร้างข้อมูลการตรวจการได้ยินใหม่ ควรคืนค่า Id ที่มากกว่า 0
    [Test]
    public async Task Create_WithValidData_ShouldReturnPositiveId()
    {
        RunAsDefaultUser();
        var prereqs = await TestDataFactory.SeedFullCheckupPrerequisitesAsync();
        var audiogramId = await TestDataFactory.CreateTestAudiogramAsync(prereqs.CheckupId, prereqs.CheckupItemId, prereqs.VisitNumber);

        var command = new CreateHearingCommand
        {
            AudiogramId = audiogramId,
            Hertz = 500,
            LeftHz = 20.0,
            RightHz = 25.0
        };

        var result = await SendAsync(command);
        result.Should().BeGreaterThan(0);
    }

    /// ทดสอบ: สร้างข้อมูลการตรวจการได้ยินแล้วตรวจสอบข้อมูลที่บันทึก
    [Test]
    public async Task Create_WithValidData_ShouldPersistEntity()
    {
        RunAsDefaultUser();
        var prereqs = await TestDataFactory.SeedFullCheckupPrerequisitesAsync();
        var audiogramId = await TestDataFactory.CreateTestAudiogramAsync(prereqs.CheckupId, prereqs.CheckupItemId, prereqs.VisitNumber);

        var id = await SendAsync(new CreateHearingCommand
        {
            AudiogramId = audiogramId,
            Hertz = 1000,
            LeftHz = 15.0,
            RightHz = 20.0
        });

        var entity = await FindAsync<Hearing>(id);
        entity.Should().NotBeNull();
        entity!.Hertz.Should().Be(1000);
    }
}
