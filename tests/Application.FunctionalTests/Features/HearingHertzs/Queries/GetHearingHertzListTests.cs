using Kondongpu.Application.Features.HearingHertzs.Queries.Get;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.HearingHertzs.Queries;

public class GetHearingHertzListTests : BaseTestFixture
{
    /// ทดสอบ: ดึงรายการความถี่การได้ยินเมื่อมีข้อมูล ควรคืนรายการที่ไม่ว่างเปล่า
    [Test]
    public async Task GetList_WhenDataExists_ShouldReturnNonEmptyList()
    {
        RunAsDefaultUser();
        await TestDataFactory.CreateTestHearingHertzAsync(500);
        await TestDataFactory.CreateTestHearingHertzAsync(1000);

        var result = await SendAsync(new GetHearingHertzListQuery());

        result.Should().NotBeNull();
        result.Items.Should().HaveCountGreaterThanOrEqualTo(2);
    }

    /// ทดสอบ: ดึงรายการความถี่การได้ยินเมื่อไม่มีข้อมูล ควรคืนรายการว่างเปล่า
    [Test]
    public async Task GetList_WhenNoData_ShouldReturnEmptyList()
    {
        RunAsDefaultUser();

        var result = await SendAsync(new GetHearingHertzListQuery());

        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
    }
}
