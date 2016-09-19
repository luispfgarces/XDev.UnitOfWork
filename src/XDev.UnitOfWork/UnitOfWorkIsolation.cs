namespace XDev.UnitOfWork
{
    using System;

    /// <summary>
    /// <see cref="UnitOfWorkIsolation"/>
    /// </summary>
    public enum UnitOfWorkIsolation
    {
        /// <summary>
        /// The read committed
        /// </summary>
        ReadCommitted = 1,

        /// <summary>
        /// The read uncommitted
        /// </summary>
        ReadUncommitted = 2
    }
}