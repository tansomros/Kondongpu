using Kondongpu.Application.Features.Recommendations.Commands.Delete;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Recommendations.Commands;

public class DeleteRecommendationTests : BaseTestFixture
{
    /// ทดสอบ: ลบคำแนะนำที่มีอยู่ ควรลบออกจากฐานข้อมูลสำเร็จ
    [Test]
    public async Task Delete_ExistingRecommendation_ShouldRemoveFromDatabase()
    {
        RunAsDefaultUser();
        var id = await TestDataFactory.CreateTestRecommendationAsync();

        await SendAsync(new DeleteRecommendationCommand { Id = id });

        var deleted = await FindAsync<Recommendation>(id);
        deleted.Should().BeNull();
    }

    /// ทดสอบ: ลบคำแนะนำที่ไม่มีในระบบ ควร throw NotFoundException
    [Test]
    public async Task Delete_NonExisting_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new DeleteRecommendationCommand { Id = 99999 }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
