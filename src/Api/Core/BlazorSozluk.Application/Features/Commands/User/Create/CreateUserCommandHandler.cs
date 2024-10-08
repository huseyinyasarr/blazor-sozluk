﻿using AutoMapper;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructer;
using BlazorSozluk.Common.Infrastructer.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (existsUser is not null)
            throw new DatabaseValidationException("Bu kullanıcı zaten bulunuyor!");

        var dbUser = mapper.Map<BlazorSozluk.Api.Domain.Models.User>(request);

        var rows = await userRepository.AddAsync(dbUser);

        // Email Changed/Created
        if (rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = dbUser.EmailAddress
            };

            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.UserEmailChangedQueueName,
                                               obj: @event);
        }

        return dbUser.Id;
    }
}


//public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
//{
//    private readonly IMapper mapper;
//    private readonly IUserRepository userRepository;

//    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
//    {
//        this.mapper = mapper;
//        this.userRepository = userRepository;
//    }

//    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
//    {
//        var existsUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

//        if (existsUser != null)
//        {
//            throw new DatabaseValidationException("Böyle bir kullanıcı bulunmaktadır.");

//            var dbUser = mapper.Map<BlazorSozluk.Api.Domain.Models.User>(request);

//            var rows = await userRepository.AddAsync(dbUser); //db'ye kadedilip kaydedilmediği bilgisini öğreniriz

//            // mail değişti/oluşturuldu

//            if (rows >0 )
//            {
//                var @event = new UserEmailChangedEvent()
//                {
//                    OldEmailAddress = null,
//                    NewEmailAddress = dbUser.EmailAddress
//                };

//                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
//                                                   exchangeType: SozlukConstants.DefaultExchangeType,
//                                                   queueName: SozlukConstants.UserEmailChangedQueueName,
//                                                   obj: @event);
//            }

//            return dbUser.Id;

//        }




//    }
//}
