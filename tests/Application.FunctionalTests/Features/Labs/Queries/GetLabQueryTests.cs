using Kondongpu.Application.Features.Labs.Queries.Get;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;

using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Labs.Queries;

public class GetLabQueryTests : BaseTestFixture
{
    /// <summary>
    /// ทดสอบ: ค้นหารายการ Lab ด้วย VisitNumber ที่มีอยู่ ควรคืนข้อมูลที่ถูกต้อง
    /// </summary>
    [Test]
    public async Task Get_ExistingLab_ShouldReturnViewModel()
    {
        RunAsDefaultUser();
        var prereqs = await TestDataFactory.SeedFullCheckupPrerequisitesAsync();

        await TestDataFactory.CreateTestLabAsync(
            prereqs.CheckupId, prereqs.VisitNumber, prereqs.CheckupItemId);

        var result = await SendAsync(new GetLabQuery { VisitNumber = prereqs.VisitNumber });

        result.Should().NotBeNull();
    }

    /// <summary>
    /// ทดสอบ: ค้นหารายการ Lab ด้วย VisitNumber ที่ไม่มีอยู่ ควร throw NotFoundException
    /// </summary>
    [Test]
    public async Task Get_NonExistingLab_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new GetLabQuery { VisitNumber = "VN_NOT_EXIST" }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
