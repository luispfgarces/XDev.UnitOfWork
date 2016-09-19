namespace XDev.UnitOfWork
{
    using System;

    /// <summary>
    /// <see cref="IUnitOfWork"/>
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; }

        /// <summary>
        /// Completes the unit of work.
        /// </summary>
        void Complete();
    }
}