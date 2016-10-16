using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class TextManager : MonoBehaviour {

    [HideInInspector] public static TextManager instance;
    [SerializeField] Languages language;
    [HideInInspector] public static GameStrings dialogs;
    ITranslatable[] texts;

    private const string GAME_STRINGS = "GameStrings", EXTENSION = "cs";
    static Dictionary<string, bool> areUsed = new Dictionary<string, bool>();
    // Use this for initialization
    void Awake() {
        instance = this;
    }

    void Start() {
        texts = InterfaceHelper.FindObjects<ITranslatable>();
        ChangeLangauge( language);
    }

    public void ChangeLangauge( Languages newLanguage ) {
        LangaugeChooser( newLanguage );
        foreach (ITranslatable t in texts ) {
            t.ChangeLanguage();
        }
    }

    private void LangaugeChooser( Languages newLanguage ) {
        switch ( newLanguage ) {
            case Languages.English:
                dialogs = new English();
                break;
            case Languages.Spanish:
                dialogs = new Spanish();
                break;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Crea una clase con los valores de las cadenas para un lenguaje.
    /// </summary>
    /// <param name="language"></param>
    /// <returns>Mensajes de error en caso de existir uno.</returns>
    public string CreateTextClass( Languages language ) {
        string outputLangFile = string.Format( "{0}_{1}.{2}", GAME_STRINGS, language, EXTENSION );
        string xml = Resources.Load<TextAsset>( string.Format( "Languages/{0}", language ) ).text;

        Dictionary<string, string> keyTexts = XMLParser.CreateDict( xml );
        if ( File.Exists( string.Format("Assets/Resources/{0}", outputLangFile ) ) ) {
            File.Delete( string.Format( "Assets/Resources/{0}", outputLangFile ) );
        }
        FileStream fs = null;
        try {
            fs = new FileStream( string.Format( "Assets/Resources/{0}", outputLangFile), FileMode.CreateNew );
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8 )) {
                string line = "";
                sw.WriteLine( "//Auto generated class, do not change." );
                sw.Write( string.Format( "\npublic class {0}: {1} {{\n", language, GAME_STRINGS ) );
                sw.WriteLine( string.Format( "\tpublic {0}(){{", language ));
                foreach ( KeyValuePair<string, string> text in keyTexts ) {
                    if ( text.Key.Contains( " " ) ) {
                        sw.Write( "}" );
                        return text.Key;
                    }
                    areUsed[text.Key] = true;
                    line = string.Format( "\t\t{0} = \"{1}\";\n", text.Key, text.Value );
                    sw.Write( line );
                }
                sw.Write( "\t}\n}" );
            }
        }
        catch {
            return "File error";
        }
        finally {
            if(fs != null ) {
                fs.Dispose();
            }
        }
        return null;  
    }

    /// <summary>
    /// Creates the base class where all languages classes will inherit from.
    /// </summary>
    /// <returns>An error message if any.</returns>
    public string CreateBaseTextClass() {
        string outputBaseFile = string.Format( "{0}.{1}", GAME_STRINGS, EXTENSION );

        if ( File.Exists( string.Format( "Assets/Resources/{0}", outputBaseFile ) ) ) {
            File.Delete( string.Format( "Assets/Resources/{0}", outputBaseFile ) );
        }

        using ( StreamWriter sw = File.CreateText( string.Format( "Assets/Resources/{0}", outputBaseFile ) ) ) {
            string line = "";
            sw.WriteLine( "//Auto generated class, do not change." );
            sw.WriteLine( string.Format("\npublic class  {0}{{", GAME_STRINGS ));
            foreach ( KeyValuePair<string, bool> isUsed in areUsed ) {
                if ( isUsed.Key.Contains( " " ) ) {
                    sw.Write( "}" );
                    return isUsed.Key;
                }
                if ( isUsed.Value ) {
                    line = string.Format( "\tpublic string {0};\n", isUsed.Key );
                    sw.Write( line );
                }else {
                    areUsed.Remove( isUsed.Key );
                }
            }
            sw.Write( "}" );
        }

        Debug.Log( "Archivos creados exitosamente" );

        return null;
    }

    /// <summary>
    /// Updates all languages classes.
    /// </summary>
    /// <returns>A error message if any.</returns>
    public string UpdateLanguages() {
        foreach(KeyValuePair<string, bool> isUsed in areUsed ) {
            areUsed[isUsed.Key] = false;
        }
        foreach(Languages l in Enum.GetValues( typeof(Languages) )) {
            string message = CreateTextClass( l );
            if ( message != null ) {
                return message;
            }
        }

        return CreateBaseTextClass();

    }
#endif
}

