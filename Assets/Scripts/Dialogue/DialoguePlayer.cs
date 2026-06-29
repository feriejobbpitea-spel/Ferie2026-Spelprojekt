using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class DialoguePlayer : Singleton<DialoguePlayer>
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private CinemachineCamera CinemachineCamera;

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


        DialogueBox.SetActive(true);

        // Display text
        DialogueText.text = Audio.Text;
        
        // Spela ljudet
        AudioSource.PlayOneShot(Audio.AudioClip);
        

        // Vänta på att ljudet spelat klart
        yield return new WaitForSeconds(Audio.AudioClip.length);


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
