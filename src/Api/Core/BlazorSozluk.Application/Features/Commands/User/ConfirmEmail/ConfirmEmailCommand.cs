using BlazorSozluk.Application;
using BlazorSozluk.Common.Infrastructer.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
public class ConfirmEmailCommand : IRequest<bool>
{
    public Guid ConfirmationId { get; set; }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IUserRepository userRepository;
    private readonly IEmailConfirmationRepository emailConfirmationRepository;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
    {
        this.userRepository = userRepository;
        this.emailConfirmationRepository = emailConfirmationRepository;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmation = await emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

        if (confirmation is null)
            throw new DatabaseValidationException("Mail onaylanmadı!");

        var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == confirmation.NewEmailAddress);

        if (dbUser is null)
            throw new DatabaseValidationException("Bu mail adresine ait kullanıcı bulunamadı!");

        if (dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Bu mail adresi zaten onaylandı!");

        dbUser.EmailConfirmed = true;
        await userRepository.UpdateAsync(dbUser);

        return true;
    }
}