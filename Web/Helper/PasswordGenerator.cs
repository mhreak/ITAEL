using System.Security.Cryptography;
using System.Text;

namespace Web.Helper
{
    //public class PasswordGenerator
    //{
    //    public static string GenerateIdentityPassword(int length = 12)
    //    {
    //        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //        const string lower = "abcdefghijklmnopqrstuvwxyz";
    //        const string digits = "0123456789";
    //        const string special = "!@#$%^&*()_-+=<>?";

    //        string all = upper + lower + digits + special;

    //        StringBuilder password = new StringBuilder();
    //        RandomNumberGenerator rng = RandomNumberGenerator.Create();

    //        // اضافه کردن حداقل یک مورد از هر دسته برای معتبر بودن
    //        password.Append(GetRandomChar(upper));
    //        password.Append(GetRandomChar(lower));
    //        password.Append(GetRandomChar(digits));
    //        password.Append(GetRandomChar(special));

    //        // پر کردن باقی پسورد
    //        for (int i = password.Length; i < length; i++)
    //            password.Append(GetRandomChar(all));

    //        // Shuffle نهایی پسورد
    //        return Shuffle(password.ToString());
    //    }

    //    private static char GetRandomChar(string chars)
    //    {
    //        byte[] data = new byte[1];
    //        using (var rng = RandomNumberGenerator.Create())
    //        {
    //            do
    //            {
    //                rng.GetBytes(data);
    //            } while (data[0] >= chars.Length * (byte.MaxValue / chars.Length));
    //        }

    //        return chars[data[0] % chars.Length];
    //    }

    //    private static string Shuffle(string input)
    //    {
    //        var chars = input.ToCharArray();
    //        byte[] buffer = new byte[chars.Length];
    //        RandomNumberGenerator.Fill(buffer);

    //        for (int i = 0; i < chars.Length; i++)
    //        {
    //            int j = buffer[i] % chars.Length;
    //            (chars[i], chars[j]) = (chars[j], chars[i]);
    //        }

    //        return new string(chars);
    //    }
    //}


    public class PasswordGenerator
    {
        public static string GenerateIdentityPassword()
        {
            const string digits = "0123456789";
            int length = 5;

            StringBuilder password = new StringBuilder(length);
            byte[] buffer = new byte[1];

            using (var rng = RandomNumberGenerator.Create())
            {
                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(buffer);
                    int index = buffer[0] % digits.Length;
                    password.Append(digits[index]);
                }
            }

            return password.ToString();
        }
    }

}
