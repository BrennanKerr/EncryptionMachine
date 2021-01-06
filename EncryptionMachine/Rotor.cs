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
        private string[] rotorRotateLetters = { };

        /// <summary>
        /// Rotor() - Constructor
        /// </summary>
        /// <param name="name">the assigned rotor name</param>
        /// <param name="cypher">the cypher for the rotor</param>
        /// <param name="rotorString">the string for the rotor</param>
        /// <param name="letters">the letters that cause the rotation</param>
        public Rotor(string name, string cypher, string rotorString, string[] letters)
        {
            RotorName = name;
            RotorCypher = cypher;
            RotorString = rotorString;
            RotorRotationLetters = letters;
        }

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
                rotorCypher = value;
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
                rotorString = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation letters
        /// </summary>
        public string[] RotorRotationLetters
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
    }
}
