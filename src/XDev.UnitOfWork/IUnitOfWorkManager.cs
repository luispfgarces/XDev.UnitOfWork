namespace XDev.UnitOfWork
{
    using System;

    /// <summary>
    /// <see cref="IUnitOfWorkManager"/>
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// Begins the unit of work.
        /// </summary>
        /// <returns></returns>
        IUnitOfWork BeginUnitOfWork();

        /// <summary>
        /// Begins the unit of work.
        /// </summary>
        /// <param name="unitOfWorkIsolation">The unit of work isolation.</param>
        /// <returns></returns>
        IUnitOfWork BeginUnitOfWork(UnitOfWorkIsolation unitOfWorkIsolation);
    }
}