using Kondongpu.Application.Features.Companies.Commands.Create;
using Kondongpu.Application.Features.Companies.Commands.Update;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Companies.Commands;

public class UpdateCompanyTests : BaseTestFixture
{
    /// ทดสอบ: อัปเดตข้อมูลบริษัทที่มีอยู่ในระบบ ควรบันทึกข้อมูลใหม่ได้สำเร็จ
    [Test]
    public async Task Update_ExistingCompany_ShouldUpdateFields()
    {
        RunAsDefaultUser();

        var id = await SendAsync(new CreateCompanyCommand
        {
            Name = "บริษัท เดิม จำกัด",
            IsActive = true
        });

        await SendAsync(new UpdateCompanyCommand
        {
            Id = id,
            Name = "บริษัท ใหม่ จำกัด",
            AddressNo = "999 ถ.ใหม่",
            IsActive = false
        });

        var updated = await FindAsync<Company>(id);
        updated.Should().NotBeNull();
        updated!.Name.Should().Be("บริษัท ใหม่ จำกัด");
        updated.AddressNo.Should().Be("999 ถ.ใหม่");
        updated.IsActive.Should().BeFalse();
    }

    /// ทดสอบ: อัปเดตบริษัทที่ไม่มีในระบบ ควร throw ValidationException
    [Test]
    public async Task Update_NonExistingCompany_ShouldThrowValidationException()
    {
        RunAsDefaultUser();

        var command = new UpdateCompanyCommand
        {
            Id = 99999,
            Name = "ไม่มี",
            IsActive = true
        };

        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<Kondongpu.Application.Exceptions.ValidationException>();
    }
}
