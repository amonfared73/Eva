using System.Security.Cryptography;
using System.Xml;

namespace Eva.Core.ApplicationService.Encryptors
{
    public class RsaParser
    {
        public RSAParameters ParseRsaParametersFromXml(string xml)
        {
            RSAParameters rsaParameters = new RSAParameters();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            XmlNodeList nodes = xmlDocument.DocumentElement.ChildNodes;

            foreach (XmlNode node in nodes)
            {
                switch (node.Name)
                {
                    case "Modulus":
                        rsaParameters.Modulus = Convert.FromBase64String(node.InnerText); 
                        break;
                    case "Exponent":
                        rsaParameters.Exponent = Convert.FromBase64String(node.InnerText);
                        break;
                    case "P":
                        rsaParameters.P = Convert.FromBase64String(node.InnerText);
                        break;
                    case "Q":
                        rsaParameters.Q = Convert.FromBase64String(node.InnerText);
                        break;
                    case "DP":
                        rsaParameters.DP = Convert.FromBase64String(node.InnerText);
                        break;
                    case "DQ":
                        rsaParameters.DQ = Convert.FromBase64String(node.InnerText);
                        break;
                    case "InverseQ":
                        rsaParameters.InverseQ = Convert.FromBase64String(node.InnerText);
                        break;
                    case "D":
                        rsaParameters.D = Convert.FromBase64String(node.InnerText);
                        break;
                }
            }
            return rsaParameters;
        }
    }
}
