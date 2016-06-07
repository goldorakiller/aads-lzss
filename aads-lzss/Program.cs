using System;
using System.IO;
using System.Linq;
using System.Text;

namespace LZSS
{
	class Program
	{
		public static void Main (string[] args)
		{
			const int _dictionaryLength = 8;
			const int _bufferLength = 4;

			string dictionary;

			try {
				StreamReader sr = new StreamReader("sam-i-am.txt", Encoding.ASCII);
				string fileContent = sr.ReadToEnd();
				sr.Close();

				dictionary = new String(fileContent.FirstOrDefault(), _dictionaryLength);

				string encoded = Encoder.Instance.Encode(fileContent, dictionary, _bufferLength, _dictionaryLength);

				#if DEBUG
					Console.WriteLine("Encoded: ");
					Console.WriteLine(encoded);
				#endif

				string decoded = Encoder.Instance.Decode(encoded, dictionary);

				#if DEBUG
					Console.WriteLine("Decoded: ");
					Console.WriteLine(decoded);
				#endif
			}
			catch (IOException e) {
				Console.WriteLine ("Couldn't read the file: " + e);
			}
		}
	}
}
