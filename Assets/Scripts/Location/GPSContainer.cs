using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System;

public class GPSContainer {
    [XmlArray("GPSCoodinates")]
    public List<GPSCoordinate> coordinates;

    public GPSContainer() {
        coordinates = new List<GPSCoordinate>();
    }
    public GPSContainer( List<GPSCoordinate> list) {
        coordinates = list;
    }

    public static GPSContainer Load(string path ) {
#if UNITY_ANDROID
        string completePath = Application.persistentDataPath +  "/" + path;
#else
        string completePath = path;
#endif
        if ( !File.Exists( completePath ) ) {
            Debug.Log( "No File" );
            return new GPSContainer();
        }
        Debug.Log( string.Format( "Loading from path: {0}", completePath ) );
        StreamReader stream = new StreamReader( completePath );
        string xml = stream.ReadToEnd();
        Debug.Log( string.Format( "XML: \n {0}", xml ) );
        GPSContainer gps;
        StringReader sr = new StringReader( xml );
        try {
            Debug.Log( "Trying to read file" );
            XmlSerializer serializer = new XmlSerializer( typeof( List<GPSCoordinate> ) );
            List<GPSCoordinate> list = serializer.Deserialize( sr ) as List<GPSCoordinate>;
            gps = new GPSContainer( list );
            Debug.Log( "Reading file complete" );
        }
        catch( XmlException  e) {
            Debug.Log( "Failure! Loading empty Array" );
            gps = new GPSContainer();
            Debug.Log( "Empty array initialized" );
        }
        catch(Exception e ) {
            Debug.Log( "Unknown Exception!" );
            gps = new GPSContainer();
            Debug.Log( "Empty array initialized" );
        }
        finally {
            Debug.Log( "Finished loading!" );
            sr.Close();
            stream.Close();
        }
        
        return gps;
    }

    public void Serialize( string path ) {
        XmlSerializer serializer = new XmlSerializer( typeof(List<GPSCoordinate> ) );
#if UNITY_ANDROID
        string completePath = Application.persistentDataPath + "/" + path;
#else
        string completePath = path;
#endif
        Debug.LogFormat(  "Saving to path: {0}", completePath  );
        TextWriter tw = new StreamWriter( completePath );
        serializer.Serialize( tw, coordinates );
        Debug.Log( "Save Finished" );
        tw.Close();
    }
}
