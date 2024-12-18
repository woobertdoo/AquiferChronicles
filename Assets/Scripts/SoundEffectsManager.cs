using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioClip jumpSound;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
    
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void PlayFootstep() {
        /* 
        *  Get a random footstep sound and randomize pitch 
        *  to make footsteps sound more natural and less consistent 
        */
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        AudioClip randomStep = footsteps[Random.Range(0, footsteps.Length - 1)];
        audioSource.PlayOneShot(randomStep);
    }
    
    void PlayJumpSound() {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(jumpSound);
    }
    
    
    public void PlayAttackSound() {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(jumpSound);
    }
}
