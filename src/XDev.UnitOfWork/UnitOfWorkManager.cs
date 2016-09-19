namespace XDev.UnitOfWork
{
    using System;
    using System.Threading;

    using Observers;

    /// <summary>
    /// <see cref="UnitOfWorkManager"/>
    /// </summary>
    /// <seealso cref="IInstanceObserver{UnitOfWorkState}"/>
    /// <seealso cref="IUnitOfWorkManager"/>
    internal class UnitOfWorkManager : IUnitOfWorkManager, IInstanceObserver<UnitOfWorkState>
    {
        /// <summary>
        /// The current master unit of work
        /// </summary>
        private readonly AsyncLocal<UnitOfWorkTemplate> currentMasterUnitOfWork;

        /// <summary>
        /// The unit of work factory
        /// </summary>
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        /// <summary>
        /// Begins the unit of work.
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork BeginUnitOfWork()
        {
            return this.BeginUnitOfWork(UnitOfWorkIsolation.ReadCommitted);
        }

        /// <summary>
        /// Begins the unit of work.
        /// </summary>
        /// <param name="unitOfWorkIsolation">The unit of work isolation.</param>
        /// <returns></returns>
        public IUnitOfWork BeginUnitOfWork(UnitOfWorkIsolation unitOfWorkIsolation)
        {
            Guid unitOfWorkId;
            UnitOfWorkTemplate masterUnitOfWork = this.currentMasterUnitOfWork.Value;

            if (masterUnitOfWork == null)
            {
                unitOfWorkId = Guid.NewGuid();

                masterUnitOfWork = this.unitOfWorkFactory.CreateMasterUnitOfWork(unitOfWorkId);

                masterUnitOfWork.Subscribe(this);

                this.currentMasterUnitOfWork.Value = masterUnitOfWork;

                return masterUnitOfWork;
            }

            unitOfWorkId = Guid.Empty;

            UnitOfWorkTemplate unitOfWork = this.unitOfWorkFactory.CreateSlaveUnitOfWork(unitOfWorkId, masterUnitOfWork);

            unitOfWork.Subscribe(masterUnitOfWork);

            return unitOfWork;
        }

        /// <summary>
        /// Receives a notification from a observed instance.
        /// </summary>
        /// <param name="observedInstance">The observed instance.</param>
        /// <param name="message">The message.</param>
        public void ReceiveNotification(object observedInstance, UnitOfWorkState message)
        {
            IUnitOfWork unitOfWork = observedInstance as IUnitOfWork;

            if (object.ReferenceEquals(unitOfWork, this.currentMasterUnitOfWork.Value) && message == UnitOfWorkState.Disposed)
            {
                this.currentMasterUnitOfWork.Value = null;
            }
        }
    }
}