using UnityEngine;
using System.Collections;

public class SphereCoordinate{
	public float longitude;
	public float latitude;
	public float magnitude;

	public SphereCoordinate (Vector3 vector) {
		magnitude = vector.magnitude;
		if (vector.x == 0) {
			if (vector.z == 0f) {
				longitude = 0f;	
			}else if (vector.z > 0f) {
				longitude = 90f;
			}else {
				longitude = -90f;
			}
		}else {
			longitude = Mathf.Atan(vector.z / vector.x) * Mathf.Rad2Deg;	
		}
		latitude = Mathf.Asin(vector.y / magnitude) * Mathf.Rad2Deg;
		if (vector.x < 0f && vector.z < 0f ) {
			longitude += 270f;
		}else if (vector.x < 0f) {
			longitude += 180f;
		}
		
	}
	
	public SphereCoordinate () {
		latitude = 0;
		longitude = 0;
		magnitude = 0;
	}

	public void UpdateVar(){
		if (longitude > 360f) {
			longitude -= 360f;
		}else if (longitude < 0f) {
			longitude += 360f;
		}
	}

	public void UpdateCoord(Vector3 vector){
		magnitude = vector.magnitude;
		if (vector.x == 0) {
			if (vector.z == 0f) {
				longitude = 0f;	
			}else if (vector.z > 0f) {
				longitude = 90f;
			}else {
				longitude = -90f;
			}
		}else {
			longitude = Mathf.Atan(vector.z / vector.x) * Mathf.Rad2Deg;	
		}
		latitude = Mathf.Asin(vector.y / magnitude) * Mathf.Rad2Deg;
		if (vector.x < 0f && vector.z < 0f ) {
			longitude += 270f;
		}else if (vector.x < 0f) {
			longitude += 180f;
		}
		
	}

	public Vector3 ToVector3(){
		float ro = magnitude *Mathf.Cos(latitude * Mathf.Deg2Rad); 
		float x = ro * Mathf.Cos(longitude * Mathf.Deg2Rad);
		float y = magnitude * Mathf.Sin(latitude * Mathf.Deg2Rad);
		float z = ro * Mathf.Sin(longitude * Mathf.Deg2Rad);
		return new Vector3(x, y, z);
	}
}
