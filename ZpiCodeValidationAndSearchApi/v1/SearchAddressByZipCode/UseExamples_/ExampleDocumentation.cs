using ResultHelpers;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode;

namespace ZipCodeValidationAndSearchApi.v1.SearchAddressByZipCode
{
    public sealed class SearchAddressByZipCodeResponseOperationResultExampleDocumentation
        : IExamplesProvider<OperationResult<SearchAddressByZipCodeResponse>>
    {
        public OperationResult<SearchAddressByZipCodeResponse> GetExamples()
        => OperationResult<SearchAddressByZipCodeResponse>.Successful(
            new SearchAddressByZipCodeResponse
            {
                City = "São Paulo",
                Street = "Rua das Pitombeiras",
                Complement = "Bloco A",
                Unit = default,
                ZipCode = "04121-987",
                Neighborhood = "Vila Alexandria",
                StateInitials = "SP",
                GiaCode = 1234,
                IbgeCode = 5678
            },
            "A match was found.",
            $"{(int)HttpStatusCode.OK}");
    }
}
