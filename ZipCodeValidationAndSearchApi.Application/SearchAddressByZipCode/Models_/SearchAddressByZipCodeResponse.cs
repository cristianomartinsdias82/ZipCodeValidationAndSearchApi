using ZipCodeValidationAndSearchApi.SharedKernel;

namespace ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode
{
    public class SearchAddressByZipCodeResponse : ApplicationResponse<SearchAddressByZipCodeResponse>
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateInitials { get; set; }
        public string Unit { get; set; }
        public int IbgeCode { get; set; }
        public int? GiaCode { get; set; }
    }
}
