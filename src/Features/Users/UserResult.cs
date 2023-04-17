namespace Web.Features.Users
{
    public record UserResult
    {
        public Guid Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? StreetName { get; init; }
        public string? HouseNumber { get; init; }
        public string? ApartmentNumber { get; init; }
        public string? PostalCode { get; init; }
        public string? Town { get; init; }
        public string? PhoneNumber { get; init; }
        public DateOnly DateOfBirth { get; init; }
        public byte Age { get; init; }
    }

    public static class UserResultMapper
    {
        public static UserResult ToUserResult(this Models.User user)
        {
            return new UserResult
            {
                Age = user.Age,
                ApartmentNumber = user.ApartmentNumber,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                HouseNumber = user.HouseNumber,
                Id = user.Id,
                LastName = user.LastName,
                PostalCode = user.PostalCode,
                PhoneNumber = user.PhoneNumber,
                StreetName = user.StreetName,
                Town = user.Town,
            };
        }
    }
}
