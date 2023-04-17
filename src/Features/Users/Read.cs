using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users.GetUsers
{
    [Route("api/users")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndexController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<UsersResult>> GetAllUsers(CancellationToken cancellationToken = new())
        {
            return await _mediator.Send(new Query(), cancellationToken);
        }
        internal record Query : IRequest<ActionResult<UsersResult>>
        {
        }

        internal class Handler : IRequestHandler<Query, ActionResult<UsersResult>>
        {
            public async Task<ActionResult<UsersResult>> Handle(Query request,
                                                                CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }
                return await Task.FromResult(new ActionResult<UsersResult>(new UsersResult
                {
                    Count = Data.Users.Current.Count,
                    Users = Data.Users.Current.Select(user => new UserResult
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        StreetName = user.StreetName,
                        HouseNumber = user.HouseNumber,
                        ApartmentNumber = user.ApartmentNumber,
                        PostalCode = user.PostalCode,
                        Town = user.Town,
                        DateOfBirth = user.DateOfBirth,
                        Age = user.Age,
                        PhoneNumber = user.PhoneNumber
                    }).ToList()
                }));
            }
        }
    }
}
