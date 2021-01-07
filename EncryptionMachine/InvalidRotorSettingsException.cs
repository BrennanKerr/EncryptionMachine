/*
 * @author:     Brennan Kerr
 * @since:      01/07/21
 * @name:       InvalidRotorSettingsException.cs
 * @desc:       Creates the invalid rotor settings class to be used whenever
 *                  there is a string or cypher error
 */
using System;
using System.Collections.Generic;

namespace EncryptionMachine
{
    /// <summary>
    /// Class used to throw an error when invalid information is entered for the rotor
    /// </summary>
    public class InvalidRotorSettingsException : Exception
    {
        #region FIELDS
        /// <summary>
        /// Stores the invalid errors using the POTENTIAL_ERRORS enum
        /// </summary>
        private Dictionary<POTENTIAL_ERRORS, List<char>> errors 
            = new Dictionary<POTENTIAL_ERRORS, List<char>>();
        /// <summary>
        /// Stores the string that was part of the error
        /// </summary>
        private string str = "";
        /// <summary>
        /// Stors the cypher that was part of the error
        /// </summary>
        private string cypher = "";

        #endregion

        #region CONSTRUCTORS
        
        /// <summary>
        /// Creates a new instance of the exception
        /// </summary>
        /// <param name="errorDictionary">Stores the list of errors that occured</param>
        /// <param name="rotorString">Stores the string that is stored for the rotor</param>
        /// <param name="rotorCypher">Stores the cypher that is stored for the rotor</param>
        public InvalidRotorSettingsException(Dictionary<POTENTIAL_ERRORS, List<char>> errorDictionary, string rotorString, string rotorCypher)
        {
            errors = errorDictionary;
            str = rotorString;
            cypher = rotorCypher;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Overrides the Message function to display the list of errors
        /// </summary>
        public override string Message
        {
            get
            {
                string stringErrors = "";   // stores the string error message
                string cypherErrors = "";   // store the cypher error message

                // checks for errors in the string
                CheckForErrors(POTENTIAL_ERRORS.Dublicates_FromString, ref stringErrors, "Dublicates", false);
                CheckForErrors(POTENTIAL_ERRORS.Missing_FromString, ref stringErrors, "Missing", false);

                // checks for errors in the cypher
                CheckForErrors(POTENTIAL_ERRORS.Dublicates_FromCypher, ref cypherErrors, "Dublicates");
                CheckForErrors(POTENTIAL_ERRORS.Missing_FromCypher, ref cypherErrors, "Missing");

                return stringErrors + cypherErrors;
            }
        }

        /// <summary>
        /// Gets the rotor string
        /// </summary>
        public string String
        {
            get { return str; }
        }

        /// <summary>
        /// Gets the rotor cypher
        /// </summary>
        public string Cypher
        {
            get { return cypher; }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Runs through the list provided to display the error characters
        /// </summary>
        /// <param name="list">the list to display</param>
        /// <returns>the list of error characters</returns>
        public string RunThroughList(List<char> list)
        {
            string str = "";    // create a new string

            // run through each char in the list
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i] + (i == list.Count - 1 ? "" : ", ");
            }

            str += "\n";

            return str;
        }

        /// <summary>
        /// Display any errors that occured
        /// </summary>
        /// <param name="key">the key provided from POTENTIAL_ERRORS</param>
        /// <param name="errorStr">the error string to append to</param>
        /// <param name="reason">the reason for the error</param>
        /// <param name="isCypher">true if the error is apart of the cypher, false if it is the string</param>
        private void CheckForErrors(POTENTIAL_ERRORS key, ref string errorStr, string reason, bool isCypher = true)
        {
            if (errors.ContainsKey(key))
            {
                if (errorStr == "")
                    errorStr += "Errors with " + (isCypher ? "Cypher: " + cypher : "String: " + str) + "\n";

                errorStr += reason + ": " + RunThroughList(errors[key]);
            }
        }

        #endregion
    }

    /// <summary>
    /// Enum used for the Error dictionary list
    /// </summary>
    public enum POTENTIAL_ERRORS
    {
        Dublicates_FromString,
        Dublicates_FromCypher,
        Missing_FromString,
        Missing_FromCypher
    }
}
