using FluentValidation;
using System.Text.RegularExpressions;

namespace ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode
{
    public sealed class SearchAddressByZipCodeValidator : AbstractValidator<SearchAddressByZipCodeRequest>
    {
        public SearchAddressByZipCodeValidator()
        {
            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .WithMessage($"{nameof(SearchAddressByZipCodeRequest.ZipCode)} cannot be null or empty.");

            When(x => !string.IsNullOrWhiteSpace(x.ZipCode), () =>
            {
                RuleFor(x => x.ZipCode)
                    .Must(zipCode => Regex.IsMatch(zipCode, @"[0-9]{8}"))
                    .WithMessage($"{nameof(SearchAddressByZipCodeRequest.ZipCode)} must be numeric and also be 8 characters long.");
            });
        }
    }
}
