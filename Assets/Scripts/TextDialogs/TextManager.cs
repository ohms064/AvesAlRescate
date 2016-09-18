using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TextManager : MonoBehaviour {
    XMLParser texts;
    [HideInInspector] public static TextManager instance;
    [SerializeField] Languages language;
    private const string GAME_STRINGS = "GameStrings.cs";
    // Use this for initialization
    void Awake() {
        instance = this;
        string xml = Resources.Load<TextAsset>( string.Format( "Languages/{0}", language ) ).text;
        texts = new XMLParser( xml );
    }

    public string GetDialog( string key ) {
        return texts.dialog[key];
    }

    public void ChangeLangauge( Languages newLanguage ) {
        language = newLanguage;
        string xml = Resources.Load<TextAsset>( string.Format( "Languages/{0}", language ) ).text;
        texts = new XMLParser( xml );
    }

#if UNITY_EDITOR
    public string CreateTextClass() {
        string xml = Resources.Load<TextAsset>( string.Format( "Languages/{0}", language ) ).text;
        Dictionary<string, string> keyTexts = XMLParser.CrateDict( xml );
        if ( File.Exists( string.Format("Assets/Resources/{0}", GAME_STRINGS ) ) ) {
            File.Delete( string.Format( "Assets/Resources/{0}", GAME_STRINGS ) );
        }
        using ( StreamWriter sw = File.CreateText( string.Format( "Assets/Resources/{0}", GAME_STRINGS ) ) ) {
            string line = "";
            sw.WriteLine( "//Auto generated class, do not change." );
            sw.Write( "\nclass GS {\n" );
            foreach(KeyValuePair<string, string> text in keyTexts ) {
                if ( text.Key.Contains( " " ) ) {
                    sw.Write( "}" );
                    return text.Key;
                }
                line = string.Format( "\tpublic static string {0} = \"{1}\";\n", text.Key, text.Key );
                sw.Write( line );
            }
            sw.Write( "}" );
        }

        return null;  
    }
#endif
}

