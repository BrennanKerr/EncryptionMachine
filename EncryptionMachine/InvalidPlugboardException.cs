/*
 * @author:     Brennan Kerr
 * @since:      01/14/21
 * @name:       InvalidPlugboardException.cs
 * @desc:       Creates the InvalidPlugboardException class
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionMachine
{
    /// <summary>
    /// InvalidPlugboard - Used to handle any errors with the plugboard
    /// </summary>
    class InvalidPlugboardException : Exception
    {
        #region FIELDS

        /// <summary>
        /// The combination string
        /// </summary>
        private string combinations = "";
        /// <summary>
        /// the list that stores the error chars
        /// </summary>
        private List<char> errors = new List<char>();
        /// <summary>
        /// the reason for the errors
        /// </summary>
        private PLUGBOARD_ERRORS reason;

        /// <summary>
        /// Enum that stores the error list
        /// </summary>
        public enum PLUGBOARD_ERRORS
        {
            DublicatedLetters,
            CharsNotPaired,
            PairDoesNotExist
        }

        #endregion

        #region CONSTRUCTOR
        
        /// <summary>
        /// Initializes a new exception object
        /// </summary>
        /// <param name="combinations">the combination string</param>
        /// <param name="errors">the error list</param>
        /// <param name="reason">the reason for the exception</param>
        public InvalidPlugboardException(string combinations, List<char> errors, PLUGBOARD_ERRORS reason)
        {
            this.combinations = combinations;
            this.errors = errors;
            this.reason = reason;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Retrieves the combinations that are set
        /// </summary>
        public string Combinations
        {
            get { return Combinations; }
        }

        /// <summary>
        /// Gets the error list
        /// </summary>
        public List<char> Errors
        {
            get { return errors; }
        }

        /// <summary>
        /// Gets the reason for the exception
        /// </summary>
        public PLUGBOARD_ERRORS Reason
        {
            get { return reason; }
        }
        #endregion

        #region OVERRIDES

        /// <summary>
        /// Displays the error message
        /// </summary>
        public override string Message
        { 
            get
            {
                // starts the exception string
                string str = "PLUGBOARD ERRORS: ";
                str += "\n" + combinations + "\n";

                // if there were dublicated errors
                if (reason == PLUGBOARD_ERRORS.DublicatedLetters)
                {
                    str += "Dublicates: " + BasicFunctions.RunThroughList(errors);
                }
                // if there are non paried chars
                else if (reason == PLUGBOARD_ERRORS.CharsNotPaired)
                {
                    str += "NOT PAIRED: " + BasicFunctions.RunThroughList(errors);
                }
                // if the pair does not exist
                else if (reason == PLUGBOARD_ERRORS.PairDoesNotExist)
                {
                    str += "PAIR DOES NOT EXIST FOR " + BasicFunctions.RunThroughList(errors);
                }

                str += "\nThe new pairs will not be set";

                return str;
            }
        }

        #endregion

    }
}
