using NUnit.Compatibility;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AuidoManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    EventCore eventcore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventcore = this.gameObject.GetComponent<EventCore>();
        eventcore.playSound.AddListener(playOneShot);
        m_AudioSource = GetComponent<AudioSource>();
    }

   void playOneShot(AudioClip ac)
    {
        m_AudioSource.PlayOneShot(ac);
    }
}
