using System.IO;
using System.Reflection;

namespace EasyCalculationTests
{
    public class TestBase
    {
        protected string ReadResourceFile(string filename)
        {
            string result;
            var assembly = Assembly.GetExecutingAssembly();
            
            using (Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + "." + filename))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
