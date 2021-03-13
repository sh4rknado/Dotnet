using System.IO;
using System.Security.Cryptography;

namespace WindowsServiceUpdate
{
    class Crypting
    {
        private void EncryptFile(string password, string file_input, string file_outpout)
        {
            byte[] key;
            byte[] iv;

            Rfc2898DeriveBytes rfcDb = new Rfc2898DeriveBytes(password, System.Text.Encoding.UTF8.GetBytes(password));

            key = rfcDb.GetBytes(16);
            iv = rfcDb.GetBytes(16);

            FileStream fsCypheredFile = new FileStream(file_outpout, FileMode.Create);

            RijndaelManaged rijndael = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Key = key,
                IV = iv
            };


            ICryptoTransform aesEncryptor = rijndael.CreateEncryptor();

            CryptoStream cs = new CryptoStream(fsCypheredFile, aesEncryptor, CryptoStreamMode.Write);

            FileStream fsPlainTextFile = new FileStream(file_input, FileMode.OpenOrCreate);

            int data;

            while ((data = fsPlainTextFile.ReadByte()) != -1) cs.WriteByte((byte)data);

            fsPlainTextFile.Close();
            cs.Close();
            fsCypheredFile.Close();
        }
        private void DecryptFile(string password, string file_input, string file_outpout)
        {
            byte[] key;
            byte[] iv;

            Rfc2898DeriveBytes rfcDb = new Rfc2898DeriveBytes(password, System.Text.Encoding.UTF8.GetBytes(password));

            key = rfcDb.GetBytes(16);
            iv = rfcDb.GetBytes(16);


            // Filestream of the new file that will be decrypted.   
            FileStream fsCrypt = new FileStream(file_outpout, FileMode.Create);

            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Mode = CipherMode.CBC;
            rijndael.Key = key;
            rijndael.IV = iv;


            ICryptoTransform aesDecryptor = rijndael.CreateDecryptor();

            CryptoStream cs = new CryptoStream(fsCrypt, aesDecryptor, CryptoStreamMode.Write);

            // FileStream of the file that is currently encrypted.    
            FileStream fsIn = new FileStream(file_input, FileMode.OpenOrCreate);

            int data;

            while ((data = fsIn.ReadByte()) != -1)
                cs.WriteByte((byte)data);
            cs.Close();
            fsIn.Close();
            fsCrypt.Close();

        }
      
        public void DecryptageFile(string password, string file_input, string file_outpout) { DecryptFile(password, file_input, file_outpout); }
        public void CryptageFile(string password, string file_input, string file_outpout) { EncryptFile(password, file_input, file_outpout); }
    }
}
