using Kondongpu.Application.Features.CheckupTypes.Queries.Get;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.CheckupTypes.Queries;

public class GetCheckupTypeListTests : BaseTestFixture
{
    /// ทดสอบ: ดึงรายการประเภทการตรวจเมื่อมีข้อมูลในระบบ ควรคืนรายการที่ไม่ว่างเปล่า
    [Test]
    public async Task GetList_WhenDataExists_ShouldReturnNonEmptyList()
    {
        RunAsDefaultUser();
        await TestDataFactory.CreateTestCheckupTypeAsync();

        var result = await SendAsync(new GetCheckupTypeListQuery());

        result.Should().NotBeNull();
        result.CheckupTypes.Should().NotBeEmpty();
    }

    /// ทดสอบ: ดึงรายการประเภทการตรวจเมื่อไม่มีข้อมูล ควรคืนรายการว่างเปล่า
    [Test]
    public async Task GetList_WhenNoData_ShouldReturnEmptyList()
    {
        RunAsDefaultUser();

        var result = await SendAsync(new GetCheckupTypeListQuery());

        result.Should().NotBeNull();
        result.CheckupTypes.Should().BeEmpty();
    }
}
