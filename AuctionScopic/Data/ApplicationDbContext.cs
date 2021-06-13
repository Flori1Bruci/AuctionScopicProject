using AuctionScopic.Domain;
using AuctionScopic.Extensions;
using AuctionScopic.Models;
using IdentityServer4.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionScopic.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        private readonly IMediator _mediator;
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions, IMediator mediator) : base(options, operationalStoreOptions)
        {
            _mediator = mediator;
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<AutoBid> AutoBids { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            if (_mediator != null)
                await _mediator.DispatchDomainEventsAsync(this);

            return result;
        }
    }
}
