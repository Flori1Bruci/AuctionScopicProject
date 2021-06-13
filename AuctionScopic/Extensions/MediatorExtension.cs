using AuctionScopic.Data;
using AuctionScopic.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionScopic.Extensions
{
    internal static class MediatorExtension
    {
        internal static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker.Entries<BaseEntity>().Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await mediator.Publish(domainEvent);
            });

            await Task.WhenAll(tasks);
        }
    }
}
