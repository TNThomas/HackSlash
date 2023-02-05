using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_CalmAudioSource;
    [SerializeField] private AudioSource m_PanicAudioSource;

    [SerializeField] private AudioClip calmTrack;
    [SerializeField] private AudioClip panicTrack;

    public AudioMixerSnapshot calmSnapshot;
    public AudioMixerSnapshot panicSnapshot;

    private void Awake()
    {
        if (m_CalmAudioSource == null || m_PanicAudioSource == null) Debug.LogError("One or more AudioSources are null!"); 

        if (calmTrack == null || panicTrack == null) Debug.LogError("One or more AudioClips are null!"); 

        if (calmSnapshot == null || panicSnapshot == null) Debug.LogError("One or more AudioSnapshots are null!"); 
    }

    private void Start()
    {
        // Set audio clips
        m_CalmAudioSource.clip = calmTrack;
        m_PanicAudioSource.clip = panicTrack;

        // Play tracks
        m_CalmAudioSource.Play();
        m_PanicAudioSource.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("MUSIC TRANSITION TO PANIC");
            TransitionToPanic();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("MUSIC TRANSITION TO CALM");
            TransitionToCalm();
        }
    }

    public void TransitionToCalm()
    {
        calmSnapshot.TransitionTo(2.5f);
    }

    public void TransitionToPanic()
    {
        panicSnapshot.TransitionTo(1f);
    }
}
