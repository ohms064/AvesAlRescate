using UnityEngine;
using System.Collections;

public class TextManager : MonoBehaviour {
    XMLParser texts;
    public static TextManager instance;
    [SerializeField] Languages language;
    // Use this for initialization
    void Start () {
        instance = this;
        string xml = Resources.Load<TextAsset>( string.Format( "Languages/{0}", language ) ).text;
        texts = new XMLParser( xml );
    }

    public string GetDialog(string key ) {
        return texts.dialog[key];
    }

    public string GetNarration( string key ) {
        return texts.narrative[key];
    }

    public void ChangeLangauge(Languages newLanguage ) {
        language = newLanguage;
        string xml = Resources.Load<TextAsset>( string.Format( "Languages/{0}", language ) ).text;
        texts = new XMLParser( xml );
    }
}
