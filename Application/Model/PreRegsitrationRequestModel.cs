namespace Application.Model
{
    public class PreRegsitrationRequestModel
    {
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public List<CreditModel> Items { get; set; }
        public string UserId { get; set; }
        public string ClientId { get; set; }
    }
    public class PreRegsitrationResponseModel
    {

    }
}
