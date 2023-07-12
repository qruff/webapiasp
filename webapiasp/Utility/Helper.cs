using System.Text;

namespace webapiasp.Utility
{
    public class Helper
    {
        public static string GetAlphaNumericOrderId()
        {
            string alphaNumericString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder(8);

            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(alphaNumericString.Length);
                sb.Append(alphaNumericString[index]);
            }

            return sb.ToString().ToUpper();
        }
    }
}
