using System;
using System.IO;

namespace LZSS
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			const int _dictionaryLength = 8;
			const int _bufferLength = 4;

			char[] dictionary = new char[_dictionaryLength];

			try
			{
				using (StreamReader sr = new StreamReader("sam-i-am.txt")) {
					String line = sr.ReadToEnd();

					for (int i = 0; i < _dictionaryLength; i++) {
						dictionary[i] = line[0];
					}
				}
			} 
			catch (IOException e) {
				Console.WriteLine ("Couldn't read the file: " + e);
			}
		}
	}
}
