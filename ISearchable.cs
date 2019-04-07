using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5B
{
    /// <summary>
    /// The class implementing the Search() method will be assumed to
    /// use a string search key as a parameter and return a boolean
    /// value to indicate if that key was successfully found.
    /// </summary>
    interface ISearchable
    {
        bool Search(string key);
    }
}
