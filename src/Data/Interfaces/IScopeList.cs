using Brack.Data.Memory;

namespace Brack.Data.Interfaces
{
    /// <summary>
    /// A manager for Scopes.
    /// </summary>
    interface IScopeList
    {
        /// <summary>
        /// The amount of Scopes.
        /// </summary>
        int ScopeCount { get; }
        /// <summary>
        /// Add a new Scope.
        /// </summary>
        void AddScope();
        /// <summary>
        /// Delete the top Scope.
        /// </summary>
        void DeleteTopScope();
        /// <summary>
        /// Peek the top Scope.
        /// </summary>
        Scope TopScope { get; }
        /// <summary>
        /// Reset the Sope Stack.
        /// </summary>
        void ResetScopes();
    }
}
