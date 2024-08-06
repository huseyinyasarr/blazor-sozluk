using MediatR;
using BlazorSozluk.Common.Events.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.Infrastructer.Exceptions;
using BlazorSozluk.Common.Infrastructer;

namespace BlazorSozluk.Application.Features.Commands.User.ChangePassword;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IUserRepository userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {

        if (!request.UserId.HasValue)
        {
            throw new ArgumentNullException(nameof(request.UserId));
        }

        var dbUser = await userRepository.GetByIdAsync(request.UserId.Value);

        if (dbUser is null)
        {
            throw new DatabaseValidationException("User not found");
        }


        var encPass = PasswordEncryptor.Encrpt(request.OldPassword);
        if (dbUser.Password != encPass)
        {
            throw new DatabaseValidationException("Şifre hatalı");
        }
        dbUser.Password = encPass;

        await userRepository.UpdateAsync(dbUser);

        return true;

    }
}
