namespace IO.Library
{
    public static class Reader
    {
        public static string ReadText(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string? text = sr.ReadToEnd();
                return text;
            }
        }
    }
}
