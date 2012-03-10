﻿//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// A &quot;glob&quot; is a string matching pattern. It is similar to the
    /// matches available in the file system (*.cs, for example). The Glob
    /// class implements this string matching.
    /// </summary>
    /// <remarks>Glob supports the following metacharacters:
    ///     * - match zero or more characters
    ///     ? - match any one character
    /// [abc] - match one character if it's in the characters inside the brackets.
    /// All other characters in the glob are literals.
    /// </remarks>
    public class Glob
    {
        private readonly Regex pattern;

        /// <summary>
        /// Constructs a new <see cref="Glob"/> instance that matches the given pattern.
        /// </summary>
        /// <remarks>
        /// The pattern match is case sensitive by default.
        /// </remarks>
        /// <param name="pattern">Pattern to use. See <see cref="Glob"/> summary for
        /// details of the pattern.</param>
        public Glob(string pattern)
            : this(pattern, true)
        {
        }

        /// <summary>
        /// Constructs a new <see cref="Glob"/> instance that matches the given pattern.
        /// </summary>
        /// <param name="pattern">The pattern to use. See <see cref="Glob"/> summary for
        /// details of the patterns supported.</param>
        /// <param name="caseSensitive">If true, perform a case sensitive match. 
        /// If false, perform a case insensitive comparison.</param>
        public Glob(string pattern, bool caseSensitive)
        {
            this.pattern = GlobPatternToRegex(pattern, caseSensitive);
        }

        /// <summary>
        /// Checks to see if the given string matches the pattern.
        /// </summary>
        /// <param name="s">String to check.</param>
        /// <returns>True if it matches, false if it doesn't.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public bool IsMatch(string s)
        {
            return pattern.IsMatch(s);
        }

        private static Regex GlobPatternToRegex(string pattern, bool caseSensitive)
        {
            StringBuilder regexPattern = new StringBuilder(pattern);

            string[] globLiterals = new string[] { "\\", ".", "$", "^", "{", "(", "|", ")", "+" };
            foreach (string globLiteral in globLiterals)
            {
                regexPattern.Replace(globLiteral, @"\" + globLiteral);
            }
            regexPattern.Replace("*", ".*");
            regexPattern.Replace("?", ".");

            regexPattern.Insert(0, "^");
            regexPattern.Append("$");
            RegexOptions options = caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
            return new Regex(regexPattern.ToString(), options);
        }
    }
}
