using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5B
{
    /// <summary>
    /// The class implementing the Encrypt() method will use some sort
    /// of encryption algorithm to return some encrypted data.
    ///
    ///  The class implementing the Decrypt() method will use (presumably)
    ///  the same encryption algorithm to return a decrypted string.
    /// </summary>
    public interface IEncryptable
    {
        string Encrypt();
        string Decrypt();
    }
}
