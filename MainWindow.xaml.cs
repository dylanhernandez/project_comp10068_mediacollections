/*
 * AUTHOR: DYLAN HERNANDEZ - 000307857
 * DATE: 2016/04/08
 * PURPOSE: This program reads data from a text file and stores the data into an array, makes use of two interfaces to search and encrypt/decrypt data
 * This program is a re-invention of lab 3 - this is integrated into a WPF application.
 * STATEMENT OF AUTHORSHIP: 
 * I, Dylan Hernandez, 000307857 certify that this material is my original work. 
 * No other person's work has been used without due acknowledgement.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab5B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //This is the array that will hold all the media content
        private Media[] contentMedia;

        /// <summary>
        /// This activates when the form starts
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// windowMain_Loaded:
        /// - This event starts when the form loads
        /// - Data is read into the content array
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            contentMedia = MediaHandler.ReadData(); //When the form loads, get data
        }

        /// <summary>
        /// listMedia_SelectionChanged:
        /// - This method activates when the selection index changes
        /// - The summary from the media item is put into the summary box
        /// - Media objects without a summary like SONG only leaves the summary box blank
        /// - Summary is decrypted using IEncryptable
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void listMedia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((listMedia.SelectedValue != null)) //Make sure there is no empty selection
            {
                string selectedItem = listMedia.SelectedValue.ToString(); //Convert current item to string
                bool summaryFlag = false; //Nothing found yet

                foreach (var media in contentMedia)
                {
                    if (summaryFlag) { //If something was found, skip through the rest of the iterations
                        continue;
                    }
                    if (media.ToString().Equals(selectedItem)) //You found a match
                    {
                        summaryFlag = true;
                        txtbxSummary.Clear();
                        if (media.GetType().Equals(typeof(Movie))) //Summary from Movie
                        {
                            Movie temp = (Movie)media;
                            txtbxSummary.Text = temp.Decrypt();
                        }
                        else if (media.GetType().Equals(typeof(Book))) //Summary from Book
                        {
                            Book temp = (Book)media;
                            txtbxSummary.Text = temp.Decrypt();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// btnAllBooks_Click:
        /// - Puts the content of all books into the list
        /// - The top item is selected by default (this will also auto-generate the summary)
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void btnAllBooks_Click(object sender, RoutedEventArgs e)
        {
            listMedia.Items.Clear(); //Clear the list
            foreach (var media in contentMedia) 
            {
                if (media.GetType().Equals(typeof(Book))) //Filter all books
                {
                    listMedia.Items.Add(media.ToString());
                }
            }
            listMedia.SelectedIndex = 0; //Select first item
        }

        /// <summary>
        /// btnAllMovies_Click:
        /// - Puts the content of all movies into the list
        /// - The top item is selected by default (this will also auto-generate the summary)
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void btnAllMovies_Click(object sender, RoutedEventArgs e)
        {
            listMedia.Items.Clear(); //Clear the list
            foreach (var media in contentMedia) 
            {
                if (media.GetType().Equals(typeof(Movie))) //Filter all movies
                {
                    listMedia.Items.Add(media.ToString());
                }
            }
            listMedia.SelectedIndex = 0;
        }

        /// <summary>
        /// btnAllSongs_Click:
        /// - Puts the content of all songs into the list
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void btnAllSongs_Click(object sender, RoutedEventArgs e)
        {
            listMedia.Items.Clear(); //Clear the list
            foreach (var media in contentMedia) 
            {
                if (media.GetType().Equals(typeof(Song))) //Filter all songs
                {
                    listMedia.Items.Add(media.ToString());
                }
            }
            listMedia.SelectedIndex = 0;
        }

        /// <summary>
        /// btnAllMedia_Click:
        /// - Puts the content of all media into the list regardless of type
        /// - If the first item is a movie or book then the summary will generate
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void btnAllMedia_Click(object sender, RoutedEventArgs e)
        {
            listMedia.Items.Clear(); //Clear the list
            foreach (var media in contentMedia) 
            {
                listMedia.Items.Add(media.ToString());
            }
            listMedia.SelectedIndex = 0;
        }

        /// <summary>
        /// btnSearchTitle_Click:
        /// - The search function of the application
        /// - A search string is extracted from the textbox
        /// - The search function of the media object is used to compare the search query
        /// - Only items matching will be added to the list
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void btnSearchTitle_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchBox.Text)) //Nothing in the text
            {
                MessageBox.Show("Enter something to search!");
            }
            else 
            {
                string searchQuery = txtSearchBox.Text;
                txtSearchBox.Clear(); //Clear the current entry
                listMedia.Items.Clear();
                foreach (var media in contentMedia) 
                {
                    if (media.Search(searchQuery)) 
                    {
                        listMedia.Items.Add(media.ToString());
                    }
                }
                if (listMedia.Items.Count > 0) //If there is more than one item, select the first index
                {
                    listMedia.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// btnExit_Click:
        /// - Exits the application
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// btnClearAll_Click:
        /// - Clears all form data
        /// - Removes the list items
        /// - Clears the search bar
        /// - Clears the summary
        /// </summary>
        /// <param name="sender">The object starting the event</param>
        /// <param name="e">The event object itself</param>
        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            txtSearchBox.Clear(); //Clear the current entry
            txtbxSummary.Clear(); //Clear the summary box
            listMedia.Items.Clear(); //Clear the list
        }
    }

    /// <summary>
    /// This static class is used for reading media data from a file
    /// Converts the data to a media array
    /// </summary>
    public static class MediaHandler 
    {
        /// <summary>
        /// ReadData
        /// - Reads data from a text file
        /// - Method defines file path in a constant for reference
        /// - Imports all content into a string
        /// - Splits string which is evaluated as an array and converted into a media array
        /// - Media array holds Books, Songs and Movies
        /// </summary>
        /// <returns>Media Array</returns>
        public static Media[] ReadData()
        {
            const string FILE = "data.txt"; //File path

            string contentString = ""; //Holds file content
            string[] contentArr; //File content as array

            Media[] mediaContent = null; //Returnable array

            try
            {
                using (TextReader r = File.OpenText(FILE)) //File handler
                {
                    string fileString;
                    while ((fileString = r.ReadLine()) != null)
                    {
                        contentString += fileString + "\n";
                    }
                }
                contentArr = contentString.Split(new string[] { "-----", }, StringSplitOptions.RemoveEmptyEntries); //Data is divided by this seperator
                mediaContent = GetContent(contentArr);
            }
            catch //Returns null if error occurs
            {
                return null;
            }

            return mediaContent;
        }

        /// <summary>
        /// GetContent
        /// - A compartment of the ReadData method
        /// - Creates a list to hold all media typed
        /// - Each component of the file string array is evaluated
        /// - Content determines Media type (Book,Movie,Song)
        /// - Adds new Media object to the list
        /// - Converts list to array
        /// - Returns the new Media array
        /// </summary>
        /// <param name="content">The file content in the form of an organized array</param>
        /// <returns>THe media array</returns>
        private static Media[] GetContent(string[] content)
        {
            List<Media> baseContent = new List<Media>();
            foreach (string element in content)
            {
                if (element.Equals("\n")) //Ignores blank new lines
                {
                    continue;
                }

                string[] currentElement = element.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); //Summaries start on a new line
                string[] currentContent = currentElement[0].Split(new string[] { "|" }, StringSplitOptions.None); //Content is divided by '|'
                string currentSummary; //If the summary is divided into multiple lines it will be reconsolidated into this variable

                switch (currentContent[0]) //First part is the content
                {
                    case "BOOK":
                        currentSummary = ReCombine(currentElement); //Put all summary lines into one string
                        baseContent.Add(new Book(currentContent[1], int.Parse(currentContent[2]), currentContent[3], currentSummary));
                        break;
                    case "MOVIE":
                        currentSummary = ReCombine(currentElement);
                        baseContent.Add(new Movie(currentContent[1], int.Parse(currentContent[2]), currentContent[3], currentSummary));
                        break;
                    case "SONG":
                        baseContent.Add(new Song(currentContent[1], int.Parse(currentContent[2]), currentContent[3], currentContent[4]));
                        break;
                    default:
                        baseContent.Add(null);
                        break;
                }
            }
            Media[] returnContent = baseContent.ToArray();
            return returnContent;
        }

        /// <summary>
        /// ReCombine
        /// - Takes all lines of a summary and puts it back into one string
        /// - Returns the summary string
        /// </summary>
        /// <param name="splitElement">All lines of the media content</param>
        /// <returns>Summary of the media content</returns>
        private static string ReCombine(string[] splitElement)
        {
            if (splitElement.Length <= 2) //Only a single summary line
            {
                return splitElement[splitElement.Length - 1]; //Return last element
            }
            var builder = new System.Text.StringBuilder();
            for (int i = 1; i < splitElement.Length; i++) //Start at the summary line
            {
                if (i == 1) { builder.Append(splitElement[i]); }
                else { builder.Append("\n\n").Append(splitElement[i]); }
            }
            return builder.ToString();
        }
    }
}
