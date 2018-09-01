using Brack.Data.Memory;

namespace Brack.Data.Interfaces
{
    /// <summary>
    /// A Manager for LocalMemory.
    /// </summary>
    interface ILocalMemoryStack
    {
        /// <summary>
        /// The amount of LocalMemories.
        /// </summary>
        int LocalMemoryCount { get; }
        /// <summary>
        /// Push a new LocalMemory.
        /// </summary>
        void PushNewLocalMemory();
        /// <summary>
        /// Remove the last LocalMemory.
        /// </summary>
        void RemoveLastLocalMemory();
        /// <summary>
        /// Get the top LocalMemory.
        /// </summary>
        LocalMemory CurrentLocalMemory { get; }
        /// <summary>
        /// Reset the LocalMemory List.
        /// </summary>
        void ResetLocalMemories();
    }
}
