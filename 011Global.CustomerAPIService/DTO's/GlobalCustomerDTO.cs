namespace _011Global.CustomerAPIService.DTO_s
{
    public class GlobalCustomerDTO
    {
        public int ServicePackageID { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public List<CreditCardDTO> GlobalCreditCards { get; set; }
        public AddressDTO BillingAddress { get; set; }
        public AddressDTO ShippingAddress { get; set; }
    }

    public class CreditCardDTO
    {
        public string CreditCardNumber { get; set; }
        public string LastFourNumbers { get; set; }
        public string CardHolder { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
    }

    public class AddressDTO
    {
        public short CountryISO2 { get; set; }
        public string StateISO2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
    }
}
