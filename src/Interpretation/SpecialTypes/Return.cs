namespace Brack.Interpretation
{
    /// <summary>
    /// The Return data of an Execute call.
    /// </summary>
    public class Return
    {
        #region Private Properties
        /// <summary>
        /// The data of this Return.
        /// </summary>
        private object _ReturnData;
        #endregion

        #region Immutable Properties
        /// <summary>
        /// The data of this Return.
        /// </summary>
        public object ReturnData
        {
            get
            {
                return _ReturnData;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="data"></param>
        public Return(object data)
        {
            _ReturnData = data;
        }
        #endregion
    }
}
