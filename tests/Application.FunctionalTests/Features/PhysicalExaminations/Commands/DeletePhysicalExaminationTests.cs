using Kondongpu.Application.Features.PhysicalExaminations.Commands.Delete;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;
using ValidationException = Kondongpu.Application.Exceptions.ValidationException;

namespace Kondongpu.Application.FunctionalTests.Features.PhysicalExaminations.Commands;

public class DeletePhysicalExaminationTests : BaseTestFixture
{
    /// ทดสอบ: ลบผลตรวจร่างกายที่มีอยู่ ควรลบสำเร็จ
    [Test]
    public async Task Delete_Existing_ShouldRemoveFromDatabase()
    {
        RunAsDefaultUser();
        var prereqs = await TestDataFactory.SeedFullCheckupPrerequisitesAsync();
        var id = await TestDataFactory.CreateTestPhysicalExaminationAsync(prereqs.CheckupId, prereqs.VisitNumber, prereqs.CheckupItemId);

        await SendAsync(new DeletePhysicalExaminationCommand { Id = id });

        var deleted = await FindAsync<PhysicalExamination>(id);
        deleted.Should().BeNull();
    }

    /// ทดสอบ: ลบผลตรวจร่างกายที่ไม่มี ควร throw ValidationException เนื่องจาก validator ตรวจสอบการมีอยู่ของข้อมูล
    [Test]
    public async Task Delete_NonExisting_ShouldThrowValidationException()
    {
        RunAsDefaultUser();

        var act = () => SendAsync(new DeletePhysicalExaminationCommand { Id = 99999 });

        (await act.Should().ThrowAsync<ValidationException>())
            .Which.Errors.Should().ContainKey("Id")
            .WhoseValue.Should().Contain("ไม่พบข้อมูล");
    }
}
