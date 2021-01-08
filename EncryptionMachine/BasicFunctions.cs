/*
 * @author:     Brennan Kerr
 * @since:      01/07/21
 * @name:       BasicFunctions.cs
 * @desc:       Creates a functions class that contains functions that will be used
 *                  by more then 1 class
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionMachine
{
    /// <summary>
    /// A class used only for commonly used functions
    /// </summary>
    public static class BasicFunctions
    {
        /// <summary>
        /// Returns the index of the letter in the alphabet
        /// </summary>
        /// <param name="c">the character to search for</param>
        /// <returns>the index</returns>
        public static int IndexInAlphabet(char c) { return c.ToString().ToUpper()[0] - 'A'; }

        /// <summary>
        /// Converts the index of the alphabet to the corresponding letter
        /// </summary>
        /// <param name="i">the index in the alphabet</param>
        /// <returns>the corresponding letter</returns>
        public static char LetterAtIndex(int i) { return (char)('A' + i); }

        /// <summary>
        /// Returns a list of dublicated letters. If there are none, an empty list is returned
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>the list of dublicated characters</returns>
        public static List<char> DuplicatedCharacters(string str)
        {
            List<char> chars = new List<char>();

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if (str.Substring(i + 1).Contains(c.ToString()))
                    if (!chars.Contains(c))
                        chars.Add(c);
            }

            return chars;
        }

        /// <summary>
        /// Runs through the list provided to display the error characters
        /// </summary>
        /// <param name="list">the list to display</param>
        /// <returns>the list of error characters</returns>
        public static string RunThroughList(List<char> list)
        {
            string str = "";    // create a new string

            // run through each char in the list
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i] + (i == list.Count - 1 ? "" : ", ");
            }

            str += "\n";

            return str;
        }
    }
}
