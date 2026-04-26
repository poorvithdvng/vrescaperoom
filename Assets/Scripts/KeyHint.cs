using UnityEngine;

public class KeyHint : MonoBehaviour
{
    public Light glowLight;
    public ParticleSystem glowParticles;
    public GameObject secondKey;

    void Start()
    {
        // Remove SetActive from here - CluePopup handles it
        if (glowLight) glowLight.enabled = false;
        if (glowParticles) glowParticles.Stop();
    }

    public void ActivateHint()
    {
        if (glowLight) glowLight.enabled = true;
        if (glowParticles) glowParticles.Play();
    }
}