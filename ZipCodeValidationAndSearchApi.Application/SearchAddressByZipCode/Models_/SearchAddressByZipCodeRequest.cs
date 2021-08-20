using MediatR;
using ResultHelpers;

namespace ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode
{
    public class SearchAddressByZipCodeRequest : IRequest<SearchAddressByZipCodeResponse>
    {
        public string ZipCode { get; set; }
    }
}
