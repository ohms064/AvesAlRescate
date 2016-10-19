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

    public static float Dot( GPSCoordinate first, GPSCoordinate second ) {
        Vector3 firstVector = new SphereCoordinate { latitude = first.latitude, longitude = first.longitude, magnitude = 100 }.ToVector3();
        Vector3 secondVector = new SphereCoordinate { latitude = second.latitude, longitude = second.longitude, magnitude = 100 }.ToVector3();
        return Vector3.Dot( firstVector, secondVector );
    }

}
