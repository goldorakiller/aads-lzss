using System;
using System.Linq;

namespace LZSS
{
	public sealed class Encoder
	{
		private static readonly Encoder instance = new Encoder();

		static Encoder() {}

		public static Encoder Instance {
			get {
				return instance;
			}
		}

		public string Encode(string fileContent, string dictionary, int bufferLength, int dictionaryLength) {

			string buffer = "";
			string output = "";

			int pos = 0;
			int offset = 0;

			while (pos < fileContent.Length) {
				int bufferEnd = pos + bufferLength <= fileContent.Length ? bufferLength : fileContent.Length - pos;
				buffer = fileContent.Substring (pos, bufferEnd);

				if (buffer.Length < 4) {
					buffer += new String ('\0', bufferLength - buffer.Length);
				}

				if (pos > 0) {
					dictionary = dictionary.Substring (offset) + fileContent.Substring (pos - offset, offset);
				}

				if (!dictionary.Contains (buffer [0])) {
					output += "1" + buffer [0] + " ";
					offset = 1;
				} else {
					var matchCount = 1;
					while (matchCount < bufferLength && dictionary.Contains (buffer.Substring (0, matchCount + 1))) {
						matchCount++;
					}

					var os = dictionaryLength - dictionary.LastIndexOf (buffer.Substring (0, matchCount));
					output += "0" + os + matchCount + " ";
					offset = matchCount;
				}

				pos += offset;
			}

			return output;
		}

		public string Decode(string encoded, string dictionary) {
			
			string output = "";

			int pos = 0;
			int offset = 0;
			int matchCount = 0;

			while (pos < encoded.Length) {
				if (encoded [pos] == '0') {
					int.TryParse (encoded [pos + 2].ToString (), out matchCount);
					int.TryParse (encoded [pos + 1].ToString (), out offset);

					output += dictionary.Substring (dictionary.Length - offset, matchCount);
					dictionary = dictionary.Substring (matchCount) + output.Substring (output.Length - matchCount, matchCount);
					pos += 3;
				} else if (encoded [pos] == '1') {
					output += encoded [pos + 1];
					dictionary = dictionary.Substring (1) + output.Last ();
					pos += 2;
				} else
					pos++;
			}

			return output;
		}
	}
}

