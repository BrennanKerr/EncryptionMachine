/*
 * @author:     Brennan Kerr
 * @since:      01/06/21
 * @name:       Machine.cs
 * @desc:       Creates the machine class
 */

using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Defines the plugboard that will be used
        /// </summary>
        private Plugboard machinePlugboard;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor - Initializes a new machine
        /// </summary>
        /// <param name="rotors">the list of rotors that will be used</param>
        /// <param name="reflector">the reflector that will be used</param>
        /// <param name="plugboard">The plugboard that will be used</param>
        public Machine(Rotor[] rotors, Reflector reflector, Plugboard plugboard)
        {
            // attempts to set the rotors and reflectors
            try
            {
                CheckMachineParameters(rotors, reflector, plugboard);

                SetRotors(rotors);
                SetReflector(reflector);
                SetPlugboard(plugboard);
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
                // if no rotors are passed
                if (value.Length <= 0)
                    throw new Exception("There must be atleast 1 rotor set");

                // Check for errors in the rotors
                Dictionary<int, List<char>> errors = CompareRotors(value);

                // if there are errors
                if (errors.Count > 0)
                    throw new RotorComparisionException(machineRotors, machineReflector, machinePlugboard, 
                        errors, new List<char>(), new List<char>());
                // otherwise set the rotors
                else
                    SetRotors(value);
            }
        }

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
                // check for errors with the reflecor
                List<char> errors = CheckReflector(machineRotors[0], value);

                // if there are errors
                if (errors.Count > 0)
                    throw new RotorComparisionException(machineRotors, machineReflector, machinePlugboard,
                        new Dictionary<int, List<char>>(), errors, new List<char>());
                // otherwise set it
                else
                    SetReflector(value);
            }
        }

        /// <summary>
        /// Gets or sets the plugboard
        /// </summary>
        public Plugboard Plugboard
        {
            get { return machinePlugboard; }
            set
            {
                List<char> errors = CheckPlugboard(machineRotors[0], value);

                if (errors.Count > 0)
                    throw new RotorComparisionException(Rotors, Reflector, value,
                        new Dictionary<int, List<char>>(), new List<char>(), errors);
                else SetPlugboard(value);
            }
        }

        #endregion

        #region METHODS

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
        public void SetRotorAt(int index, Rotor newRotor)
        {
            List<char> errorCheck = CheckRotor(machineRotors[0], newRotor);

            if (errorCheck.Count > 0)
            {
                // creates a temporary array used for the exception
                Rotor[] temp = new Rotor[] { machineRotors[0], newRotor };
                // creates a dictionary to store the information
                Dictionary<int, List<char>> errors = new Dictionary<int, List<char>>();
                errors.Add(1, errorCheck);

                // throws the exception
                throw new RotorComparisionException(machineRotors, machineReflector, machinePlugboard,
                    errors, new List<char>(), new List<char>());
            }

            // save the rotor if no errors
            machineRotors[index] = newRotor.DeepCopy();
        }

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

        /// <summary>
        /// Checks the rotors and reflectors to ensure they share similar characters
        /// </summary>
        /// <param name="rotors">the rotor list</param>
        /// <param name="reflector">the reflector list</param>
        private void CheckMachineParameters(Rotor[] rotors, Reflector reflector, Plugboard plugboard)
        {
            // checks the rotors
            Dictionary<int, List<char>> rotorErrors = CompareRotors(rotors);
            // checks the reflectors
            List<char> reflectorError = CheckReflector(rotors[0], reflector);
            // checks the plugboard
            List<char> plugboardError = CheckPlugboard(rotors[0], plugboard);

            // if there are any errors, throw the exception
            if (reflectorError.Count > 0 || rotorErrors.Count > 0)
                throw new RotorComparisionException(rotors, reflector, plugboard, 
                    rotorErrors, reflectorError, plugboardError);
        }

        /// <summary>
        /// Checks the rotors to ensure they have the same values
        /// </summary>
        /// <param name="rotors">the rotors</param>
        /// <returns>the dictionary of <index, list> if there are any errors</returns>
        private Dictionary<int, List<char>> CompareRotors(Rotor[] rotors)
        {
            // dictionary to store the index of the error rotor and the error chars
            Dictionary<int, List<char>> rotorErrors = new Dictionary<int, List<char>>();

            // compare rotors
            for (int i = 1; i < rotors.Length; i++)
            {
                List<char> errors =
                    BasicFunctions.CompareStringsCharacters(rotors[0].RotorString, rotors[i].RotorString);

                if (errors.Count > 0)
                    rotorErrors.Add(i, errors);
            }

            // returns the error dictionary
            return rotorErrors;
        }

        /// <summary>
        /// Compares 2 rotors
        /// </summary>
        /// <param name="rotor1"></param>
        /// <param name="rotor2"></param>
        /// <returns></returns>
        private List<char> CheckRotor(Rotor rotor1, Rotor rotor2)
        {
            List<char> errors =
                    BasicFunctions.CompareStringsCharacters(rotor1.RotorString, rotor2.RotorString);

            return errors;
        }

        /// <summary>
        /// Checks the reflecor to ensure the characters match the rotor
        /// </summary>
        /// <param name="rotor">the rotor to check</param>
        /// <param name="reflector">the reflecor to check</param>
        /// <returns></returns>
        private List<char> CheckReflector(Rotor rotor, Reflector reflector)
        {
            return BasicFunctions.CompareStringsCharacters(rotor.RotorString, reflector.ReflectorReflection);
        }
        
        /// <summary>
        /// Compares the plugboard to a rotor to ensure the characters match
        /// </summary>
        /// <param name="rotor">the rotor to check</param>
        /// <param name="plugboard">the plugboard to check</param>
        /// <returns>the list of invalid characters</returns>
        private List<char> CheckPlugboard(Rotor rotor, Plugboard plugboard)
        {
            return BasicFunctions.CompareStringsCharacters(rotor.RotorString, plugboard.Combinations);
        }

        /// <summary>
        /// Sets the rotors
        /// </summary>
        /// <param name="rotors">the new rotor array</param>
        private void SetRotors(Rotor[] rotors)
        {
            machineRotors = new Rotor[rotors.Length];    // sets the length of the rotors array

            // runs through each passed rotor and copies it
            for (int i = 0; i < machineRotors.Length; i++)
            {
                machineRotors[i] = rotors[i].DeepCopy();
            }
        }

        /// <summary>
        /// Sets the reflector
        /// </summary>
        /// <param name="reflector">the new reflector</param>
        private void SetReflector(Reflector reflector) { machineReflector = reflector.DeepCopy(); }

        /// <summary>
        /// Sets the plugboard
        /// </summary>
        /// <param name="plugboard">the new plugboard</param>
        private void SetPlugboard(Plugboard plugboard) { machinePlugboard = plugboard.DeepCopy(); }

        /// <summary>
        /// Copies the machine
        /// </summary>
        /// <returns>the copied machine</returns>
        public Machine DeepCopy()
        {
            Rotor[] rotors = new Rotor[this.Rotors.Length];
            Reflector reflector = this.Reflector.DeepCopy();
            Plugboard plugboard = this.Plugboard.DeepCopy();

            for (int i = 0; i < this.Rotors.Length; i++)
                rotors[i] = this.Rotors[i].DeepCopy();

            return new Machine(rotors, reflector, plugboard);
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
            str += "Plugboard: \n\n" + machinePlugboard.ToString();

            return str;
        }
        #endregion
    }
}
