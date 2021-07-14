using Business.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces.Utilities
{
    public interface IHashingService
    {
        string ComputeLongHash(string value);

        string ComputeHash(string value);

        string ComputeShortHash(string value);

        byte[] ComputeLongHashBytes(string value);

        byte[] ComputeHashBytes(string value);
        byte[] ComputeShortHashBytes(string value);
        SaltedHash ComputeShortSaltedHash(string value, string salt = "");
        SaltedHash ComputeSaltedHash(string value, string salt = "");
        SaltedHash ComputeLongSaltedHash(string value, string salt = "");
    }
}
