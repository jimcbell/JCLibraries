using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Library
{
    public interface IBaseIOManager //In case want to use DI in the future
    {
        public string PathInput { get; }    
        public string PathOutput { get; }
    }
    public class BaseIOManager
    {
        private readonly string pathInput = Path.Combine(Environment.CurrentDirectory, "input.txt");
        public string PathInput { get => pathInput; }
        private readonly string pathOutput = Path.Combine(Environment.CurrentDirectory, "output.txt");
        public string PathOutput { get => pathOutput; }
        public BaseIOManager()
        {
            if(!File.Exists(pathInput))
            {
                File.Create(pathInput);
            }
            if(!File.Exists(pathOutput))
            {
                File.Create(pathOutput);
            }
        }
    }
    public interface IBaseIOCryptoManager : IBaseIOManager
    {
        public string PathKey { get; }
    }
    public class BaseIOCryptoManager : BaseIOManager
    {
        private readonly string pathKey = Path.Combine(Environment.CurrentDirectory, "key.txt");
        public string PathKey { get => pathKey; }
        public BaseIOCryptoManager() : base()
        {
            if(!File.Exists(pathKey))
            {
                File.Create(pathKey);
            }
        }
    }
}
