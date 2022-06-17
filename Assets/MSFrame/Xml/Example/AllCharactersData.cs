using MSFrame.Xml;
using System.Collections;
using System.Xml;
using UnityEngine;


public class AllCharactersData : XmlBaseReader<int, CharaterData>
{
    public override string xmlText => GetXmlFile();

    private AllCharactersData()
    {

    }

    public override CharaterData CreateDataInstance(XmlElement data)
    {
        return new CharaterData()
        {
            ID = data.GetInt("ID"),
            Name = data.GetString("Name"),
            Attack = data.GetFloat("Attack"),
            Defence = data.GetFloat("Defence"),
            Speed = data.GetFloat("Speed"),
        };
    }

    public static string GetXmlFile()
    {
        //Read Xml File here...
        return null;
    }
}
