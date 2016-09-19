namespace XDev.UnitOfWork
{
    using System;

    /// <summary>
    /// <see cref="UnitOfWorkState"/>
    /// </summary>
    public enum UnitOfWorkState
    {
        /// <summary>
        /// The completed
        /// </summary>
        Completed = 1,

        /// <summary>
        /// The disposed
        /// </summary>
        Disposed = 2,

        /// <summary>
        /// The wont complete
        /// </summary>
        WontComplete = 3
    }
}