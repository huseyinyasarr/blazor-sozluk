﻿using AutoMapper;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructer;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Infrastructer.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Features.Commands.User.Update;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await userRepository.GetByIdAsync(request.Id);

        if (dbUser is null)
            throw new DatabaseValidationException("User not found!");

        var dbEmailAddress = dbUser.EmailAddress;
        var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

        mapper.Map(request, dbUser);

        var rows = await userRepository.UpdateAsync(dbUser);

        if (emailChanged && rows > 0)
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

            dbUser.EmailConfirmed = false;
            await userRepository.UpdateAsync(dbUser);
        }

        return dbUser.Id;
    }
}


//public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid> 
//{

//    private readonly IMapper mapper;
//    private readonly IUserRepository userRepository;

//    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
//    {
//        this.mapper = mapper;
//        this.userRepository = userRepository;
//    }

//    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
//    {
//        var dbUser = await userRepository.GetByIdAsync(request.Id);

//        if (dbUser == null)
//        {
//            throw new DatabaseValidationException("Kullanıcı bulunamadı");
//        }

//        var dbEmailAddress = dbUser.EmailAddress;
//        var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

//        mapper.Map(request, dbUser);

//        var rows = await userRepository.UpdateAsync(dbUser);

//        // eğer kullancıı değişmişse

//        if (emailChanged && rows > 0)
//        {
//            var @event = new UserEmailChangedEvent()
//            {
//                OldEmailAddress = null,
//                NewEmailAddress = dbUser.EmailAddress
//            };

//            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
//                                               exchangeType: SozlukConstants.DefaultExchangeType,
//                                               queueName: SozlukConstants.UserEmailChangedQueueName,
//                                               obj: @event);

//            dbUser.EmailConfirmed = false; // çünkü yeni mail'i de confirmed yapması gerekiyor
//            await userRepository.UpdateAsync(dbUser);

//        }

//    }
//}
