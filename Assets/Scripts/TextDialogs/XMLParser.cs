using System.Collections.Generic;
using System.Xml;
using System.IO;

public class XMLParser {
    private const string DIALOG = "dialog", NARRATIVE = "narrative", TYPE = "type", NAME = "name", STRING_KEY = "string", TEXTS = "text", LANGUAGE = "language";
    public Dictionary<string, string> dialog;

    public XMLParser( string xmlString ) {
        string attribute;
        dialog = new Dictionary<string, string>();

        using ( XmlReader reader = XmlReader.Create( new StringReader( xmlString ) ) ) {
            //if ( (reader.NodeType == XmlNodeType.Element) && (reader.Name.Equals( TEXTS )) && reader.Read() ) {
            //    lang = reader.GetAttribute( LANGUAGE );
            //}
            while ( reader.Read() ) {
                if ( (reader.NodeType == XmlNodeType.Element) && (reader.Name.Equals( STRING_KEY )) ) {
                    attribute = reader.GetAttribute( NAME );
                    if ( reader.Read() ) {
                        dialog.Add( attribute, reader.Value );
                    }
                }
            }
        }
    }
    

    public static Dictionary<string, string> CreateDict( string xmlString ) {
        Dictionary<string, string> output = new Dictionary<string, string>();
        string attribute;

        using ( XmlReader reader = XmlReader.Create( new StringReader( xmlString ) ) ) {
            while ( reader.Read() ) {
                if ( (reader.NodeType == XmlNodeType.Element) && (reader.Name.Equals( STRING_KEY )) ) {
                    attribute = reader.GetAttribute( NAME );
                    if ( reader.Read() ) {
                        output.Add( attribute, reader.Value );
                    }
                }
            }
        }
        return output;
    }
}