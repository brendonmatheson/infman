namespace cc.bren.infman.infrastructure
{
    using System;

    /// <summary>
    /// Represents an instance of a HostSpec that has been deployed to an Infrastructure.
    /// </summary>
    public interface HostInstanceEntity
    {
        Guid HostInstanceId { get; }

        string Name { get; }

        Guid HostSpecId { get; }

        Guid InfrastructureId { get; }
    }
}
