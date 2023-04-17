using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Features.Users
{
    [Route("api/users")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeleteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteUser(Guid id,
                                                   CancellationToken cancellationToken = new())
        {
            return await _mediator.Send(new Command(id),
                                        cancellationToken);
        }

        internal record Command : IRequest<ActionResult>
        {
            public Guid Id { get; init; }
            public Command(Guid id)
            {
                Id = id;
            }
        }

        internal class Handler : IRequestHandler<Command, ActionResult>
        {
            public async Task<ActionResult> Handle(Command request,
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
                if (Data.Users.Current.Remove(userFound))
                {
                    return await Task.FromResult(new NoContentResult());
                }
                throw new Exception("Failed to delete user");
            }
        }
    }
}
