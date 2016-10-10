using UnityEngine;
using System.Collections;

public class LocationManager : MonoBehaviour {

    [HideInInspector] public static bool isEnabled = false;
    [HideInInspector] public static LocationInfo gpsCoordinates { get { return Input.location.lastData; } }

	// Use this for initialization
	void Start () {
        Begin();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        isEnabled = Input.location.isEnabledByUser;
    }

    void OnDestroy() {
        Stop();
    }

    public void Begin() {
        Input.location.Start();
    }

    public void Stop() {
        Input.location.Stop();
    }
}
