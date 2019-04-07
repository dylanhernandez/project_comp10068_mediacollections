using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5B
{
    /// <summary>
    /// Purpose:    This class is an object that is a type of Media
    ///             Song does not contain a description like Book and Movie
    ///             If the toString method is called it will print all properties in a formatted string
    ///             The unique properties of this class are Artist and Album, inherits the base class
    ///             Does not implement IEncryptable
    /// </summary>
    class Song : Media
    {
        /// <summary>
        /// These properties are used in the print results
        /// Names are self-explanitory
        /// </summary>
        public string Artist { get; set; }
        public string Album { get; set; }

        /// <summary>
        /// Contructor
        /// - Assigns the value of the properties Artist and Album
        /// - Assigns the value from Title and Year in the base
        /// </summary>
        /// <param name="title">The name of the media</param>
        /// <param name="year">The year of publication</param>
        /// <param name="artist"></param>
        /// <param name="album"></param>
        public Song(string title, int year, string artist, string album)
            : base(title, year)
        {
            Title = title;
            Year = year;
            Artist = artist;
            Album = album;
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
            string returnString = string.Format("Song Title: {0} ({1})\nAlbum: {2}  Artist: {3}", Title, Year, Album, Artist);
            return returnString;
        }
    }
}
