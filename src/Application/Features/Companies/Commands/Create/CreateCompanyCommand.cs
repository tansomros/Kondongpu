using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;
using MediatR;

namespace Kondongpu.Application.Features.Companies.Commands.Create
{
    public class CreateCompanyCommand : IRequest<int>
    {
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
        public int? CUser { get; set; }
    }

    public class CreateCompanyCommmandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly IKondongpuDatabaseContext _kondongpuDatabaseContext;

        public CreateCompanyCommmandHandler(IKondongpuDatabaseContext kondongpuDatabaseContext)
        {
            _kondongpuDatabaseContext = kondongpuDatabaseContext;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company(request.CompanyCode, request.Name, request.OwnerName)
            {
                AliasName = request.AliasName,
                VatId = request.VatId,
                AddressNo = request.AddressNo,             
                Moo = request.Moo,
                Village = request.Village,
                SubDistrict = request.District,
                District = request.City,
                ProvinceId = request.ProvinceId,
                ZipCode = request.ZipCode,
                Telephone = request.Telephone,
                Fax = request.Fax,
                Email = request.Email,
                Website = request.Website,
                CUser = request.CUser
            };

            await _kondongpuDatabaseContext.Company.AddAsync(company, cancellationToken);
            await _kondongpuDatabaseContext.SaveChangesAsync(cancellationToken);
            return company.Id;
        }
    }
}
