using AuctionScopic.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace AuctionScopic.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created => DateTime.UtcNow;

        public Guid CreatedBy => (Guid)ClaimsPrincipal.Current.GetUserId();

        public DateTime Updated => DateTime.UtcNow;

        #region domain events
        private List<INotification> _domainEvents;

        [IgnoreDataMember]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        #endregion
    }
}
