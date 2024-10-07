using System;
using System.Collections.Generic;

namespace SampleProject.Domain.Entities
{
    public class Property : BaseAuditableEntity
    {
        public string PropertyType { get; set; } = string.Empty;
        public string FoundationType { get; set; } = string.Empty;
        public int YearBuilt { get; set; }

        public string CoownershipType { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int CadastralDesignation { get; set; }

        public string Bedroom { get; set; } = string.Empty;
        public string Bathroom { get; set; } = string.Empty;
        public string ElevatorAccess { get; set; } = string.Empty;
        public ICollection<Amenity> Amenities { get; set; } = null!;
        public ICollection<ExternalFeature> ExternalFeatures { get; set; } = null!;

        public bool IsMultilevel { get; set; }

        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal TotalLotSize { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
