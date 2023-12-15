namespace JCLibraries.IO.Library
{
    public static class IOHandler
    {
        static string ReadText(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string? text = sr.ReadToEnd();
                return text;
            }
        }
        static void WriteText(string decryptedText, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(decryptedText);
            }
        }
    }
}
