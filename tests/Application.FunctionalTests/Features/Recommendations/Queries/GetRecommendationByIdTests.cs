using Kondongpu.Application.Features.Recommendations.Queries.Get;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Recommendations.Queries;

public class GetRecommendationByIdTests : BaseTestFixture
{
    /// ทดสอบ: ค้นหาคำแนะนำด้วย Id ที่มีอยู่ ควรคืนข้อมูลที่ถูกต้อง
    [Test]
    public async Task Get_ExistingId_ShouldReturnViewModel()
    {
        RunAsDefaultUser();
        var id = await TestDataFactory.CreateTestRecommendationAsync();

        var result = await SendAsync(new GetRecommendationByIdQuery { Id = id });

        result.Should().NotBeNull();
        result.Id.Should().Be(id);
    }

    /// ทดสอบ: ค้นหาคำแนะนำด้วย Id ที่ไม่มี ควร throw NotFoundException
    [Test]
    public async Task Get_NonExisting_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new GetRecommendationByIdQuery { Id = 99999 }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
