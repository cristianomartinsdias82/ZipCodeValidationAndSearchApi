using MediatR;
using ResultHelpers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode
{
    public class SearchAddressByZipCodeHandler : IRequestHandler<SearchAddressByZipCodeRequest, SearchAddressByZipCodeResponse>
    {
        private readonly IAddressSearchClient _addressSearchClient;

        public SearchAddressByZipCodeHandler(IAddressSearchClient addressSearchClient)
        {
            _addressSearchClient = addressSearchClient;
        }

        public async Task<SearchAddressByZipCodeResponse> Handle(SearchAddressByZipCodeRequest request, CancellationToken cancellationToken)
        {
            var result = await _addressSearchClient.SearchByZipCodeAsync(request.ZipCode, cancellationToken);

            return new SearchAddressByZipCodeResponse
            {
                Result = OperationResult<SearchAddressByZipCodeResponse>.Successful(result, result == null ? "No address was found." : "A match was found.", $"{(int)HttpStatusCode.OK}")
            };
        }
    }
}
