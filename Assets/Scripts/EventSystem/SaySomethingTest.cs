using UnityEngine;
using System.Collections;

public class SaySomethingTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log( TextManager.dialogs.test );
	}
	
	// Update is called once per frame
	void Update () {
        var va = TextManager.dialogs;
        print( string.Format( "valor: {0} {1}", va, va.test ));
        if ( Input.GetKeyDown( KeyCode.E ) ) {
            print( "Inglés" );
            TextManager.instance.ChangeLangauge( Languages.English );
        }
        else if ( Input.GetKeyDown( KeyCode.S ) ) {
            print( "Español" );
            TextManager.instance.ChangeLangauge( Languages.Spanish );
        }
    }
}
