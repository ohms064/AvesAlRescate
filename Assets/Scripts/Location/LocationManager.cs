using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class LocationManager : MonoBehaviour, ITranslatable {

    private const string XmlPath = "XML/locations.xml";


    [HideInInspector]
    public static bool isEnabled { get { return Input.location.isEnabledByUser; } }
    [HideInInspector]
    public static LocationInfo lastCoordinate { get { return Input.location.lastData; } }
    private GPSContainer gpsData;
    private string gpsStatus;
    [SerializeField]
    private Text longText, latText, altText, statusText, locateButtonText, stopButtonText;

    // Use this for initialization
    void Start () {
        Begin();
        gpsData = GPSContainer.Load(XmlPath);

    }

    void OnDestroy() {
        Stop();
    }

    public void Begin() {
        Input.location.Start();
        StartCoroutine( "DetectLocation" );
    }

    public void Stop() {
        StopCoroutine( "DetectLocation" );
        Input.location.Stop();
    }

    IEnumerator DetectLocation() {
        // First, check if user has location service enabled
        if ( !isEnabled ) {
            gpsStatus = TextManager.dialogs.no_gps;
            statusText.text = string.Format( "{0}: {1}", TextManager.dialogs.status, gpsStatus );
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while ( Input.location.status == LocationServiceStatus.Initializing && maxWait > 0 ) {
            gpsStatus = TextManager.dialogs.waiting;
            statusText.text = string.Format( "{0}: {1} {2}", TextManager.dialogs.status, gpsStatus, maxWait );
            yield return new WaitForSeconds( 1 );
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if ( maxWait < 1 ) {
            gpsStatus = TextManager.dialogs.timeout;
            statusText.text = string.Format( "{0}: {1}", TextManager.dialogs.status, gpsStatus);
            yield break;
        }

        // Connection has failed
        if ( Input.location.status == LocationServiceStatus.Failed ) {
            gpsStatus = TextManager.dialogs.location_failure;
            statusText.text = string.Format( "{0}: {1}", TextManager.dialogs.status, gpsStatus );
            yield break;
        }
        else {
            gpsStatus = TextManager.dialogs.connected;
            statusText.text = string.Format( "{0}: {1}", TextManager.dialogs.status, gpsStatus);
            // Access granted and location value could be retrieved
            longText.text = string.Format("{0}: {1}", TextManager.dialogs.longitude, lastCoordinate.longitude.ToString());
            latText.text = string.Format( "{0}: {1}", TextManager.dialogs.latitude, lastCoordinate.latitude.ToString());
            altText.text = string.Format( "{0}: {1}", TextManager.dialogs.altitude, lastCoordinate.altitude.ToString());
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();

    }

    public void SaveLocation() {
        gpsData.coordinates.Add( new GPSCoordinates( lastCoordinate ) );
    }

    public void ChangeLanguage() {
        if ( isEnabled ) {
            locateButtonText.text = TextManager.dialogs.locate;
            stopButtonText.text = TextManager.dialogs.stop;
            statusText.text = string.Format( "{0}: {1}", TextManager.dialogs.status, gpsStatus );
            longText.text = string.Format( "{0}: {1}", TextManager.dialogs.longitude, lastCoordinate.longitude.ToString() );
            latText.text = string.Format( "{0}: {1}", TextManager.dialogs.latitude, lastCoordinate.latitude.ToString() );
            altText.text = string.Format( "{0}: {1}", TextManager.dialogs.altitude, lastCoordinate.altitude.ToString() );
        }
        else {
            locateButtonText.text = TextManager.dialogs.locate;
            stopButtonText.text = TextManager.dialogs.stop;
            statusText.text = string.Format( "{0}: {1}", TextManager.dialogs.status, TextManager.dialogs.no_gps );
            longText.text = string.Format( "{0}: ", TextManager.dialogs.longitude );
            latText.text = string.Format( "{0}: ", TextManager.dialogs.latitude );
            altText.text = string.Format( "{0}: ", TextManager.dialogs.altitude );
        }
    }
}
