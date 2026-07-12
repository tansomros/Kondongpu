using Kondongpu.Application.Features.Companies.Commands.Create;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;
using ValidationException = Kondongpu.Application.Exceptions.ValidationException;

namespace Kondongpu.Application.FunctionalTests.Features.Companies.Commands;

public class CreateCompanyTests : BaseTestFixture
{
    /// ทดสอบ: สร้างบริษัทใหม่ด้วยข้อมูลที่ถูกต้อง ควรคืนค่า Id ที่มากกว่า 0
    [Test]
    public async Task Create_WithValidData_ShouldReturnPositiveId()
    {
        RunAsDefaultUser();

        var command = new CreateCompanyCommand
        {
            Name = "บริษัท สุรนารี จำกัด",
            AddressNo = "111 ถ.มหาวิทยาลัย",
            ZipCode = "30000",
            IsActive = true
        };

        var result = await SendAsync(command);

        result.Should().BeGreaterThan(0);
    }

    /// ทดสอบ: สร้างบริษัทใหม่แล้วตรวจสอบว่าข้อมูลถูกบันทึกในฐานข้อมูลอย่างถูกต้อง
    [Test]
    public async Task Create_WithValidData_ShouldPersistEntity()
    {
        RunAsDefaultUser();

        var command = new CreateCompanyCommand
        {
            Name = "บริษัท ทดสอบระบบ จำกัด",
            AddressNo = "222 ถ.สุรนารี",
            ZipCode = "30000",
            IsActive = true
        };

        var id = await SendAsync(command);

        var company = await FindAsync<Company>(id);

        company.Should().NotBeNull();
        company!.Name.Should().Be("บริษัท ทดสอบระบบ จำกัด");
        company.AddressNo.Should().Be("222 ถ.สุรนารี");
        company.ZipCode.Should().Be("30000");
        company.IsActive.Should().BeTrue();
    }

    /// ทดสอบ: สร้างบริษัทโดยไม่ระบุชื่อ ควร throw ValidationException เนื่องจากชื่อเป็นข้อมูลบังคับ
    [Test]
    public async Task Create_WithEmptyName_ShouldThrowValidationException()
    {
        RunAsDefaultUser();

        var command = new CreateCompanyCommand
        {
            Name = "",
            IsActive = true
        };

        var act = () => SendAsync(command);
        (await act.Should().ThrowAsync<ValidationException>())
            .Which.Errors.Should().ContainKey("Name")
            .WhoseValue.Should().Contain("ชื่อหน่วยงาน/บริษัท ต้องไม่ว่าง");
    }
}
