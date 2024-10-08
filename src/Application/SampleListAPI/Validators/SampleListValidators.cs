using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Application.SampleListAPI.Commands.CreateSampleList;

namespace SampleProject.Application.SampleListAPI.Validators;
public class SampleListValidators : AbstractValidator<CreateSampleList>
{
    public SampleListValidators()
    {
        RuleFor(x => x.PropertyTypeDetails.PropertyType)
            .NotEmpty().WithMessage("Property Type is required.");

        RuleFor(x => x.PropertyTypeDetails.FoundationType)
            .NotEmpty().WithMessage("Foundation Type is required.");

        RuleFor(x => x.PropertyTypeDetails.YearBuilt)
            .GreaterThan(1800).WithMessage("Year built must be valid.");

        RuleFor(x => x.PropertyAddress.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.PropertyAddress.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.PropertyAddress.Province)
            .NotEmpty().WithMessage("Province is required.");

        RuleFor(x => x.PropertyAddress.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.")
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("Postal code must be a valid format.");

        RuleFor(x => x.PropertyAddress.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(x => x.PropertyAddress.CadastralDesignation)
        .GreaterThan(0).WithMessage("Cadastral designation is required and must be a positive integer.")
        .Must(x => x.ToString().Length == 7)
        .WithMessage("Cadastral designation must be exactly 7 digits long.");

        RuleFor(x => x.PropertyOverview.Bedroom)
            .Must(BeValidNumber).WithMessage("Bedroom count must be a valid number or empty.");

        RuleFor(x => x.PropertyOverview.Bathroom)
            .Must(BeValidNumber).WithMessage("Bathroom count must be a valid number or empty.");

        RuleFor(x => x.PropertyOverview.ElevatorAccess)
            .NotEmpty().When(x => x.PropertyOverview.ElevatorAccess != string.Empty)
            .WithMessage("Elevator Access must be specified if provided.");

        RuleForEach(x => x.PropertyOverview.AmenityIds)
            .GreaterThan(0).WithMessage("Amenity Id must be greater than zero.");

        RuleForEach(x => x.PropertyOverview.ExternalFeatureIds)
            .GreaterThan(0).WithMessage("External Feature Id must be greater than zero.");

        RuleFor(x => x.PropertyDimension.IsMultilevel)
            .NotNull().WithMessage("IsMultilevel property must be specified.");

        RuleFor(x => x.LotDimension.Length)
            .GreaterThan(0).WithMessage("Lot length must be greater than zero.");

        RuleFor(x => x.LotDimension.Width)
            .GreaterThan(0).WithMessage("Lot width must be greater than zero.");

        RuleFor(x => x.LotDimension.TotalLotSize)
            .GreaterThan(0).WithMessage("Total lot size must be greater than zero.");

        RuleFor(x => x.LotDimension.Unit)
            .NotEmpty().WithMessage("Unit of measurement is required.");

        RuleFor(x => x.CoownershipDetails.Type)
            .NotEmpty().WithMessage("Coownership type must be provided.")
            .When(x => !string.IsNullOrEmpty(x.CoownershipDetails.Type));
    }
    private bool BeValidNumber(string value)
    {
        return string.IsNullOrEmpty(value) || int.TryParse(value, out _);
    }
}
