using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5B
{
    /// <summary>
    /// Purpose:    This class is an object that is a type of Media
    ///             Book contains a summary description which needs to be decrypted
    ///             If the toString method is called it will print all properties in a formatted string EXCLUDING the Summary property
    ///             The unique properties of this class are Author and Summary, inherits the base class
    ///             Implements IEncryptable in order to decode the Summary property
    /// </summary>
    class Book : Media, IEncryptable
    {
        /// <summary>
        /// These properties are used in the print results
        /// Names are self-explanitory
        /// Summary will be used in IEncryptable
        /// </summary>
        public string Author { get; set; }
        public string Summary { get; set; }

        /// <summary>
        /// - Assigns the value of the properties Author and Summary
        /// - Assigns the value from Title and Year in the base
        /// </summary>
        /// <param name="title">The name of the media</param>
        /// <param name="year">The year of publication</param>
        /// <param name="author"></param>
        /// <param name="summary">The description of the content</param>
        public Book(string title, int year, string author, string summary)
            : base(title, year)
        {
            Title = title;
            Year = year;
            Author = author;
            Summary = summary;
        }

        /// <summary>
        /// toString
        /// - This is an override method of the typical toString
        /// - When this is called it will instead format the string based on the properties in this class
        /// - Returns the formatted string
        /// </summary>
        /// <returns>Information of media content in formatted text</returns>
        public override string ToString()
        {
            string returnString = string.Format("Book Title: {0} ({1})\nAuthor: {2}", Title, Year, Author);
            return returnString;
        }

        #region IEncryptable

        /// <summary>
        /// Encrypt:
        /// - Uses an encryption algorithm to return some encrypted data
        /// - Uses ROT13 algorithm, switches a letter with another 13 characters ahead/behind
        /// </summary>
        /// <returns>Encrypted Message</returns>
        public string Encrypt()
        {
            char[] summaryCharactersArr = Summary.ToCharArray();
            for (int i = 0; i < summaryCharactersArr.Length; i++)
            {
                int number = (int)summaryCharactersArr[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                summaryCharactersArr[i] = (char)number;
            }
            return new string(summaryCharactersArr);
        }

        /// <summary>
        /// Decrypt:
        /// - Uses the same encryption algorithm to return a decrypted string.
        /// - Uses ROT13 algorithm, switches a letter with another 13 characters ahead/behind
        /// </summary>
        /// <returns>Decrypted Message</returns>
        public string Decrypt()
        {
            char[] summaryCharactersArr = Summary.ToCharArray();
            for (int i = 0; i < summaryCharactersArr.Length; i++)
            {
                int number = (int)summaryCharactersArr[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                summaryCharactersArr[i] = (char)number;
            }
            return new string(summaryCharactersArr);
        }

        #endregion //End IEncryptable
    }
}
