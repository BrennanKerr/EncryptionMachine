/*
 * @author:     Brennan Kerr
 * @since:      01/06/21
 * @name:       Rotor.cs
 * @desc:       Creates the rotor class
 */

using System;
using System.Collections.Generic;
using System.Text;

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
                CheckStrings(rotorString, cypher);

                RotorName = name;
                RotorRotationLetters = letters;
            }
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
            get
            {
                return rotorName;
            }
            set
            {
                rotorName = value;
            }
        }

        /// <summary>
        /// RotorCypher() - Gets or sets the rotor cypher
        /// </summary>
        public string RotorCypher
        {
            get
            {
                return rotorCypher;
            }
            set
            {
                CheckStrings(RotorString, value);
            }
        }

        /// <summary>
        /// RotorString() - gets or sets the rotor string
        /// </summary>
        public string RotorString
        {
            get
            {
                return rotorString;
            }
            set
            {
                CheckStrings(value, RotorCypher);
            }
        }

        /// <summary>
        /// Gets or sets the rotation letters
        /// </summary>
        public char[] RotorRotationLetters
        { 
            get
            {
                return rotorRotateLetters;
            }
            set
            {
                rotorRotateLetters = value;
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
            rotorCypher = RotateString(rotorCypher, rotations);
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
        /// Allows for the rotor to be offset
        /// </summary>
        /// <param name="rotations"></param>
        public void OffsetCypher(int rotations = 1)
        {
            rotorCypher = RotateString(rotorCypher, rotations);
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
        /// Sets the rotor string
        /// </summary>
        /// <param name="val"></param>
        private void SetString(string val)
        {
            rotorString = val;
        }

        /// <summary>
        /// Sets the rotor cypher
        /// </summary>
        /// <param name="val"></param>
        private void SetCypher(string val)
        {
            rotorCypher = val;
        }

        /// <summary>
        /// Checks to make sure the strings are the same
        /// </summary>
        /// <param name="str">the string used to cross the rotor</param>
        /// <param name="cypher">the cypher used in the rotor</param>
        private void CheckStrings(string str, string cypher)
        {
            Dictionary<POTENTIAL_ERRORS, List<char>> errors
                = new Dictionary<POTENTIAL_ERRORS, List<char>>();

            List<char> currentCheck = new List<char>();

            // check for dublicates in str
            currentCheck = BasicFunctions.DuplicatedCharacters(str);

            if (currentCheck.Count > 0)
            {
                errors.Add(POTENTIAL_ERRORS.Dublicates_FromString, currentCheck);
            }

            // check for dublicates in cypher
            currentCheck = BasicFunctions.DuplicatedCharacters(cypher);

            if (currentCheck.Count > 0)
            {
                errors.Add(POTENTIAL_ERRORS.Dublicates_FromCypher, currentCheck);
            }

            // look for missing letters
            currentCheck = CompareStringsCharacters(cypher, str);
            if (currentCheck.Count > 0)
            {
                errors.Add(POTENTIAL_ERRORS.Missing_FromString, currentCheck);
            }

            currentCheck = CompareStringsCharacters(str, cypher);
            if (currentCheck.Count > 0)
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
                throw new InvalidRotorSettingsException(errors, str, cypher);
            }
        }

        /// <summary>
        /// Compares the two strings to ensure they share characters
        /// </summary>
        /// <param name="str1">the first string that contains all the desired characters</param>
        /// <param name="str2">the second string</param>
        /// <returns>the list of characters that are missing</returns>
        private List<char> CompareStringsCharacters(string str1, string str2)
        {
            List<char> invalid = new List<char>();

            for (int i = 0; i < str1.Length; i++)
            {
                char c = str1[i];

                if (!str2.Contains(c.ToString()))
                    if (!invalid.Contains(c))
                        invalid.Add(c);

            }

            return invalid;
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
            str += "Rotor Rotation Letters: ";

            for (int i = 0; i < RotorRotationLetters.Length; i++)
                str += RotorRotationLetters[i].ToString() + " ";

            return str;
        }

        #endregion
    }
}
