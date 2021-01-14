/*
 * @author:     Brennan Kerr
 * @since:      01/14/21
 * @name:       Plugboard.cs
 * @desc:       Creates the plugboard class
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionMachine
{
    /// <summary>
    /// The plugboard class is used to create an instance of plugboard
    /// which is used to change the letter
    /// </summary>
    public class Plugboard
    {
        /// <summary>
        /// Stores the combination string
        /// </summary>
        private string plugboardCombinations = "";

        /// <summary>
        /// Creates a new plugboard instance
        /// </summary>
        /// <param name="combinations">the combination string (Each pair is an odd and even index)</param>
        public Plugboard(string combinations = "")
        {
            Combinations = combinations;
        }

        /// <summary>
        /// Sets the combination string
        /// </summary>
        public string Combinations
        {
            get { return plugboardCombinations; }
            set
            {
                // if the length is an even number
                if (value.Length % 2 == 0)
                {
                    // check and store any dublicates
                    List<char> dublicates = BasicFunctions.DuplicatedCharacters(value);

                    // if there are dublicates, throw an exception
                    if (dublicates.Count > 0)
                        throw new InvalidPlugboardException(value, dublicates, 
                            InvalidPlugboardException.PLUGBOARD_ERRORS.DublicatedLetters);
                    // otherwise set the plugboard
                    else
                        plugboardCombinations = value;
                }
                // if the length is odd
                else
                    throw new InvalidPlugboardException(value, new List<char>() { value[value.Length - 1] }, 
                        InvalidPlugboardException.PLUGBOARD_ERRORS.CharsNotPaired);
            }
        }

        #region METHODS

        /// <summary>
        /// Gets the paired letter
        /// </summary>
        /// <param name="letter">the letter to check</param>
        /// <returns>if the letter is in the plugboard, the paired letter, otherwise returns the same letter</returns>
        public char GetPairedLetter(char letter)
        {
            // if the letter is in the plugboard list
            if (IsPaired(letter))
            {
                // gets the index
                int index = plugboardCombinations.IndexOf(letter);

                // returns the paired index
                    // (index % 2 == 0) determines if the index is even or odd
                return plugboardCombinations[PairedLetterIndex(index)];
            }
            // otherwise return the same letter
            else return letter;
        }

        /// <summary>
        /// Checks if the letter is already paired
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public bool IsPaired(char letter)
        {
            if (plugboardCombinations.Contains(letter.ToString())) return true;
            else return false;
        }

        /// <summary>
        /// Sets a pair of letters
        /// </summary>
        /// <param name="letter1">the first letter to pair</param>
        /// <param name="letter2">the second letter to pair</param>
        public void SetPair(char letter1, char letter2)
        {
            SetPair(letter1.ToString() + letter2.ToString());
        }

        /// <summary>
        /// Sets pairs based on a string
        /// </summary>
        /// <param name="letters">the string that contains the pairings</param>
        public void SetPair(string letters)
        {
            // if there are an even number of pairs
            if (letters.Length % 2 == 0)
            {
                // stores any dublicates
                List<char> dublicates = new List<char>();

                // checks for any dublicates
                for (int i = 0; i < letters.Length; i++)
                {
                    char c = letters[i];
                    if (IsPaired(c)) dublicates.Add(c);
                    // if the letters are the same
                    else if (i % 2 == 0)
                    {
                        if (letters[i] == letters[i + 1])
                            dublicates.Add(c);
                    }
                }

                // if there are dublicates
                if (dublicates.Count > 0)
                    throw new InvalidPlugboardException(Combinations + letters, dublicates,
                        InvalidPlugboardException.PLUGBOARD_ERRORS.DublicatedLetters);
                // otherwise, add the combinations to the string
                else plugboardCombinations += letters;
            }
            else
                throw new InvalidPlugboardException(Combinations + letters, new List<char>() { letters[letters.Length - 1] },
                    InvalidPlugboardException.PLUGBOARD_ERRORS.CharsNotPaired);
        }

        /// <summary>
        /// Removes the pair based on the letter
        /// </summary>
        /// <param name="letter">the letter of the pair to remove</param>
        public void RemovePair(char letter)
        {
            if (!IsPaired(letter))
                throw new InvalidPlugboardException(Combinations, new List<char>() { letter },
                    InvalidPlugboardException.PLUGBOARD_ERRORS.PairDoesNotExist);
            else
            {
                // gets the two indexes
                int index1 = plugboardCombinations.IndexOf(letter);
                int index2 = PairedLetterIndex(index1);

                // removes the letters
                plugboardCombinations = plugboardCombinations.Remove((index1 < index2 ? index1 : index2), 2);
            }
        }

        /// <summary>
        /// Gets the index of the paired letter
        /// </summary>
        /// <param name="i">the index of the first letter</param>
        /// <returns>the paired letter index</returns>
        private int PairedLetterIndex(int i)
        {
            return i + (i % 2 == 0 ? 1 : -1);
        }

        /// <summary>
        /// Creates a copy of the plugboard
        /// </summary>
        /// <returns>the plugboard copy</returns>
        public Plugboard DeepCopy()
        {
            string combos = this.plugboardCombinations;
            return new Plugboard(combos);
        }
        #endregion

        /// <summary>
        /// Converts the plugboard to a string
        /// </summary>
        /// <returns>the plugboard combinations</returns>
        public override string ToString()
        {
            string str = "Combinations: ";
            
            for (int i = 0; i < Combinations.Length; i+=2)
            {
                str += Combinations[i] + " = " + Combinations[i + 1] 
                    + (i == Combinations.Length - 2 ? "" : " , ");
            }

            return str;
        }
    }
}
