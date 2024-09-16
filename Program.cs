using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AuthLKGenerator
{
    internal class Program
    {
		public static List<string> CandidateKeys = new List<string>
		{
			"3ynnjzqn", "33yud0b3", "2rr30lvu", "nlz4nwa0", "wkiftlsd", "3au35del", "5eps0gdv", "tgqrprgw", "b0ig33ds", "ldr34nd0",
			"sxjl1ued", "jmy1hxm3", "gp55l4lw", "ym4hxqyf", "hcunbdea", "c1ge1wpu", "rej1wpxy", "ntr0t3hj", "p2erfffu", "2mxjmgjy",
			"3kl5hmy3", "om5aatmq", "ywzrotu0", "gebhwccx", "0e2xrz3q", "zfddkmwr", "gsvhxdek", "xue5v1sy", "pyne5ktt", "ujpydksc",
			"ecewer3p", "vkdxfaoa", "gworvaqy", "bg2nxnkb", "teyau31w", "zbajffbd", "a0h35454", "nl2zom2g", "m5t3tl5e", "5fnpvqkj",
			"zqhyd3il", "fmunino3", "od1xmiiw", "12hgk0k4", "rofz2jwx", "zx4cdnhl", "d3xe2mg4", "robhhzfp", "0gpjcxzs", "5edv5tbd",
			"4ddwwkkx", "0zcrazuz", "djwm5wnn", "0su2febf", "jlgdajns", "rmgxhh4g", "vb1zry1j", "o2ucskhs", "wo2dtrll", "oqnat2qz",
			"oxoevvw4", "h3tjznmk", "25f0els3", "obpqvlre", "jbdy21b0", "o2blmtyl", "tg4qziiz", "at22er1j", "o1cw5iy2", "qpbihhq4",
			"krpyyru5", "kokdhy1q", "dovelsvr", "qsg2kaee", "23zoyex5", "hgbbmiwr", "1gsouwd0", "dpdetide", "5dlgtn30", "0c05fmm5",
			"muuux3g3", "mbxxyuct", "3juhy5ko", "bmlobvj4", "ayrscu4l", "x3ui2wkj", "urxfsbiq", "ve0jpbzy", "tcmayoxo", "5x13jnk2",
			"4vufocbo", "wb3afjq0", "dvdawybx", "bmmggtpc", "aeghrte4", "trnodemx", "kzgclqam", "bqk4qszz", "11abxfri", "ppgnyjgx"
		};

		[STAThread]
		static void Main(string[] args)
        {
			DateTime CreateDateTime = DateTime.Now;

			string text = String.Empty, inputString = $@"<?xml version=""1.0""?>
<LKInformation xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <CreateDateTime>{CreateDateTime.ToString("o")}</CreateDateTime>
  <ExpireDateTime>{CreateDateTime.AddHours(1).ToString("o")}</ExpireDateTime>
  <AuthTo>{Environment.MachineName}</AuthTo>
</LKInformation>";

			Random random = new Random();
			int index = random.Next(0, 100);
			byte[] bytes = Encoding.ASCII.GetBytes(CandidateKeys[index]);

			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			StreamWriter streamWriter = null;
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();

			DirectoryInfo info = null;
			XDocument doc;

			try
			{
				memoryStream = new MemoryStream();
				cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

				streamWriter = new StreamWriter(cryptoStream);
				streamWriter.Write(inputString);
				streamWriter.Flush();

				cryptoStream.FlushFinalBlock();

				text = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			finally
			{
				streamWriter?.Close();
				cryptoStream?.Close();
				memoryStream?.Close();
			}

			string source = index.ToString("D2");
			string text2 = text;
			text2 = text.Insert(1, source.First().ToString());
			text2 = text2.Insert(text2.Length - 1, source.Last().ToString());
            
			Clipboard.SetText(text2);
		}
    }
}
