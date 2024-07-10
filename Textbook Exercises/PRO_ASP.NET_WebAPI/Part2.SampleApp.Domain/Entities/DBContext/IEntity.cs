using System;

namespace PingYourPackage.Domain
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }
}
