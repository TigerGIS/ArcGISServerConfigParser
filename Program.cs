using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ArcGISServerConfigParser
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("conf.cdi");

            double[] fullExtent = new double[4];
            int wkID = 0;

            XmlNode xmlNode = xmlDoc.SelectSingleNode("EnvelopeN/XMin");
            if (xmlNode != null)
            {
                fullExtent[0] = double.Parse(xmlNode.InnerText);
            }
            xmlNode = xmlDoc.SelectSingleNode("EnvelopeN/YMin");
            if (xmlNode != null)
            {
                fullExtent[1] = double.Parse(xmlNode.InnerText);
            }
            xmlNode = xmlDoc.SelectSingleNode("EnvelopeN/XMax");
            if (xmlNode != null)
            {
                fullExtent[2] = double.Parse(xmlNode.InnerText);
            }
            xmlNode = xmlDoc.SelectSingleNode("EnvelopeN/YMax");
            if (xmlNode != null)
            {
                fullExtent[3] = double.Parse(xmlNode.InnerText);
            }

            xmlNode = xmlDoc.SelectSingleNode("EnvelopeN/SpatialReference/WKID");
            if (xmlNode != null)
            {
                wkID = int.Parse(xmlNode.InnerText);
            }

            double[] origin = new double[2];
            xmlDoc.Load("conf.xml");

            xmlNode = xmlDoc.SelectSingleNode("CacheInfo/TileCacheInfo/TileOrigin/X");
            if (xmlNode != null)
            {
                origin[0] = double.Parse(xmlNode.InnerText);
            }
            xmlNode = xmlDoc.SelectSingleNode("CacheInfo/TileCacheInfo/TileOrigin/Y");
            if (xmlNode != null)
            {
                origin[1] = double.Parse(xmlNode.InnerText);
            }

            List<double> resolution = new List<double>();

            XmlNodeList nodes = xmlDoc.SelectNodes("CacheInfo/TileCacheInfo/LODInfos/LODInfo/Resolution");
            if (nodes != null && nodes.Count > 0)
            {
                foreach (XmlNode node in nodes)
                {
                    resolution.Add(double.Parse(node.InnerText));
                }
            }

            Console.WriteLine("var projection=ol.proj.get(\"EPSG: {0}\");", wkID);

            Console.WriteLine("var origin=[{0}];", string.Join(",", origin));

            Console.WriteLine("var resolutions=[{0}];", string.Join(",", resolution));

            Console.WriteLine("var fullExtent =[{0}]", string.Join(",", fullExtent));

            Console.WriteLine("--参数解析成功，请复制上面的代码到Openlayers中调用测试，敲任意键结束程序。");

            Console.ReadKey();
        }
    }
}
