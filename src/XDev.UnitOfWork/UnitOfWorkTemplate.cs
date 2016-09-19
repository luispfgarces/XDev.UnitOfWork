namespace XDev.UnitOfWork
{
    using System;
    using System.Collections.Generic;

    using Observers;

    /// <summary>
    /// <see cref="UnitOfWorkTemplate"/>
    /// </summary>
    /// <seealso cref="IObservableInstance{UnitOfWorkState}"/>
    /// <seealso cref="IInstanceObserver{UnitOfWorkState}"/>
    /// <seealso cref="IUnitOfWork"/>
    public class UnitOfWorkTemplate : IUnitOfWork, IObservableInstance<UnitOfWorkState>, IInstanceObserver<UnitOfWorkState>
    {
        /// <summary>
        /// The observers
        /// </summary>
        private readonly List<IInstanceObserver<UnitOfWorkState>> observers;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkTemplate"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected UnitOfWorkTemplate(Guid id)
        {
            this.Id = id;
            this.IsCompleted = false;
            this.WontComplete = false;

            this.observers = new List<IInstanceObserver<UnitOfWorkState>>(0);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is completed.
        /// </summary>
        /// <value><c>true</c> if this instance is completed; otherwise, <c>false</c>.</value>
        protected bool IsCompleted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this unit of work won't complete.
        /// </summary>
        /// <value><c>true</c> if this unit of work won't complete; otherwise, <c>false</c>.</value>
        protected bool WontComplete { get; private set; }

        /// <summary>
        /// Completes the unit of work.
        /// </summary>
        public void Complete()
        {
            this.IsCompleted = true;
            this.SendMessageToObservers(UnitOfWorkState.Completed);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            this.SendMessageToObservers(UnitOfWorkState.Disposed);
        }

        /// <summary>
        /// Receives a notification from a observed instance.
        /// </summary>
        /// <param name="observedInstance">The observed instance.</param>
        /// <param name="message">The message.</param>
        public void ReceiveNotification(object observedInstance, UnitOfWorkState message)
        {
            IUnitOfWork unitOfWork = observedInstance as IUnitOfWork;
            if (unitOfWork != null && message == UnitOfWorkState.WontComplete)
            {
                this.WontComplete = true;
            }
        }

        /// <summary>
        /// Subscribes this instance for updates to the given instance observer.
        /// </summary>
        /// <param name="instanceObserver">The instance observer.</param>
        public void Subscribe(IInstanceObserver<UnitOfWorkState> instanceObserver)
        {
            if (!this.observers.Contains(instanceObserver))
            {
                this.observers.Add(instanceObserver);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Sends a message to observers.
        /// </summary>
        /// <param name="unitOfWorkState">the state of the unit of work.</param>
        private void SendMessageToObservers(UnitOfWorkState unitOfWorkState)
        {
            foreach (var observer in this.observers)
            {
                observer.ReceiveNotification(this, unitOfWorkState);
            }
        }
    }
}