using _011Global.Shared.JobsServiceDBContext.Entities;


namespace _011Global.CustomerAPIService.DTO_s
{
    public class GlobalCustomerMapper
    {
        private GlobalCustomerDTO globalCustomerDTO { get; set; }
        public GlobalCustomerMapper(GlobalCustomerDTO globalCustomerDTO)
        {
            this.globalCustomerDTO = globalCustomerDTO;
        }
        public async Task<GlobalCustomer> Map()
        {
            GlobalCustomer globalCustomer = new GlobalCustomer();

            globalCustomer.BillingAddress = new GlobalAddress();
            globalCustomer.ShippingAddress = new GlobalAddress();
            globalCustomer.CustomerEmail = globalCustomerDTO.CustomerEmail;
            globalCustomer.CustomerFirstName = globalCustomerDTO.CustomerFirstName;
            globalCustomer.CustomerLastName = globalCustomerDTO.CustomerLastName;
            globalCustomer.ServicePackageID = globalCustomerDTO.ServicePackageID;
            globalCustomer.Subscribed = true;
            globalCustomer.CreationDate = DateTime.Now;

            globalCustomer.BillingAddress.CountryISO2 = globalCustomerDTO.BillingAddress.CountryISO2;
            globalCustomer.BillingAddress.StateISO2 = globalCustomerDTO.BillingAddress.StateISO2;
            globalCustomer.BillingAddress.City = globalCustomerDTO.BillingAddress.City;
            globalCustomer.BillingAddress.ZipCode = globalCustomerDTO.BillingAddress.ZipCode;
            globalCustomer.BillingAddress.Address = globalCustomerDTO.BillingAddress.Address;
            globalCustomer.BillingAddress.CreationDate = DateTime.Now;

            globalCustomer.ShippingAddress.CountryISO2 = globalCustomerDTO.ShippingAddress.CountryISO2;
            globalCustomer.ShippingAddress.StateISO2 = globalCustomerDTO.ShippingAddress.StateISO2;
            globalCustomer.ShippingAddress.City = globalCustomerDTO.ShippingAddress.City;
            globalCustomer.ShippingAddress.ZipCode = globalCustomerDTO.ShippingAddress.ZipCode;
            globalCustomer.ShippingAddress.Address = globalCustomerDTO.ShippingAddress.Address;
            globalCustomer.ShippingAddress.CreationDate = DateTime.Now;

            
            foreach(CreditCardDTO dto in globalCustomerDTO.GlobalCreditCards)
            {
                GlobalCreditCard globalCreditCard = new GlobalCreditCard();
                globalCreditCard.CreditCardNumber = dto.CreditCardNumber;
                globalCreditCard.LastFourNumbers = dto.LastFourNumbers;
                globalCreditCard.CardHolder = dto.CardHolder;
                globalCreditCard.ExpirationMonth = dto.ExpirationMonth;
                globalCreditCard.ExpirationYear = dto.ExpirationYear;
                globalCreditCard.CreationDate = DateTime.Now;
                globalCustomer.GlobalCreditCards.Add(globalCreditCard);
            }
            return globalCustomer;
        }
    }
}
