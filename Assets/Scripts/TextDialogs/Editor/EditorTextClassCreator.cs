using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor( typeof( TextManager ) )]
[CanEditMultipleObjects]
public class EditorTextClassCreator : Editor {
    public override void OnInspectorGUI() {
        TextManager targetScript = target as TextManager;
        DrawDefaultInspector();
        if ( GUILayout.Button( "Actualizar GameStrings" ) ) {
            string error = targetScript.UpdateLanguages();
            if ( error != null ) {
                Debug.Log( string.Format("Hubo un error {0}", error ));
            }
        }

    }

}
