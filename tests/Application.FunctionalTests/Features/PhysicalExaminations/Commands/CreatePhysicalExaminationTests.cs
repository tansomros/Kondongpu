using Kondongpu.Application.Features.PhysicalExaminations.Commands.Create;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.PhysicalExaminations.Commands;

public class CreatePhysicalExaminationTests : BaseTestFixture
{
    /// ทดสอบ: สร้างผลตรวจร่างกายใหม่ ควรคืนค่า Id ที่มากกว่า 0
    [Test]
    public async Task Create_WithValidData_ShouldReturnPositiveId()
    {
        RunAsDefaultUser();
        var prereqs = await TestDataFactory.SeedFullCheckupPrerequisitesAsync();

        var command = new CreatePhysicalExaminationCommand
        {
            CheckupId = prereqs.CheckupId,
            VisitNumber = prereqs.VisitNumber,
            CheckupItemId = prereqs.CheckupItemId,
            Ga = "Normal"
        };

        var result = await SendAsync(command);
        result.Should().BeGreaterThan(0);
    }
}
