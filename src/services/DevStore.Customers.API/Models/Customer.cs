using System;
using DevStore.Core.DomainObjects;

namespace DevStore.Customers.API.Models;

public class Customer : Entity, IAggregateRoot
{
    // EF Relation
    protected Customer()
    {
    }

    public Customer(Guid id, string name, string email, string ssn)
    {
        Id = id;
        Name = name;
        Email = new Email(email);
        SocialNumber = ssn;
        Deleted = false;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string SocialNumber { get; private set; }
    public bool Deleted { get; private set; }
    public Address Address { get; private set; }

    public void TrocarEmail(string email)
    {
        Email = new Email(email);
    }

    public void AtribuirEndereco(Address address)
    {
        Address = address;
    }
}