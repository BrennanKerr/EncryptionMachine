/*
 * @author:     Brennan Kerr
 * @since:      01/07/21
 * @name:       InvalidReflector.cs
 * @desc:       Creates the invalid reflector settings class to be used whenever
 *                  there is an error in the reflection
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionMachine
{
    /// <summary>
    /// InvalidReflector - Used to handle any errors with the reflector
    /// </summary>
    public class InvalidReflectorException : Exception
    {
        #region FIELDS

        /// <summary>
        /// The reflection string
        /// </summary>
        private string reflection = "";
        /// <summary>
        /// The list of duplicate values in the reflection
        /// </summary>
        private List<char> duplicates;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new InvalidReflector exception
        /// </summary>
        /// <param name="reflectorReflection">the reflection</param>
        /// <param name="duplicateList">the duplicated list</param>
        public InvalidReflectorException(string reflectorReflection, List<char> duplicateList)
        {
            reflection = reflectorReflection;
            duplicates = duplicateList;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Retrieves the reflection stored in the exception
        /// </summary>
        public string Reflection
        {
            get { return reflection; }
        }

        /// <summary>
        /// Retrieves the list of duplicate values
        /// </summary>
        public List<char> Duplicates
        {
            get { return duplicates; }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Overrides the Message property to create a list of errors
        /// </summary>
        public override string Message
        {
            get
            {
                string str = "Invalid Reflector " + Reflection + ":\nDuplicates: ";
                str += BasicFunctions.RunThroughList(Duplicates);
                return str;
            }
        }

        #endregion
    }
}
