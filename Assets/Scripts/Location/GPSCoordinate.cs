using UnityEngine;
using System.Xml.Serialization;

public class GPSCoordinate {

    public GPSCoordinate(LocationInfo info, string name ) {
        latitude = info.latitude;
        longitude = info.longitude;
        altitude = info.altitude;
        horizontalAccuracy = info.horizontalAccuracy;
        timestamp = info.timestamp;
        verticalAccuracy = info.verticalAccuracy;
        this.name = name;
    }

    public GPSCoordinate() {

    }

    public float latitude { get; set; }

    public float longitude { get; set; }

    public float altitude { get; set; }

    public float horizontalAccuracy { get; set; }

    public double timestamp { get; set; }

    public float verticalAccuracy { get; set; }

    public string name { get; set; }

}
