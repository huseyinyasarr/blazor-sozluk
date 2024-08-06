using BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav;
using BlazorSozluk.Common.Infrastructer;
using BlazorSozluk.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.Events.Entry;

namespace BlazorSozluk.Application.Features.Commands.Entry.CreateFav;
public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{

    public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryFavQueueName,
            obj: new CreateEntryFavEvent()
            {
                EntryId = request.EntryId.Value,
                CreatedBy = request.UserId.Value
            });

        return Task.FromResult(true);
    }
}
