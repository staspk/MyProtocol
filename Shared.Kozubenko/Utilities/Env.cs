using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kozubenko.Utilities
{
    /// <summary>
    /// Will save this skeleton for the time being. May finish bringing over Kozubenko.Env.py when/if the need actually arises
    /// </summary>
    public class Env
    {
        public static Dictionary<string, string> Vars = new Dictionary<string, string>();

        public static string PathToEnvFile = "";
        private static string DefaultEnvAbsPath                               // Assumption: .env file resides on Solution level, will only work during development
        {
            get     
            {
                var splitStr = Directory.GetCurrentDirectory().Split('\\');   // C:\Users\stasp\Desktop\C#\Shared.Kozubenko\Shared.Kozubenko\bin\Debug\net9.0
                string path = "";
                for (int i = 0; i < splitStr.Length - 4; i++)                 // 3 levels up
                {
                    path += $"{splitStr[i]}\\";
                }

                string absPath = $"{path}.env";                               // C:\Users\stasp\Desktop\C#\Shared.Kozubenko\.env

                if (File.Exists(absPath))
                    return absPath;
                else
                    throw new Exception($".env file not found at {absPath}");
            }
        }

        public static void Load(string pathToEnvFile = null, string keyToDelete = null)
        {

        }

        public static void Save(string key, string value)
        {

        }
    }
}
