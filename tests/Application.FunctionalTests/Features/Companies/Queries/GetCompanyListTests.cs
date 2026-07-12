using Kondongpu.Application.Features.Companies.Commands.Create;
using Kondongpu.Application.Features.Companies.Queries.Get;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Companies.Queries;

public class GetCompanyListTests : BaseTestFixture
{
    /// ทดสอบ: ดึงรายชื่อบริษัทเมื่อมีข้อมูลในระบบ ควรคืนรายการที่ไม่ว่างเปล่า
    [Test]
    public async Task GetList_WhenDataExists_ShouldReturnNonEmptyList()
    {
        RunAsDefaultUser();

        await SendAsync(new CreateCompanyCommand { Name = "บริษัท A จำกัด", IsActive = true });
        await SendAsync(new CreateCompanyCommand { Name = "บริษัท B จำกัด", IsActive = true });

        var result = await SendAsync(new GetCompanyListQuery());

        result.Should().NotBeNull();
        result.Companies.Should().HaveCountGreaterThanOrEqualTo(2);
    }

    /// ทดสอบ: ดึงรายชื่อบริษัทเมื่อไม่มีข้อมูล ควรคืนรายการว่างเปล่า
    [Test]
    public async Task GetList_WhenNoData_ShouldReturnEmptyList()
    {
        RunAsDefaultUser();

        var result = await SendAsync(new GetCompanyListQuery());

        result.Should().NotBeNull();
        result.Companies.Should().BeEmpty();
    }
}
