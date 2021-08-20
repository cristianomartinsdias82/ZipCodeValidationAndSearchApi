using System;
using System.Threading;
using System.Threading.Tasks;
using ViaCep;
using ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode;

namespace ZipCodeValidationAndSearchApi.Infrastructure.SearchAddressByZipCode
{
    public class AddressSearchClient : IAddressSearchClient
    {
        private readonly IViaCepClient _viaCepClient;

        public AddressSearchClient(IViaCepClient viaCepClient)
        {
            _viaCepClient = viaCepClient ?? throw new ArgumentNullException($"The {nameof(viaCepClient)} argument cannot be null.");
        }

        public SearchAddressByZipCodeResponse SearchByZipCode(string zipCode)
            => SearchByZipCodeAsync(zipCode)
                .GetAwaiter()
                .GetResult();

        public async Task<SearchAddressByZipCodeResponse> SearchByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            var searchResult = await _viaCepClient.SearchAsync(zipCode, cancellationToken);

            return Map(searchResult);
        }

        private static SearchAddressByZipCodeResponse Map(ViaCepResult result)
        => result == null ? null : new SearchAddressByZipCodeResponse
        {
            ZipCode = result.ZipCode,
            Street = result.Street,
            Complement = result.Complement,
            Neighborhood = result.Neighborhood,
            City = result.City,
            StateInitials = result.StateInitials,
            Unit = result.Unit,
            IbgeCode = result.IBGECode,
            GiaCode = result.GIACode
        };
    }
}