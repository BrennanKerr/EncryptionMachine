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
            // attempts to set the rotors and reflectors
            try
            {
                Rotors = rotors;
                Reflector = reflector;
            }
            // catches any exceptions and rethrows it
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
            // retireves all the machines rotors
            get { return machineRotors; }
            // sets all the machines rotors
            set
            {
                machineRotors = new Rotor[value.Length];    // sets the length of the rotors array

                // runs through each passed rotor and copies it
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
        public Rotor GetRotorAt(int index) { return machineRotors[index]; }

        /// <summary>
        /// Sets the rotor at the given index
        /// </summary>
        /// <param name="index">the index to change</param>
        /// <param name="newRotor">the settings of the new rotor</param>
        public void SetRotorAt(int index, Rotor newRotor) { machineRotors[index] = newRotor.DeepCopy(); }

        /// <summary>
        /// Gets or sets the machines reflectors
        /// </summary>
        public Reflector Reflector
        {
            // retrieves the reflector used in the machine
            get { return machineReflector; }
            // sets the reflector used in the machine
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
            // rotate the rotor
            machineRotors[index].Rotate(rotations);
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
}
