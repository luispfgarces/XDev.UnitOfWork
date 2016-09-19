namespace XDev.UnitOfWork
{
    using System;

    /// <summary>
    /// <see cref="IUnitOfWorkFactory"/>
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates the master unit of work.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UnitOfWorkTemplate CreateMasterUnitOfWork(Guid id);

        /// <summary>
        /// Creates the slave unit of work.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="masterUnitOfWork">The master unit of work.</param>
        /// <returns></returns>
        UnitOfWorkTemplate CreateSlaveUnitOfWork(Guid id, IUnitOfWork masterUnitOfWork);
    }
}