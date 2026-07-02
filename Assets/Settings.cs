using UnityEngine;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    public AudioMixer MainMixer;
 public void SetVolume(float volume)
    {
        MainMixer.SetFloat("volume", volume);
    }
}
