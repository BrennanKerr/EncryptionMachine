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
        /// <summary>
        /// The name of the reflector
        /// </summary>
        private string reflectorName = "";
        /// <summary>
        /// The reflection that occurs
        /// </summary>
        private string reflectorReflection = "";

        /// <summary>
        /// Creates a new reflector object
        /// </summary>
        /// <param name="name">The reflector name</param>
        /// <param name="reflection">The reflection for the reflector</param>
        public Reflector(string name, string reflection)
        {

        }

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
                reflectorReflection = value;
            }
        }
    }
}
