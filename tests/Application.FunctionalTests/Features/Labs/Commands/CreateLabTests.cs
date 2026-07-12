using Kondongpu.Application.Features.Labs.Commands.Create;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Labs.Commands;

public class CreateLabTests : BaseTestFixture
{
    /// ทดสอบ: สร้างผลแลปใหม่ด้วยข้อมูลที่ถูกต้อง ควรคืนค่า Id ที่มากกว่า 0
    [Test]
    public async Task Create_WithValidData_ShouldReturnPositiveId()
    {
        RunAsDefaultUser();
        var prereqs = await SeedLabPrerequisitesAsync("LAB001");

        var command = new CreateLabCommand
        {
            CheckupId = prereqs.CheckupId,
            VisitNumber = prereqs.VisitNumber,
            LabItemCode = "LAB001",
            ResultValue = "120",
            ReferenceRange = "70-110",
            IsAbnormal = "Y",
            Comments = "สูงกว่าปกติ"
        };

        var result = await SendAsync(command);
        result.Should().BeGreaterThan(0);
    }

    /// ทดสอบ: สร้างผลแลปแล้วตรวจสอบข้อมูลที่บันทึก
    [Test]
    public async Task Create_WithValidData_ShouldPersistEntity()
    {
        RunAsDefaultUser();
        var prereqs = await SeedLabPrerequisitesAsync("LAB002");

        var id = await SendAsync(new CreateLabCommand
        {
            CheckupId = prereqs.CheckupId,
            VisitNumber = prereqs.VisitNumber,
            LabItemCode = "LAB002",
            ResultValue = "5.5",
            ReferenceRange = "4.0-6.0",
            IsAbnormal = "N",
            Comments = "ปกติ"
        });

        var entity = await FindAsync<Lab>(id);
        entity.Should().NotBeNull();
        entity!.VisitNumber.Should().Be(prereqs.VisitNumber);
    }

    private static async Task<TestDataFactory.CheckupPrerequisites> SeedLabPrerequisitesAsync(string labItemCode)
    {
        var checkupTypeId = await TestDataFactory.CreateTestCheckupTypeAsync();
        var patientId = await TestDataFactory.CreateTestPatientAsync();
        var classId = await TestDataFactory.CreateTestCheckupClassAsync();
        var groupId = await TestDataFactory.CreateTestCheckupGroupAsync(classId);
        await TestDataFactory.CreateTestCheckupItemAsync(groupId, labItemCode);
        var checkupId = await TestDataFactory.CreateTestCheckupAsync(patientId, checkupTypeId);

        var checkup = await FindAsync<Checkup>(checkupId);
        var visitNumber = checkup!.VisitNumber;

        return new TestDataFactory.CheckupPrerequisites(
            patientId, checkupTypeId, checkupId,
            classId, groupId, 0, visitNumber);
    }
}
