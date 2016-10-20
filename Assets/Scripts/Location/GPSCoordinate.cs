using UnityEngine;
using System.Xml.Serialization;

public class GPSCoordinate {

    private const float EARTH_RADIUS = 6378.1370f;

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

    public float haversineDistance { get; set; }

    public static float Dot( GPSCoordinate first, GPSCoordinate second ) {
        Vector3 firstVector = new SphereCoordinate { latitude = first.latitude, longitude = first.longitude, magnitude = EARTH_RADIUS }.ToVector3();
        Vector3 secondVector = new SphereCoordinate { latitude = second.latitude, longitude = second.longitude, magnitude = EARTH_RADIUS }.ToVector3();
        return Vector3.Dot( firstVector, secondVector );
    }

    public static float HaversineDistance( GPSCoordinate first, GPSCoordinate second ) {
        float dlon = (second.longitude - first.longitude) * Mathf.Deg2Rad;
        float dlat = (second.latitude - first.latitude) * Mathf.Deg2Rad;
        float a = Mathf.Pow( (Mathf.Sin( dlat * 0.5f )), 2 ) + Mathf.Cos( first.latitude ) *
            Mathf.Cos(second.latitude) * Mathf.Pow( Mathf.Sin( dlon*0.5f),2);
        float c = 2 * Mathf.Atan2( Mathf.Sqrt( a ), Mathf.Sqrt( 1 - a ) );
        return c * EARTH_RADIUS;
    }

    /* Example http://stackoverflow.com/questions/365826/calculate-distance-between-2-gps-coordinates/7595937#7595937
     * 
     *  double _eQuatorialEarthRadius = 6378.1370D;
        double _d2r = (Math.PI / 180D);

        private int HaversineInM(double lat1, double long1, double lat2, double long2)
        {
            return (int)(1000D * HaversineInKM(lat1, long1, lat2, long2));
        }

        private double HaversineInKM(double lat1, double long1, double lat2, double long2)
        {
            double dlong = (long2 - long1) * _d2r;
            double dlat = (lat2 - lat1) * _d2r;
            double a = Math.Pow(Math.Sin(dlat / 2D), 2D) + Math.Cos(lat1 * _d2r) * Math.Cos(lat2 * _d2r) * Math.Pow(Math.Sin(dlong / 2D), 2D);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = _eQuatorialEarthRadius * c;

            return d;
        }
     */

}
