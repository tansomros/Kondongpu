using Kondongpu.Application.Features.Recommendations.Queries.Get;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Recommendations.Queries;

public class GetRecommendationListTests : BaseTestFixture
{
    /// ทดสอบ: ดึงรายการคำแนะนำทั้งหมดเมื่อมีข้อมูล ควรคืนรายการที่ไม่ว่างเปล่า
    [Test]
    public async Task GetList_WhenDataExists_ShouldReturnNonEmptyList()
    {
        RunAsDefaultUser();
        await TestDataFactory.CreateTestRecommendationAsync();

        var result = await SendAsync(new GetRecommendationListQuery());

        result.Should().NotBeNull();
        result.Recommendations.Should().NotBeEmpty();
    }

    /// ทดสอบ: ดึงรายการคำแนะนำเมื่อไม่มีข้อมูล ควรคืนรายการว่างเปล่า
    [Test]
    public async Task GetList_WhenNoData_ShouldReturnEmptyList()
    {
        RunAsDefaultUser();

        var result = await SendAsync(new GetRecommendationListQuery());

        result.Should().NotBeNull();
        result.Recommendations.Should().BeEmpty();
    }
}
