using Web.Models;

namespace Web.Data
{
    public static class Users
    {
        public static List<User> Current { get; } = new()
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                StreetName = "Main Street",
                HouseNumber = "1",
                ApartmentNumber = "1",
                PostalCode = "00-000",
                Town = "Town",
                PhoneNumber = "123456789",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(365 *21))),
                Age = 21
            }
        };
    }
}
