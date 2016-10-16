using UnityEngine;
using System.Xml.Serialization;

public class GPSCoordinates {

    public GPSCoordinates(LocationInfo info ) {
        latitude = info.latitude;
        longitude = info.longitude;
        altitude = info.altitude;
        horizontalAccuracy = info.horizontalAccuracy;
        timestamp = info.timestamp;
        verticalAccuracy = info.verticalAccuracy;
    }

    [XmlElement("latitude")]
    public float latitude;

    [XmlElement("longitude")]
    public float longitude;

    [XmlElement("altitude")]
    public float altitude;

    [XmlElement("horizontal accuaracy")]
    public float horizontalAccuracy;

    [XmlElement( "timestamp" )]
    public double timestamp;

    [XmlElement( "vertical accuaracy" )]
    public float verticalAccuracy;

}
