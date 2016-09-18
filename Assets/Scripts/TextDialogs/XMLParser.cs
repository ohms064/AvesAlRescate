using System.Collections.Generic;
using System.Xml;
using System.IO;

public class XMLParser {
    private const string DIALOG = "dialog", NARRATIVE = "narrative", TYPE = "type", NAME = "name", STRING_KEY = "string";
    public Dictionary<string, string> dialog, narrative;

    public XMLParser( string xmlString ) {
        dialog = new Dictionary<string, string>();
        narrative = new Dictionary<string, string>();
        string attribute;

        using ( XmlReader reader = XmlReader.Create( new StringReader( xmlString ) ) ) {
            while ( reader.Read() ) {
                if ( (reader.NodeType == XmlNodeType.Element) && (reader.Name.Equals( STRING_KEY )) ) {
                    switch ( reader.GetAttribute( TYPE ) ) {
                        case DIALOG:
                            attribute = reader.GetAttribute( NAME );
                            if ( reader.Read() ) {
                                dialog.Add( attribute, reader.Value );
                            }
                            break;
                        case NARRATIVE:
                        default:
                            attribute = reader.GetAttribute( NAME );
                            if ( reader.Read() ) {
                                narrative.Add( attribute, reader.Value );
                            }
                            break;
                    }
                }
            }
        }//end using
    }

    public static Dictionary<string, string> CrateDict( string xmlString ) {
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