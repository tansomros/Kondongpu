using Kondongpu.Application.Features.Recommendations.Commands.Create;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;
using ValidationException = Kondongpu.Application.Exceptions.ValidationException;

namespace Kondongpu.Application.FunctionalTests.Features.Recommendations.Commands;

public class CreateRecommendationTests : BaseTestFixture
{
    /// ทดสอบ: สร้างคำแนะนำใหม่ด้วยข้อมูลที่ถูกต้อง ควรคืนค่า Id ที่มากกว่า 0
    [Test]
    public async Task Create_WithValidData_ShouldReturnPositiveId()
    {
        RunAsDefaultUser();

        var command = new CreateRecommendationCommand
        {
            Code = "REC001",
            Name = "ผล FBS สูง",
            CheckType = "LAB",
            SexCode = "A",
            CompareValue = "B",
            LowValue = 100,
            HighValue = 125,
            ConclusionTh = "น้ำตาลสูงกว่าปกติ",
            ConclusionEn = "High blood sugar",
            RecommendTh = "ควรพบแพทย์",
            RecommendEn = "See doctor",
            IsActive = true
        };

        var result = await SendAsync(command);
        result.Should().BeGreaterThan(0);
    }

    /// ทดสอบ: สร้างคำแนะนำแล้วตรวจสอบข้อมูลที่บันทึก
    [Test]
    public async Task Create_WithValidData_ShouldPersistEntity()
    {
        RunAsDefaultUser();

        var id = await SendAsync(new CreateRecommendationCommand
        {
            Code = "REC002",
            Name = "ผล Cholesterol",
            CheckType = "LAB",
            SexCode = "A",
            CompareValue = "B",
            LowValue = 200,
            HighValue = 240,
            ConclusionTh = "ไขมันสูง",
            ConclusionEn = "High cholesterol",
            RecommendTh = "ควรพบแพทย์",
            RecommendEn = "See doctor",
            IsActive = true
        });

        var entity = await FindAsync<Recommendation>(id);
        entity.Should().NotBeNull();
        entity!.Code.Should().Be("REC002");
        entity.Name.Should().Be("ผล Cholesterol");
    }

    /// ทดสอบ: สร้างคำแนะนำโดยไม่ระบุรหัส ควร throw ValidationException
    [Test]
    public async Task Create_WithEmptyCode_ShouldThrowValidationException()
    {
        RunAsDefaultUser();

        var command = new CreateRecommendationCommand
        {
            Code = "",
            Name = "ทดสอบ",
            CheckType = "LAB",
            SexCode = "A",
            CompareValue = "B",
            LowValue = 0,
            HighValue = 100,
            ConclusionTh = "ปกติ",
            ConclusionEn = "Normal",
            RecommendTh = "ปกติ",
            RecommendEn = "Normal",
            IsActive = true
        };

        var act = () => SendAsync(command);
        (await act.Should().ThrowAsync<ValidationException>())
            .Which.Errors.Should().ContainKey("Code")
            .WhoseValue.Should().Contain("Code ไม่สามารถเป็นค่าว่างได้");
    }
}
