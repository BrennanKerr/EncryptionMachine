/*
 * @author:     Brennan Kerr
 * @since:      01/06/21
 * @name:       Rotor.cs
 * @desc:       Creates the rotor class
 */

using System;
using System.Collections.Generic;

namespace EncryptionMachine
{
    /// <summary>
    /// The rotor class is used to create an instance of a rotor
    /// that can be used in an encryption device
    /// </summary>
    public class Rotor
    {
        #region FIELDS

        /// <summary>
        /// The name given to the rotor
        /// </summary>
        private string rotorName = "";
        /// <summary>
        /// The cypher assigned to the rotor
        /// </summary>
        private string rotorCypher = "";
        /// <summary>
        /// The original string used to cross the cypher
        /// </summary>
        private string rotorString = "";
        /// <summary>
        /// The array of letters that would cause the rotor beside to rotate
        /// </summary>
        private char[] rotorRotateLetters = { };

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Rotor() - Constructor
        /// </summary>
        /// <param name="name">the assigned rotor name</param>
        /// <param name="cypher">the cypher for the rotor</param>
        /// <param name="rotorString">the string for the rotor</param>
        /// <param name="letters">the letters that cause the rotation</param>
        public Rotor(string name, string cypher, string rotorString, char[] letters)
        {
            try
            {
                // Attempts to save the cypher and string that was added
                CheckStrings(rotorString, cypher);

                // saves the rotor name and the rotation letters
                RotorName = name;
                RotorRotationLetters = letters;
            }
            // catches all exceptions
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// RotorName - Gets or sets the rotor name
        /// </summary>
        public string RotorName
        {
            // retrieves the rotor name
            get { return rotorName; }
            // sets the rotor name
            set { rotorName = value; }
        }

        /// <summary>
        /// RotorCypher() - Gets or sets the rotor cypher
        /// </summary>
        public string RotorCypher
        {
            // retrieves the rotor cypher
            get { return rotorCypher; }
            // attempts to set the rotor cypher
            set { CheckStrings(RotorString, value); }
        }

        /// <summary>
        /// RotorString() - gets or sets the rotor string
        /// </summary>
        public string RotorString
        {
            // retrieves the rotor string
            get { return rotorString; }
            // attempts to set the rotor string
            set { CheckStrings(value, RotorCypher); }
        }

        /// <summary>
        /// Gets or sets the rotation letters
        /// </summary>
        public char[] RotorRotationLetters
        { 
            // retrieves the rotor rotate letters
            get { return rotorRotateLetters; }
            // sets the rotor rotation letters
            set
            {
                // a new list that stores the characters that are unique
                List<char> characters = new List<char>();

                // a list that stores any error characters (e.g. not contained in the rotorString);
                List<char> errorChars = new List<char>();

                // runs through each option in the list
                for (int i = 0; i < value.Length; i++)
                {
                    char c = value[i];  // the char to check

                    // if the string contains the desired letter
                    if (rotorString.Contains(c.ToString()))
                    {
                        // if the character is not already listed
                        if (!characters.Contains(c))
                            characters.Add(c);
                    }
                    // add the character to the error list
                    else
                        errorChars.Add(c);
                }

                // if there are errors
                if (errorChars.Count > 0)
                {
                    // add the errors to a temporary dictionary
                    Dictionary<POTENTIAL_ERRORS, List<char>> temp = new Dictionary<POTENTIAL_ERRORS, List<char>>();
                    temp.Add(POTENTIAL_ERRORS.RototationLetters, errorChars);

                    // throw an exception
                    throw new InvalidRotorSettingsException(temp, RotorString, RotorCypher, 
                        BasicFunctions.RunThroughList(new List<char>(value)));
                }
                // otherwise everything is valid
                else
                {
                    // copy the rotation letters to the array
                    rotorRotateLetters = new char[characters.Count];
                    characters.CopyTo(rotorRotateLetters);
                }
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Rotates the rotor a defined number of times
        /// </summary>
        /// <param name="rotations">the number of rotations to make</param>
        public void Rotate(int rotations = 1)
        {
            CheckRotations(ref rotations);  // ensures a valid value was entered for the rotation

            // rotate the cypher
            rotorCypher = RotateString(rotorCypher, rotations);
            // rotate the string
            rotorString = RotateString(rotorString, rotations);
        }

        /// <summary>
        /// RotateString() - used to rotate a given string a defined number of times
        /// </summary>
        /// <param name="str">the string to rotate</param>
        /// <param name="rotations">the number of rotations</param>
        /// <returns>The rotated string</returns>
        private string RotateString(string str, int rotations = 1)
        {
            string new_str = str.Substring(rotations, str.Length - rotations) + str.Substring(0, rotations);
            return new_str;
        }

        /// <summary>
        /// Allows for the rotor to be offset (moves the cypher x number of times)
        /// </summary>
        /// <param name="rotations">the number of rotations to make the offset</param>
        public void OffsetCypher(int rotations = 1)
        {
            CheckRotations(ref rotations);  // ensures a valid value was entered for the rotations

            // rotates the cypher the desired number of times
            rotorCypher = RotateString(rotorCypher, rotations);
        }

        /// <summary>
        /// Verifies the number of rotations is a valid amount
        /// </summary>
        /// <param name="rotations"></param>
        private void CheckRotations(ref int rotations)
        {
            // the exception that will be thrown if the rotation value is invalid
            Exception ex = new IndexOutOfRangeException("You can not rotate " + rotations + " number of times");
            
            // if the rotations value is too small
            if (rotations < -rotorString.Length)
                throw ex;
            // if the rotation value is less then 0, change the value to rotate the opposite direction
            if (rotations < 0)
                rotations = rotorString.Length + rotations;
            // if the rotation value is too large
            else if (rotations > rotorString.Length)
                throw ex;
        }

        /// <summary>
        /// Creates a deep copy of the rotor
        /// </summary>
        /// <returns>the copy</returns>
        public Rotor DeepCopy()
        {
            string name = this.rotorName;
            string cypher = this.rotorCypher;
            string str = this.rotorString;
            char[] letters = this.rotorRotateLetters;

            return new Rotor(name, cypher, str, letters);
            
        }

        /// <summary>
        /// Checks to make sure the strings are the same
        /// </summary>
        /// <param name="str">the string used to cross the rotor</param>
        /// <param name="cypher">the cypher used in the rotor</param>
        private void CheckStrings(string str, string cypher)
        {
            // stores any errors that may occur
            Dictionary<POTENTIAL_ERRORS, List<char>> errors
                = new Dictionary<POTENTIAL_ERRORS, List<char>>();

            // a list that stores the information that is checked
            List<char> currentCheck = new List<char>();

            // check for dublicates in str
            currentCheck = BasicFunctions.DuplicatedCharacters(str);

            if (currentCheck.Count > 0) // if there are dublicates in the string
            {
                errors.Add(POTENTIAL_ERRORS.Dublicates_FromString, currentCheck);
            }

            // check for dublicates in cypher
            currentCheck = BasicFunctions.DuplicatedCharacters(cypher);

            if (currentCheck.Count > 0) // if there are duplicates in the cypher
            {
                errors.Add(POTENTIAL_ERRORS.Dublicates_FromCypher, currentCheck);
            }

            // look for missing letters
            currentCheck = BasicFunctions.CompareStringsCharacters(cypher, str);
            if (currentCheck.Count > 0) // if there are letters missing in the string
            {
                errors.Add(POTENTIAL_ERRORS.Missing_FromString, currentCheck);
            }

            currentCheck = BasicFunctions.CompareStringsCharacters(str, cypher);
            if (currentCheck.Count > 0) // if there are letters missing in the cypher
            {
                errors.Add(POTENTIAL_ERRORS.Missing_FromCypher, currentCheck);
            }


            // if everything is valid
            if (errors.Count == 0)
            {
                rotorString = str;
                rotorCypher = cypher;
            }
            else
            {
                // throw exception
                throw new InvalidRotorSettingsException(errors, str, cypher, DisplayRotationLetters());
            }
        }

        

        /// <summary>
        /// Displays the letters that cause the rotor to rotate
        /// </summary>
        /// <returns></returns>
        private string DisplayRotationLetters()
        {
            return BasicFunctions.RunThroughList(new List<char>(rotorRotateLetters));
        }

        #endregion

        #region OVERRIDES

        /// <summary>
        /// Converts the Rotor to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "Rotor Name: " + RotorName + "\n";
            str += "Rotor Cypher: " + rotorCypher + "\n";
            str += "Rotor String: " + rotorString + "\n";
            str += "Rotor Rotation Letters: " + DisplayRotationLetters();

            return str;
        }

        #endregion
    }
}
