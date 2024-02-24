using Newtonsoft.Json;
using System.Text;

namespace Eva.Infra.Tools.Serialization
{
    public static class EvaConvert
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static byte[] ToBytes(this string jsonString)
        {
            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
