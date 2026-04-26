using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip boxOpen;
    public AudioClip clueAppear;
    public AudioClip success;
    public AudioClip ambience;

    void Start()
    {
        PlayAmbience();
    }

    public void PlayBoxOpen()
    {
        audioSource.PlayOneShot(boxOpen);
    }

    public void PlayClue()
    {
        audioSource.PlayOneShot(clueAppear);
    }

    public void PlaySuccess()
    {
        audioSource.PlayOneShot(success);
    }

    public void PlayAmbience()
    {
        audioSource.clip = ambience;
        audioSource.loop = true;
        audioSource.Play();
    }
}