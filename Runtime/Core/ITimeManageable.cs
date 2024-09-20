namespace MartonioJunior.Flow
{
    /**
    <summary>Interface that describes a temporal object that can be controlled.</summary>
    */
    public interface ITimeManageable: ITime
    {
        // MARK: Methods
        /**
        <summary>Pauses updates in certain states of the object.</summary>
        */
        void Pause();
        /**
        <summary>Resumes updates in certain states of the object.</summary>
        */
        void Resume();
        /**
        <summary>Resets certain states of the object.</summary>
        */
        void Stop();
    }

    #region Default Implementation
    public static partial class ITimeManageableExtensions
    {
        public static void Restart(this ITimeManageable self)
        {
            self.Stop();
            self.Resume();
        }
    }
    #endregion
}