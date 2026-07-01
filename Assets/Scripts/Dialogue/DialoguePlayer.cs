using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePlayer : Singleton<DialoguePlayer>
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private Image DialogueProfile;
    [SerializeField] private CinemachineCamera CinemachineCamera;
    [SerializeField] private float TypewriterSpeed = 0.1F;
    [SerializeField] private SerializedDictionary<Speaker, Sprite> SpeakerProfiles = new();

    private bool _isPlayingAudio;

    private Queue<KeyValuePair<AudioWithSubtitles, Transform>> queuedVoicelines = new();

    public static Action<AudioWithSubtitles, Transform> OnStartedPlayingAudio;
    public static Action<AudioWithSubtitles, Transform> OnFinishedPlayingAudio;

    protected override void Awake()
    {
        base.Awake();
        DialogueBox.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void NewDialogue(AudioWithSubtitles audioWithSubtitles, Transform talkingPlayer) 
    {
        var newItem = new KeyValuePair<AudioWithSubtitles, Transform>(audioWithSubtitles, talkingPlayer);
        queuedVoicelines.Enqueue(newItem);
        
        if (_isPlayingAudio)
            return;

        StartCoroutine(Internal_PlayNewDialogue());
    }

    private IEnumerator Internal_PlayNewDialogue()
    {
        var Dialogue = queuedVoicelines.Dequeue();
        AudioWithSubtitles Audio = Dialogue.Key;
        Transform Target = Dialogue.Value;

        _isPlayingAudio = true;

        OnStartedPlayingAudio?.Invoke(Audio, Target);

        CinemachineCamera.gameObject.SetActive(true);
        CinemachineCamera.Target.TrackingTarget = Target;

        DialogueProfile.sprite = SpeakerProfiles[Audio.Speaker];

        DialogueBox.SetActive(true);

        
        // Spela ljudet
        AudioSource.PlayOneShot(Audio.AudioClip);
        
        // Display text
        DialogueText.text = string.Empty;

        float occupiedTime = 0.0F;

        string finalText = Audio.Text;
        foreach (char c in finalText)
        {
            DialogueText.text += c;
            occupiedTime += TypewriterSpeed;
            yield return new WaitForSecondsRealtime(TypewriterSpeed);
        }

        // Vänta på att ljudet spelat klart
        yield return new WaitForSecondsRealtime(Audio.AudioClip.length - occupiedTime);


        DialogueBox.SetActive(false);
        CinemachineCamera.gameObject.SetActive(false);

        _isPlayingAudio = false;

        OnFinishedPlayingAudio?.Invoke(Audio, Target);
    
        if(queuedVoicelines.Count > 0) 
        {
            StartCoroutine(Internal_PlayNewDialogue());
        }
    }
}
