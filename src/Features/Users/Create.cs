using Ardalis.Result;
using Ardalis.Result.AspNetCore;
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

        [TranslateResultToActionResult]
        [HttpPost]
        public async Task<Result<UserResult>> CreateUser([FromBody] UserRequest data,
                                                         CancellationToken cancellationToken = new())
        {
            return await _mediator.Send(new Command(data),
                                              cancellationToken);
        }

        internal record Command : IRequest<Result<UserResult>>
        {
            public UserRequest Data { get; init; }
            public Command(UserRequest data)
            {
                Data = data;
            }
        }

        internal class Handler : IRequestHandler<Command, Result<UserResult>>
        {
            public async Task<Result<UserResult>> Handle(Command request,
                                                         CancellationToken cancellationToken)
            {
                var dateOfBirth = DateOnly.Parse(request.Data.DateOfBirth);
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
                return await Task.FromResult(Data.Users.Current.First()
                                                               .ToUserResult());
            }
        }
    }
}
