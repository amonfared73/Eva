using Newtonsoft.Json;
using System.Text;

namespace Eva.Infra.Tools.Serialization
{
    public static class EvaConvert
    {
        /// <summary>
        /// Extension method to convert any object to its own respective json string format
        /// </summary>
        /// <param name="obj">Accepts any object as an input parameter</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// Extension method to convert any string to its own respective array of bytes
        /// </summary>
        /// <param name="obj">Accepts json string as an input parameter</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string jsonString)
        {
            return Encoding.UTF8.GetBytes(jsonString);
        }
        /// <summary>
        /// Extension method to convert any object to Base64 byte format
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this object obj)
        {
            return obj.ToJson().ToBytes();
        }
    }
}
