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
                for (int i = 0; i < value.Length; i++)
                {
                    string c = value[i].ToString();
                    if (value.Substring(i + 1, value.Length - i - 1).Contains(c))
                    {
                        throw new Exception("There is a dublicate letter: " + c);
                    }
                }

                reflectorReflection = value;
            }
        }

        #endregion

        #region METHODS

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
