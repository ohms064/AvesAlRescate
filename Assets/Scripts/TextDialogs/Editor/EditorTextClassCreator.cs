using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor( typeof( ManagerAI ) )]
[CanEditMultipleObjects]
public class EditorTextClassCreator : Editor {
    public override void OnInspectorGUI() {
        TextClassCreator targetScript = target as TextClassCreator;
        DrawDefaultInspector();
        EditorGUILayout.HelpBox( "This will initialize the previous arrays. Before doing anything press this button or fill the arrays manually. The arrays can me modified.", MessageType.None );
        if ( GUILayout.Button( "Init Vars" ) ) {
            targetScript.InitVars();
        }

        EditorGUILayout.HelpBox( "This will affect only the TrackWaypoints on tracksOnScene.", MessageType.None );
        if ( GUILayout.Button( "Set High Precision to tracksOnScene." ) ) {
            targetScript.SetAllHighPrecision();
        }
    }

}
