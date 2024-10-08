﻿using AutoMapper;
using BlazorSozluk.Application;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.Create;
public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Guid>
{
    private readonly IEntryRepository entryRepository;
    private readonly IMapper mapper;

    public CreateEntryCommandHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        this.entryRepository = entryRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        var dbEntry = mapper.Map<Domain.Models.Entry>(request);

        await entryRepository.AddAsync(dbEntry);

        return dbEntry.Id;
    }
}