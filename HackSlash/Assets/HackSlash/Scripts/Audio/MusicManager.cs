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

    [SerializeField] private AudioMixer musicMixer;

    public AudioMixerSnapshot calmSnapshot;
    public AudioMixerSnapshot panicSnapshot;

    private readonly AudioMixerSnapshot[] BGMSnapshots = new AudioMixerSnapshot[2];
    private readonly float[] snapshotBlendWeights = new float[] { 0, 0 };

    private ActorsManager m_ActorsManager;
    private Health m_PlayerHealth;

    private void Awake()
    {
        if (m_CalmAudioSource == null || m_PanicAudioSource == null) Debug.LogError("One or more AudioSources are null!"); 

        if (calmTrack == null || panicTrack == null) Debug.LogError("One or more AudioClips are null!"); 

        if (calmSnapshot == null || panicSnapshot == null) Debug.LogError("One or more AudioSnapshots are null!");

        BGMSnapshots[0] = calmSnapshot;
        BGMSnapshots[1] = panicSnapshot;
    }

    private void Start()
    {
        m_ActorsManager = FindObjectOfType<ActorsManager>();
        //DebugUtility.HandleErrorIfNullFindObject<ActorsManager, MusicManager>(m_ActorsManager, this);

        if(m_ActorsManager != null)
        {
            m_PlayerHealth = m_ActorsManager.Player.GetComponent<Health>();
            //DebugUtility.HandleErrorIfNullGetComponent<Health, MusicManager>(m_PlayerHealth, this, m_ActorsManager.Player);

            if (m_PlayerHealth != null)
            {
                m_PlayerHealth.OnDamaged += BlendBMGEncapsulated;
                m_PlayerHealth.OnHealed += BlendBMGEncapsulated;
            }
        }

        // Set audio clips
        m_CalmAudioSource.clip = calmTrack;
        m_PanicAudioSource.clip = panicTrack;

        // Play tracks
        m_CalmAudioSource.Play();
        m_PanicAudioSource.Play();

        //playerHealth.OnDamaged += new(() => BlendBGM(playerHealth.CurrentHealth, playerHealth.MaxHealth));
        //playerHealth.OnHealed += new(() => BlendBGM(playerHealth.CurrentHealth, playerHealth.MaxHealth));
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.I))
    //    {
    //        Debug.Log("MUSIC TRANSITION TO PANIC");
    //        TransitionToPanic();
    //    }

    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        Debug.Log("MUSIC TRANSITION TO CALM");
    //        TransitionToCalm();
    //    }
    //}

    //Lmao bad Gamejam code is bad
    void BlendBMGEncapsulated(float amount, GameObject source) => BlendBMGEncapsulated(amount);
    void BlendBMGEncapsulated(float amount)
    {
        BlendBGM(m_PlayerHealth.CurrentHealth, m_PlayerHealth.MaxHealth);
    }

    public void BlendBGM(float faderPos, float blendThreshold = 100)
    {
        snapshotBlendWeights[0] = faderPos;
        snapshotBlendWeights[1] = blendThreshold - faderPos;
        musicMixer.TransitionToSnapshots(BGMSnapshots, snapshotBlendWeights, 0.2f);
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
