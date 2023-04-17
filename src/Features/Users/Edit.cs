using Web.Features.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users
{
    [Route("api/users")]
    [ApiController]
    public class EditController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserResult>> EditUser(Guid id,
                                                             [FromBody] UserRequest data,
                                                             CancellationToken cancellationToken = new())
        {
            return await _mediator.Send(new Command(id, data),
                                        cancellationToken);
        }

        internal record Command : IRequest<ActionResult<UserResult>>
        {
            public Guid Id { get; init; }
            public UserRequest Data { get; init; }
            public Command(Guid id,
                           UserRequest data)
            {
                Id = id;
                Data = data;
            }
        }

        internal class Handler : IRequestHandler<Command, ActionResult<UserResult>>
        {
            public async Task<ActionResult<UserResult>> Handle(Command request,
                                                               CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }
                var userFound = Data.Users.Current.FirstOrDefault(u => u.Id == request.Id);
                if (userFound is null)
                {
                    return await Task.FromResult(new NotFoundResult());
                }
                userFound.HouseNumber = request.Data.HouseNumber;
                userFound.PhoneNumber = request.Data.PhoneNumber;
                userFound.ApartmentNumber = request.Data.ApartmentNumber;
                userFound.LastName = request.Data.LastName;
                userFound.FirstName = request.Data.FirstName;
                userFound.LastName = request.Data.LastName;
#pragma warning disable CS8604 // Possible null reference argument.
                userFound.DateOfBirth = DateOnly.Parse(request.Data.DateOfBirth);
#pragma warning restore CS8604 // Possible null reference argument.
                userFound.Town = request.Data.Town;
                userFound.StreetName = request.Data.StreetName;
                userFound.PostalCode = request.Data.PostalCode;
                userFound.Age = (byte)(DateTime.Now.Year - userFound.DateOfBirth.Year);
                return await Task.FromResult(new ActionResult<UserResult>(userFound.ToUserResult()));
            }
        }
    }
}

