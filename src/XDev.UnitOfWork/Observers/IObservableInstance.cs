namespace XDev.UnitOfWork.Observers
{
    using System;

    /// <summary>
    /// <see cref="IObservableInstance{TMessage}"/>
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    internal interface IObservableInstance<TMessage>
    {
        /// <summary>
        /// Subscribes this instance for updates to the given instance observer.
        /// </summary>
        /// <param name="instanceObserver">The instance observer.</param>
        void Subscribe(IInstanceObserver<TMessage> instanceObserver);
    }
}