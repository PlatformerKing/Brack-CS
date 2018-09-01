using System.Collections.Generic;
using System.IO;

namespace Brack.Interpretation
{
    /// <summary>
    /// A utilty class for parsing Brack from strings.
    /// </summary>
    public class BrackParser
    {
        #region Static Utility Methods
        /// <summary>
        /// Parse a raw string to Brack.
        /// </summary>
        /// <param name="raw">The raw string to parse.</param>
        /// <returns>The resulting Brack.</returns>
        public static object[] ParseString(string raw)
        {
            List<object> ret = new List<object>(), curStatement = null;
            int level = 0;
            bool inComment = false, inString = false;
            string tok = "";
            for (var i = 0; i < raw.Length; i++)
            {
                char cur = raw[i], bef = (i == 0) ? ' ' : raw[i - 1];
                if (cur == '#' && bef != '\\')
                {
                    inComment = !inComment;
                }
                else if (!inComment)
                {
                    if (inString)
                    {
                        if (cur == '"' && bef != '\\')
                        {
                            inString = false;
                            if (curStatement != null)
                            {
                                curStatement.Add(tok);
                                tok = "";
                            }
                        }
                        else
                        {
                            tok += cur;
                        }
                    }
                    else if (cur == '\"' && bef != '\\')
                    {
                        inString = true;
                    }
                    else if (level == 1)
                    {
                        if (cur == ']' && bef != '\\')
                        {
                            level = 0;
                            if (tok != "")
                            {
                                curStatement.Add(ParseObj(tok));
                                tok = "";
                            }
                            ret.Add(curStatement.ToArray());
                            curStatement = null;
                        }
                        else if (cur == '[' && bef != '\\')
                        {
                            level = 2;
                            if (tok != "")
                            {
                                curStatement.Add(ParseObj(tok));
                            }
                            tok = cur.ToString();
                        }
                    }
                    else if (level > 1)
                    {
                        tok += cur;
                        if (cur == '[' && bef != '\\')
                        {
                            level++;
                        }
                        else if (cur == ']' && bef != '\\')
                        {
                            level--;
                            if (level == 1)
                            {
                                curStatement.Add(ParseString(tok));
                                tok = "";
                            }
                        }
                    }
                    else if (cur == '[' && bef != '\\')
                    {
                        level = 1;
                        curStatement = new List<object>();
                    }
                }
            }
            return ret.ToArray();
        }
        internal static object ParseObj(string raw)
        {
            if (float.TryParse(raw, out float f))
            {
                return f;
            }
            return raw;
        }
        #endregion
        #region Properties
        /// <summary>
        /// The representation for an invalid index.
        /// </summary>
        public const int NullIndex = -1;
        /// <summary>
        /// The raw string.
        /// </summary>
        public string Raw { get; private set; }
        /// <summary>
        /// The current index.
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// The index of the last Brack statement.
        /// </summary>
        public int LastIndex { get; private set; }
        /// <summary>
        /// Is there another Brack statement?
        /// </summary>
        public bool HasNext { get { return Index <= LastIndex && !IsEmpty; } }
        /// <summary>
        /// Is the raw string devoid of Brack?
        /// </summary>
        public bool IsEmpty { get { return LastIndex == NullIndex; } }
        #endregion
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="raw">The raw string.</param>
        public BrackParser(string raw)
        {
            Raw = raw;
            Reset();
            LastIndex = NullIndex;
            bool inComment = false, inString = false;
            int level = 0;
            for (var i = 0; i < raw.Length; i++)
            {
                char cur = Raw[i], bef = (i == 0) ? ' ' : Raw[i - 1];

                if (cur == '#' && bef != '\\')
                {
                    inComment = !inComment;
                }
                else if (!inComment)
                {
                    if (cur == '\"' && bef != '\\')
                    {
                        inString = !inString;
                    }
                    else if (!inString)
                    {
                        if (cur == '[' && bef != '\\')
                        {
                            if (level == 0)
                            {
                                LastIndex = i;
                            }
                            level++;
                        }
                        else if (cur == ']' && bef != '\\')
                        {
                            level--;
                        }
                    }
                }
            }
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get the next Brack statement.
        /// </summary>
        /// <returns></returns>
        public object[] Next()
        {
            int level = 0;
            bool inComment = false, inString = false;
            string tok = "";
            for (; HasNext; Index++)
            {
                char cur = Raw[Index], bef = (Index == 0) ? ' ' : Raw[Index - 1];
                if (cur == '#' && bef != '\\')
                {
                    inComment = !inComment;
                }
                else if (!inComment)
                {
                    if (level == 0 && cur == '\"' && bef != '\\')
                    {
                        inString = !inString;
                    }
                    else
                    {
                        tok += cur;
                        if (cur == '\"' && bef != '\\')
                        {
                            inString = !inString;
                        }
                        else if (!inString)
                        {
                            if (cur == '[' && bef != '\\')
                            {
                                level++;
                            }
                            else if (cur == ']' && bef != '\\')
                            {
                                level--;
                                if (level == 0)
                                {
                                    return ParseString(tok);
                                }
                            }
                        }
                    }
                }
            }
            throw new IOException();
        }
        /// <summary>
        /// Get all brack from the raw string.
        /// </summary>
        /// <returns></returns>
        public object[] All()
        {
            return ParseString(Raw);
        }
        /// <summary>
        /// Reset the index.
        /// </summary>
        public void Reset()
        {
            Index = 0;
        }
        #endregion
    }
}
