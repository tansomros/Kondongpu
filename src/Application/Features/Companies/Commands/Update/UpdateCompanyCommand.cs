using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Companies.Commands.Update;

public record UpdateCompanyCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required string CompanyCode { get; set; }
    public required string Name { get; set; }
    public string? AliasName { get; set; }
    public required string OwnerName { get; set; }
    public string? VatId { get; set; }
    public string? AddressNo { get; set; }
    public string? Moo { get; set; }
    public string? Village { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? ProvinceId { get; set; }
    public string? ZipCode { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public int? MUser { get; set; }
}

public class UpdateCommandHandler : IRequestHandler<UpdateCompanyCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public UpdateCommandHandler(IKondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Company.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (company == null)
        {
            throw new NotFoundException(nameof(company), request.Id);
        }

        company.Code = request.CompanyCode;
        company.Name = request.Name;
        company.AliasName = request.AliasName;
        company.OwnerName = request.OwnerName;
        company.VatId = request.VatId;
        company.AddressNo = request.AddressNo;       
        company.Moo = request.Moo;
        company.Village = request.Village;
        company.SubDistrict = request.District;
        company.District = request.City;
        company.ProvinceId = request.ProvinceId;
        company.ZipCode = request.ZipCode;
        company.Telephone = request.Telephone;
        company.Fax = request.Fax;
        company.Email = request.Email;
        company.Website = request.Website;
        company.MUser = request.MUser;

        _context.Company.Update(company);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
