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
    public record class CreateSampleList : IRequest<int>
    {
        public PropertyTypeDetails PropertyTypeDetails { get; init; } = null!;
        public CoownershipDetails CoownershipDetails { get; init; } = new();
        public PropertyAddress PropertyAddress { get; init; } = null!;
        public PropertyOverview PropertyOverview { get; init; } = new();
        public PropertyDimension PropertyDimension { get; init; } = new();
        public LotDimension LotDimension { get; init; } = new();
    }

    public record class PropertyTypeDetails
    {
        [Required(ErrorMessage = "Property Type is required.")]
        public required string PropertyType { get; init; }
        [Required(ErrorMessage = "Fouundation Type is required.")]
        public required string FoundationType { get; init; }
        [Required(ErrorMessage = "Year built Type is required.")]
        public required int YearBuilt { get; init; }
    }

    public record class CoownershipDetails
    {
        public string Type { get; init; } = string.Empty;
    }

    public record class PropertyAddress
    {
        [Required(ErrorMessage = "Address is required.")]
        public required string Address { get; init; }
        public string AddressLine2 { get; init; } = string.Empty;
        [Required(ErrorMessage = "City is required.")]
        public required string City { get; init; }
        [Required(ErrorMessage = "Province is required.")]
        public required string Province { get; init; }
        [Required(ErrorMessage = "Postal code is required.")]
        public required string PostalCode { get; init; }
        [Required(ErrorMessage = "Country is required.")]
        public required string Country { get; init; }
        [Required(ErrorMessage = "Cadastral designation is required.")]
        public required int CadastralDesignation { get; init; }
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
                    Unit = request.LotDimension.Unit
                };

                foreach (var amenityId in request.PropertyOverview.AmenityIds)
                {
                    var amenity = await _context.Amenities.FindAsync(amenityId);
                    if (amenity != null)
                    {
                        amenity.PropertyId = property.Id;
                        property.Amenities.Add(amenity);
                    }
                }

                foreach (var externalFeatureId in request.PropertyOverview.ExternalFeatureIds)
                {
                    var externalFeature = await _context.ExternalFeatures.FindAsync(externalFeatureId);
                    if (externalFeature != null)
                    {
                        externalFeature.PropertyId = property.Id;
                        property.ExternalFeatures.Add(externalFeature);
                    }
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
