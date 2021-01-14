/*
 * @author:     Brennan Kerr
 * @since:      01/12/21
 * @name:       RotorComparisionException.cs
 * @desc:       Creates the rotor comparision exception class to be used whenever
 *                  there are errors within the encryption machine
 */
using System;
using System.Collections.Generic;

namespace EncryptionMachine
{
    /// <summary>
    /// RotorComparisionException - Used to handle any errors with the machine
    /// </summary>
    public class RotorComparisionException : Exception
    {
        /// <summary>
        /// Stores the rotors
        /// </summary>
        private Rotor[] rotors;
        /// <summary>
        /// Stores the reflector
        /// </summary>
        private Reflector reflector;
        /// <summary>
        /// Stores the plugboard
        /// </summary>
        private Plugboard plugboard;

        /// <summary>
        /// Stores the reflector errors
        /// </summary>
        private List<char> reflectorMissingChars = new List<char>();
        /// <summary>
        /// Stores the rotor missing characters with the index as the key
        /// </summary>
        private Dictionary<int, List<char>> rotorMissingChars = new Dictionary<int, List<char>>();
        /// <summary>
        /// Stores the plugboard missing characters
        /// </summary>
        private List<char> plugboardMissingChars = new List<char>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rotors">the rotors of the machine</param>
        /// <param name="reflector">the reflectors of the machine</param>\
        /// <param name = "plugboard" > the plugboard of the machien</param>
        /// <param name="rotorMissingChars">the characters missing in the rotors and the index</param>
        /// <param name="reflectorMissingChars">the characters missing in the reflector</param>
        /// <param name="plugboardMissingChars">the characters missing in the plugboard</param>
        public RotorComparisionException(Rotor[] rotors, Reflector reflector, Plugboard plugboard,
            Dictionary<int, List<char>> rotorMissingChars, List<char> reflectorMissingChars,
            List<char> plugboardMissingChars)
        {
            this.rotors = rotors;
            this.reflector = reflector;

            this.rotorMissingChars = rotorMissingChars;
            this.reflectorMissingChars = reflectorMissingChars;

            this.plugboard = plugboard;
            this.plugboardMissingChars = plugboardMissingChars;
        }

        /// <summary>
        /// Retrieves the error message
        /// </summary>
        public override string Message
        {
            get
            {
                // starts creating the string by placing the defualt comparision rotor
                string returnString = "Base Rotor: ";
                returnString += rotors[0].ToString() + "\n";

                // adds the errors
                returnString += RotorErrors;
                returnString += ReflectorErrors;
                returnString += PlugboardErrors;

                // returns the errror
                return returnString;
            }
        }

        /// <summary>
        /// Gets the rotor errors
        /// </summary>
        public string RotorErrors
        {
            get
            {
                string returnString = "";   // starts the string

                // if there are errors
                if (rotorMissingChars.Count > 0)
                {
                    returnString += "Rotor Errors: \n";

                    for (int i = 0; i < rotors.Length; i++)
                    {
                        if (rotorMissingChars.ContainsKey(i))
                        {
                            returnString += rotors[i].ToString();
                            returnString += "\tMissing Characters: ";
                            returnString += BasicFunctions.RunThroughList(rotorMissingChars[i]) + "\n";
                        }
                    }

                    returnString += "\n";
                }
                // otherwise
                else returnString += "No Rotor Errors\n\n";

                // returns the string
                return returnString;
            }
        }

        /// <summary>
        /// Gets the reflecor errors
        /// </summary>
        public string ReflectorErrors
        {
            get
            {
                string returnString = "";   // starts the strings

                // if there are reflector errors
                if (reflectorMissingChars.Count > 0)
                {
                    returnString += "Reflector Errors:\n";
                    returnString += reflector.ToString() + "\n\tMissing Chars: ";
                    returnString += BasicFunctions.RunThroughList(reflectorMissingChars) + "\n";
                }
                // otherwise
                else returnString += "No Reflector Errors\n\n";
                
                // returns the errors
                return returnString;
            }
        }

        /// <summary>
        /// Gets the plugboard errors
        /// </summary>
        public string PlugboardErrors
        {
            get
            {
                string returnString = "";

                if (plugboardMissingChars.Count > 0)
                {
                    returnString += "Plugboard Errors: \n";
                    returnString += plugboard.ToString() + "\n\tMissing Chars: ";
                    returnString += BasicFunctions.RunThroughList(plugboardMissingChars) + "\n";
                }

                return returnString;
            }
        }
    }
}
