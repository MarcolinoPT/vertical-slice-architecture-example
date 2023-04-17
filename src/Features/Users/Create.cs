using Web.Features.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users
{
    [Route("api/users")]
    [ApiController]
    public class CreateController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CreateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<UserResult>> EditUser([FromBody] UserRequest data,
                                                             CancellationToken cancellationToken = new())
        {
            return await _mediator.Send(new Command(data),
                                        cancellationToken);
        }

        internal record Command : IRequest<ActionResult<UserResult>>
        {
            public UserRequest Data { get; init; }
            public Command(UserRequest data)
            {
                Data = data;
            }
        }

        internal class Handler : IRequestHandler<Command, ActionResult<UserResult>>
        {
            public async Task<ActionResult<UserResult>> Handle(Command request,
                                                               CancellationToken cancellationToken)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var dateOfBirth = DateOnly.Parse(request.Data.DateOfBirth);
#pragma warning restore CS8604 // Possible null reference argument.
                Data.Users.Current.Insert(index: 0, new Models.User
                {
                    Id = Guid.NewGuid(),
                    HouseNumber = request.Data.HouseNumber,
                    PhoneNumber = request.Data.PhoneNumber,
                    ApartmentNumber = request.Data.ApartmentNumber,
                    LastName = request.Data.LastName,
                    FirstName = request.Data.FirstName,
                    DateOfBirth = dateOfBirth,
                    Town = request.Data.Town,
                    StreetName = request.Data.StreetName,
                    PostalCode = request.Data.PostalCode,
                    Age = (byte)(DateTime.Now.Year - dateOfBirth.Year)
                });
                return await Task.FromResult(new ActionResult<UserResult>(Data.Users.Current.First()
                                                                                            .ToUserResult()));
            }
        }
    }
}
