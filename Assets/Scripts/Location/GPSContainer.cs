using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("GPS")]
public class GPSContainer {

    [XmlArray( "coordinates" )]
    [XmlArrayItem( "coordinate" )]
    public List<GPSCoordinates> coordinates;

    public GPSContainer() {
        coordinates = new List<GPSCoordinates>();
    }

    public static GPSContainer Load(string path ) {
        TextAsset xml = Resources.Load<TextAsset>( path );
        XmlSerializer serializer = new XmlSerializer( typeof( GPSContainer ));
        StringReader sr = new StringReader( xml.text );
        GPSContainer gps = serializer.Deserialize( sr ) as GPSContainer;
        sr.Close();
        return gps;
    }

    public void Serialize( string path ) {
        XmlSerializer serializer = new XmlSerializer( typeof( GPSContainer ) );
        TextWriter tw = new StreamWriter( path );
        serializer.Serialize( tw, coordinates );
        tw.Close();
    }
}
