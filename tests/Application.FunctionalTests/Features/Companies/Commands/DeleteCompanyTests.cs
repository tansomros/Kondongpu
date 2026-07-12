using Kondongpu.Application.Features.Companies.Commands.Create;
using Kondongpu.Application.Features.Companies.Commands.Delete;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Companies.Commands;

public class DeleteCompanyTests : BaseTestFixture
{
    /// ทดสอบ: ลบบริษัทที่มีอยู่ในระบบ ควรลบข้อมูลออกจากฐานข้อมูลสำเร็จ
    [Test]
    public async Task Delete_ExistingCompany_ShouldRemoveFromDatabase()
    {
        RunAsDefaultUser();

        var id = await SendAsync(new CreateCompanyCommand
        {
            Name = "บริษัท จะลบ จำกัด",
            IsActive = true
        });

        await SendAsync(new DeleteCompanyCommand { Id = id });

        var deleted = await FindAsync<Company>(id);
        deleted.Should().BeNull();
    }

    /// ทดสอบ: ลบบริษัทที่ไม่มีในระบบ ควร throw NotFoundException
    [Test]
    public async Task Delete_NonExistingCompany_ShouldThrowNotFoundException()
    {
        RunAsDefaultUser();

        await FluentActions.Invoking(() => SendAsync(new DeleteCompanyCommand { Id = 99999 }))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.NotFoundException>();
    }
}
