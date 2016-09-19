namespace XDev.UnitOfWork.Observers
{
    using System;

    /// <summary>
    /// <see cref="IInstanceObserver{TMessage}"/>
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public interface IInstanceObserver<TMessage>
    {
        /// <summary>
        /// Receives a notification from a observed instance.
        /// </summary>
        /// <param name="observedInstance">The observed instance.</param>
        /// <param name="message">The message.</param>
        void ReceiveNotification(object observedInstance, TMessage message);
    }
}