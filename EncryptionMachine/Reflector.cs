/*
 * @author:     Brennan Kerr
 * @since:      01/06/21
 * @name:       Reflector.cs
 * @desc:       Creates the reflector class
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionMachine
{
    /// <summary>
    /// The reflector class is used to create an instance of a reflector
    /// that can be used in an encryption machine
    /// </summary>
    public class Reflector
    {
        #region FIELDS

        /// <summary>
        /// The name of the reflector
        /// </summary>
        private string reflectorName = "";
        /// <summary>
        /// The reflection that occurs
        /// </summary>
        private string reflectorReflection = "";

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new reflector object
        /// </summary>
        /// <param name="name">The reflector name</param>
        /// <param name="reflection">The reflection for the reflector</param>
        public Reflector(string name, string reflection)
        {
            ReflectorName = name;

            try
            {
                ReflectorReflection = reflection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PROPERTIES
        /// <summary>
        /// Gets or sets the reflector
        /// </summary>
        public string ReflectorName
        {
            get
            {
                return reflectorName;
            }
            set
            {
                reflectorName = value;
            }
        }

        /// <summary>
        /// Gets or sets the reflection
        /// </summary>
        public string ReflectorReflection
        {
            get
            {
                return reflectorReflection;
            }
            set
            {
                List<char> duplicates = BasicFunctions.DuplicatedCharacters(value);
                if (duplicates.Count > 0)
                    throw new InvalidReflectorException(value, duplicates);


                reflectorReflection = value;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Gets the character that corresponds to the letter
        /// </summary>
        /// <param name="c">the letter to reflect</param>
        /// <returns>the reflected character</returns>
        public char ReflectionCharacter(char c)
        {
            return ReflectorReflection[BasicFunctions.IndexInAlphabet(c)];
        }

        #endregion

        #region OVERRIDES

        /// <summary>
        /// Returns the reflector as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "Reflector Name: " + ReflectorName + "\n";
            str += "Reflector Reflection: " + ReflectorReflection;

            return str;
        }

        #endregion
    }
}
