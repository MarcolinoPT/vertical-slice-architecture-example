namespace Web.Features.Users
{
    public record UsersResult
    {
        public List<UserResult>? Users { get; init; }
        public int Count { get; init; }
    }
}
