using UnityEngine;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class ParserXml{
    public ParserXml(){
    }

    public List<Dictionary<string, string>> parseXml(string s){
        Dictionary<string, string> properties = new Dictionary<string, string>();
        List<Dictionary<string, string>> propertiesArray = new List<Dictionary<string, string>>();
        string key = "";
        string values = "";
        XmlReaderSettings xmlEinstellungen=new XmlReaderSettings();
        xmlEinstellungen.ProhibitDtd = false;
        using (XmlReader reader = XmlReader.Create(new StringReader(s), xmlEinstellungen)){
            while (reader.Read()){
                switch (reader.NodeType){
                    case XmlNodeType.Element:                        
                        key = reader["key"];
                        break;
                    case XmlNodeType.Text:                        
                        values = reader.Value;                        
                        properties.Add(key, values);
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals("properties")){
                            propertiesArray.Add(properties);                           
                            properties = null;
                            properties = new Dictionary<string, string>();
                        }
                        break;
                }
            }            
        }
        return propertiesArray;
    }    
}
