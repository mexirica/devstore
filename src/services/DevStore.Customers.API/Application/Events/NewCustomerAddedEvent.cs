using System;
using DevStore.Core.Messages;

namespace DevStore.Customers.API.Application.Events;

public class NewCustomerAddedEvent : Event
{
    public NewCustomerAddedEvent(Guid id, string name, string email, string socialNumber)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Email = email;
        SocialNumber = socialNumber;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string SocialNumber { get; private set; }
}