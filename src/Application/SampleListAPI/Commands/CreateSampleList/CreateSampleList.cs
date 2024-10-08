using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Application.SampleListAPI.Commands.CreateSampleList
{
    public class CreateSampleList : IRequest<int>
    {
        public PropertyTypeDetails PropertyTypeDetails { get; init; } = new();
        public CoownershipDetails CoownershipDetails { get; init; } = new();
        public PropertyAddress PropertyAddress { get; init; } = new();
        public PropertyOverview PropertyOverview { get; init; } = new();
        public PropertyDimension PropertyDimension { get; init; } = new();
        public LotDimension LotDimension { get; init; } = new();
    }

    public record class PropertyTypeDetails
    {
        public string PropertyType { get; init; } = string.Empty;
        public string FoundationType { get; init; } = string.Empty;
        public int YearBuilt { get; init; }
    }

    public record class CoownershipDetails
    {
        public string Type { get; init; } = string.Empty;
    }

    public record class PropertyAddress
    {
        public string Address { get; init; } = string.Empty;
        public string AddressLine2 { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string Province { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public int CadastralDesignation { get; init; }
    }

    public record class PropertyOverview
    {
        public string Bedroom { get; init; } = string.Empty;
        public string Bathroom { get; init; } = string.Empty;
        public string ElevatorAccess { get; init; } = string.Empty;
        public List<int> AmenityIds { get; init; } = new();
        public List<int> ExternalFeatureIds { get; init; } = new();
    }

    public record class PropertyDimension
    {
        public bool IsMultilevel { get; init; }
    }

    public record class LotDimension
    {
        public decimal Length { get; init; }
        public decimal Width { get; init; }
        public decimal TotalLotSize { get; init; }
        public string Unit { get; init; } = string.Empty;
    }


    public class CreateSampleListCommandHandler : IRequestHandler<CreateSampleList, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateSampleListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSampleList request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(request);

                if (!Validator.TryValidateObject(request, context, validationResults, true))
                {
                    throw new FluentValidation.ValidationException($"Validation failed: {string.Join(", ", validationResults.Select(x => x.ErrorMessage))}");
                }
                var property = new Property
                {
                    PropertyType = request.PropertyTypeDetails.PropertyType,
                    FoundationType = request.PropertyTypeDetails.FoundationType,
                    YearBuilt = request.PropertyTypeDetails.YearBuilt,
                    CoownershipType = request.CoownershipDetails.Type,
                    Address = request.PropertyAddress.Address,
                    AddressLine2 = request.PropertyAddress.AddressLine2,
                    City = request.PropertyAddress.City,
                    Province = request.PropertyAddress.Province,
                    PostalCode = request.PropertyAddress.PostalCode,
                    Country = request.PropertyAddress.Country,
                    CadastralDesignation = request.PropertyAddress.CadastralDesignation,
                    Bedroom = request.PropertyOverview.Bedroom,
                    Bathroom = request.PropertyOverview.Bathroom,
                    ElevatorAccess = request.PropertyOverview.ElevatorAccess,
                    IsMultilevel = request.PropertyDimension.IsMultilevel,
                    Length = request.LotDimension.Length,
                    Width = request.LotDimension.Width,
                    TotalLotSize = request.LotDimension.TotalLotSize,
                    Unit = request.LotDimension.Unit,
                    Amenities = new List<Amenity>(),
                    ExternalFeatures = new List<ExternalFeature>()
                };

                foreach (var amenityId in request.PropertyOverview.AmenityIds)
                {   
                    property.Amenities.Add(new Amenity { Id = amenityId });
                }

                foreach (var externalFeatureId in request.PropertyOverview.ExternalFeatureIds)
                {
                    property.ExternalFeatures.Add(new ExternalFeature { Id = externalFeatureId });
                }

                _context.Properties.Add(property);
                await _context.SaveChangesAsync(cancellationToken);

                return property.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}
