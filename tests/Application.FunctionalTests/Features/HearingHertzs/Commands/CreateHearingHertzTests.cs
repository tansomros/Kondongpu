using Kondongpu.Application.Features.HearingHertzs.Commands.Create;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.HearingHertzs.Commands;

public class CreateHearingHertzTests : BaseTestFixture
{
    /// ทดสอบ: สร้างความถี่การได้ยินใหม่ ควรคืนค่า Id ที่มากกว่า 0
    [Test]
    public async Task Create_WithValidHertz_ShouldReturnPositiveId()
    {
        RunAsDefaultUser();

        var command = new CreateHeringHertzCommand { Hertz = 500 };

        var result = await SendAsync(command);

        result.Should().BeGreaterThan(0);
    }

    /// ทดสอบ: สร้างความถี่การได้ยินแล้วตรวจสอบว่าข้อมูลถูกบันทึกอย่างถูกต้อง
    [Test]
    public async Task Create_WithValidHertz_ShouldPersistEntity()
    {
        RunAsDefaultUser();

        var id = await SendAsync(new CreateHeringHertzCommand { Hertz = 1000 });

        var entity = await FindAsync<HearingHertz>(id);
        entity.Should().NotBeNull();
        entity!.Hertz.Should().Be(1000);
    }
}
