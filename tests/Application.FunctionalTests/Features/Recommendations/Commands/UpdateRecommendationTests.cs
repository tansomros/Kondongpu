using Kondongpu.Application.Features.Recommendations.Commands.Update;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Recommendations.Commands;

public class UpdateRecommendationTests : BaseTestFixture
{
    /// ทดสอบ: อัปเดตคำแนะนำที่มีอยู่ ควรบันทึกข้อมูลใหม่สำเร็จ
    [Test]
    public async Task Update_ExistingRecommendation_ShouldUpdateFields()
    {
        RunAsDefaultUser();
        var id = await TestDataFactory.CreateTestRecommendationAsync();

        await SendAsync(new UpdateRecommendationCommand
        {
            Id = id,
            Code = "UPDATED",
            Name = "คำแนะนำที่อัปเดต",
            CheckType = "LAB",
            LowValue = 50,
            HighValue = 150,
            ConclusionTh = "อัปเดตแล้ว",
            IsActive = true
        });

        var updated = await FindAsync<Recommendation>(id);
        updated.Should().NotBeNull();
        updated!.Code.Should().Be("UPDATED");
        updated.Name.Should().Be("คำแนะนำที่อัปเดต");
    }

    /// ทดสอบ: อัปเดตคำแนะนำที่ไม่มีในระบบ ควร throw ValidationException
    [Test]
    public async Task Update_NonExisting_ShouldThrowValidationException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new UpdateRecommendationCommand
        {
            Id = 99999,
            Code = "X",
            Name = "X",
            CheckType = "LAB",
            LowValue = 0,
            HighValue = 100,
            ConclusionTh = "X",
            IsActive = true
        })).Should().ThrowAsync<Kondongpu.Application.Exceptions.ValidationException>();
    }
}
