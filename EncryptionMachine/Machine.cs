/*
 * @author:     Brennan Kerr
 * @since:      01/06/21
 * @name:       Machine.cs
 * @desc:       Creates the machine class
 */

using System;

namespace EncryptionMachine
{
    /// <summary>
    /// The machine class is used to create an instance of an
    /// encryption machine.
    /// </summary>
    public class Machine
    {
        /// <summary>
        /// Defines a list of the rotors that will be used in the machine
        /// </summary>
        private Rotor[] machineRotors;

        /// <summary>
        /// Defines the reflector that will be used
        /// </summary>
        private Reflector machineReflector;

        /// <summary>
        /// Constructor - Initializes a new machine
        /// </summary>
        /// <param name="rotors">the list of rotors that will be used</param>
        /// <param name="reflector">the reflector that will be used</param>
        public Machine(Rotor[] rotors, Reflector reflector)
        {
            Rotors = rotors;
            Reflector = reflector;
        }

        /// <summary>
        /// Gets or sets the machines rotors
        /// </summary>
        public Rotor[] Rotors
        {
            get
            {
                return machineRotors;
            }
            set
            {
                machineRotors = value;
            }
        }

        /// <summary>
        /// Gets or sets the machines reflectors
        /// </summary>
        public Reflector Reflector
        {
            get
            {
                return machineReflector;
            }
            set
            {
                machineReflector = value;
            }
        }

        /// <summary>
        /// Delegate function used to allow user to define their own encryption
        /// method
        /// </summary>
        /// <param name="character">the character to encrypt</param>
        /// <returns>the encrypted character</returns>
        public delegate char EncyptCharacter(char character);
    }
}
