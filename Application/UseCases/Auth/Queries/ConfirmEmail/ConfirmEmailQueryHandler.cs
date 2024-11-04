using Application.Abstractions;
using Application.Abstractions.AuthUser;
using Application.UseCases.Auth;
using Application.UseCases.DomainUsers;
using Domain.DomainUsers;
using MediatR;

namespace Application.UseCases.Auth.Queries.ConfirmEmail
{
    internal class ConfirmEmailQueryHandler : IRequestHandler<ConfirmEmailQuery, Result<ConfirmEmailQueryResponse>>
    {
        private readonly IAuthService _authService;
        private readonly IAuthUserService _authUserService;
        private readonly IDomainUserRepository _userRepository;

        public ConfirmEmailQueryHandler(IAuthService authService, IAuthUserService authUserService, IDomainUserRepository gardenerRepository)
        {
            _authService = authService;
            _authUserService = authUserService;
            _userRepository = gardenerRepository;
        }

        public async Task<Result<ConfirmEmailQueryResponse>> Handle(ConfirmEmailQuery query, CancellationToken cancellationToken)
        {
            AuthUserDTO? user = await _authUserService.FindByIdAsync(query.id);

            if (user == null)
                return Result<ConfirmEmailQueryResponse>.Failure(AuthErrors.EmailConfirmationFailed());

            ConfirmEmailQueryResponse? response = await _authService.ConfirmEmailAsync(user.Email, query.code);

            if (response == null)
                return Result<ConfirmEmailQueryResponse>.Failure(AuthErrors.EmailConfirmationFailed());

            _userRepository.Add(new DomainUser(new DomainUserId(query.id)));

            return Result<ConfirmEmailQueryResponse>.Success(response);
        }
    }
}
