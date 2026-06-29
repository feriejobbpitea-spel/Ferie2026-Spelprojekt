using UnityEngine;

public enum Speaker 
{
    Erich,
    EMS,
    JKubb,
    Sparkle
}

[CreateAssetMenu]
public class AudioWithSubtitles : ScriptableObject
{
    public AudioClip AudioClip;
    public string Text;
    public Speaker Speaker;
}
