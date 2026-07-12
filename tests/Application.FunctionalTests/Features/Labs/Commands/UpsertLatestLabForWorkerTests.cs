using FluentAssertions;
using NUnit.Framework;
using Kondongpu.Application.Features.Labs.Commands.Update;
using Kondongpu.Application.FunctionalTests.Features._Shared;
using Kondongpu.Domain.Entities;
using static Kondongpu.Application.FunctionalTests.Testing;

namespace Kondongpu.Application.FunctionalTests.Features.Labs.Commands;

public class UpsertLatestLabForWorkerTests : BaseTestFixture
{
    /// ทดสอบ: อัปเดตสถานะ IsLabResultReady เป็น true เมื่อได้รับผลแลปครบตาม LabOrder (ตรวจสอบทั้งหมด)
    [Test]
    public async Task Handle_WithAllLabOrdersSatisfied_ShouldSetIsLabResultReadyToTrue()
    {
        RunAsDefaultUser();

        // Arrange
        var checkupTypeId = await TestDataFactory.CreateTestCheckupTypeAsync();
        var patientId = await TestDataFactory.CreateTestPatientAsync();
        var checkupId = await TestDataFactory.CreateTestCheckupAsync(patientId, checkupTypeId);

        var checkup = await FindAsync<Checkup>(checkupId);
        checkup!.LabOrder = new List<string> { "900", "901" };
        checkup.IsLabResultReady = false;
        await UpdateAsync(checkup);

        var command = new UpsertLatestLabForWorkerCommand
        {
            UpsertList = new List<UpsertLabCommand>
            {
                new UpsertLabCommand
                {
                    VisitNumber = checkup.VisitNumber,
                    LabItemCode = 900,
                    IsAbnormal = "N",
                    ResultValue = "1",
                    ReferenceRange = "0-2",
                    ResultDate = DateOnly.FromDateTime(DateTime.Now),
                    LabItemName = "Test 900"
                },
                new UpsertLabCommand
                {
                    VisitNumber = checkup.VisitNumber,
                    LabItemCode = 901,
                    IsAbnormal = "Y",
                    ResultValue = "5",
                    ReferenceRange = "0-2",
                    ResultDate = DateOnly.FromDateTime(DateTime.Now),
                    LabItemName = "Test 901"
                }
            }
        };

        // Act
        await SendAsync(command);

        // Assert
        var updatedCheckup = await FindAsync<Checkup>(checkupId);
        updatedCheckup.Should().NotBeNull();
        updatedCheckup!.IsLabResultReady.Should().BeTrue();
    }

    /// ทดสอบ: ไม่แก้ไขสถานะ IsLabResultReady (ยังคงเป็น false) เมื่อได้รับผลแลปไม่ครบตาม LabOrder
    [Test]
    public async Task Handle_WithPartialLabOrdersSatisfied_ShouldNotSetIsLabResultReady()
    {
        RunAsDefaultUser();

        // Arrange
        var checkupTypeId = await TestDataFactory.CreateTestCheckupTypeAsync();
        var patientId = await TestDataFactory.CreateTestPatientAsync();
        var checkupId = await TestDataFactory.CreateTestCheckupAsync(patientId, checkupTypeId);

        var checkup = await FindAsync<Checkup>(checkupId);
        checkup!.LabOrder = new List<string> { "902", "903" };
        checkup.IsLabResultReady = false;
        await UpdateAsync(checkup);

        var command = new UpsertLatestLabForWorkerCommand
        {
            UpsertList = new List<UpsertLabCommand>
            {
                new UpsertLabCommand
                {
                    VisitNumber = checkup.VisitNumber,
                    LabItemCode = 902,
                    IsAbnormal = "N",
                    ResultValue = "1",
                    ReferenceRange = "0-2",
                    ResultDate = DateOnly.FromDateTime(DateTime.Now),
                    LabItemName = "Test 902"
                }
            }
        };

        // Act
        await SendAsync(command);

        // Assert
        var updatedCheckup = await FindAsync<Checkup>(checkupId);
        updatedCheckup.Should().NotBeNull();
        updatedCheckup!.IsLabResultReady.Should().BeFalse();
    }

    /// ทดสอบ: ไม่แก้ไขสถานะ IsLabResultReady เมื่อไม่มีการสั่ง LabOrder (อาร์เรย์ว่าง)
    [Test]
    public async Task Handle_WithEmptyLabOrders_ShouldNotSetIsLabResultReady()
    {
        RunAsDefaultUser();

        // Arrange
        var checkupTypeId = await TestDataFactory.CreateTestCheckupTypeAsync();
        var patientId = await TestDataFactory.CreateTestPatientAsync();
        var checkupId = await TestDataFactory.CreateTestCheckupAsync(patientId, checkupTypeId);

        var checkup = await FindAsync<Checkup>(checkupId);
        checkup!.LabOrder = new List<string>();
        checkup.IsLabResultReady = false;
        await UpdateAsync(checkup);

        var command = new UpsertLatestLabForWorkerCommand
        {
            UpsertList = new List<UpsertLabCommand>
            {
                new UpsertLabCommand
                {
                    VisitNumber = checkup.VisitNumber,
                    LabItemCode = 905,
                    IsAbnormal = "N",
                    ResultValue = "1",
                    ReferenceRange = "0-2",
                    ResultDate = DateOnly.FromDateTime(DateTime.Now),
                    LabItemName = "Test 905"
                }
            }
        };

        // Act
        await SendAsync(command);

        // Assert
        var updatedCheckup = await FindAsync<Checkup>(checkupId);
        updatedCheckup.Should().NotBeNull();
        updatedCheckup!.IsLabResultReady.Should().BeFalse();
    }
}
