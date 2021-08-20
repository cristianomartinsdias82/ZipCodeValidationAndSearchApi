using ResultHelpers;
using System.Threading;
using System.Threading.Tasks;

namespace ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode
{
    public interface IAddressSearchClient
    {
        SearchAddressByZipCodeResponse SearchByZipCode(string zipCode);
        Task<SearchAddressByZipCodeResponse> SearchByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default);
    }
}
