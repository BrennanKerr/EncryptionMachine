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
        #region FIELDS

        /// <summary>
        /// Defines a list of the rotors that will be used in the machine
        /// </summary>
        private Rotor[] machineRotors;

        /// <summary>
        /// Defines the reflector that will be used
        /// </summary>
        private Reflector machineReflector;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor - Initializes a new machine
        /// </summary>
        /// <param name="rotors">the list of rotors that will be used</param>
        /// <param name="reflector">the reflector that will be used</param>
        public Machine(Rotor[] rotors, Reflector reflector)
        {
            try
            {
                Rotors = rotors;
                Reflector = reflector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PROPERTIES

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
                machineRotors = new Rotor[value.Length];

                for (int i = 0; i < machineRotors.Length; i++)
                {
                    machineRotors[i] = value[i].DeepCopy();
                }
            }
        }

        /// <summary>
        /// Retrieves the rotor at the given index
        /// </summary>
        /// <param name="index">the index of the desired rotor</param>
        /// <returns>the rotor at the given index</returns>
        public Rotor GetRotor(int index)
        {
            return machineRotors[index];
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

        #endregion

        #region METHODS

        /// <summary>
        /// Rotates the rotor at the given index
        /// </summary>
        /// <param name="index">the idnex of the rotor</param>
        /// <param name="rotations">the number of rotations</param>
        public void RotateRotor(int index = 0, int rotations = 1)
        {
            if (index >= 0 && index < machineRotors.Length)
            {
                machineRotors[index].Rotate(rotations);
            }
            else
            {
                throw new IndexOutOfRangeException("The index selected is out of range");
            }
        }
        #endregion

        #region OVERRIDES

        /// <summary>
        /// Returns the machine as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "Machine Information: \n";

            str += "ROTORS \n\n";

            for (int i = 0; i < machineRotors.Length; i++)
            {
                str += machineRotors[i].ToString() + "\n\n";
            }

            str += "Reflector: \n\n" + machineReflector.ToString();

            return str;
        }
        #endregion
    }


    /// <summary>
    /// Delegate function used to allow user to define their own encryption
    /// method
    /// </summary>
    /// <param name="character">the character to encrypt</param>
    /// <returns>the encrypted character</returns>
    public delegate char EncryptionMethod(char character);
}
