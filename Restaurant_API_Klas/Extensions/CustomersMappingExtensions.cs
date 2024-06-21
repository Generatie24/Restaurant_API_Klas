using Restaurant_API_Klas.Models;
using Restaurant_API_Klas.Models.DTOs.Customers;

namespace Restaurant_API_Klas.Extensions
{
    public static class CustomersMappingExtensions
    {

        public static CustomerDto ToCustomerDto(this Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }
        public static Customer ToCustomer(this CreateCustomerDto createCustomerDto)
        {
            return new Customer
            {
                Name = createCustomerDto.Name,
                Email = createCustomerDto.Email,
                PhoneNumber = createCustomerDto.PhoneNumber
            };
        }
        public static Customer ToCustomer(this UpdateCustomerDto updateCustomerDto, Customer customer)
        {
            customer.Name = updateCustomerDto.Name;
            customer.Email = updateCustomerDto.Email;
            customer.PhoneNumber = updateCustomerDto.PhoneNumber;
            return customer;
        }
        public static IEnumerable<CustomerDto> ToCustomerDtos(this IEnumerable<Customer> customers)
        {
            return customers.Select(c => c.ToCustomerDto());
        }
    }
}
