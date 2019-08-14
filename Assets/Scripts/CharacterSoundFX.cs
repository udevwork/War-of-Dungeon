using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

public class CharacterSoundFX : MonoBehaviour {

    public AudioSource source;

    public AudioClip[] footSteps;
    public AudioClip[] SwordSounds;
    public AudioClip[] SwordSwapSounds;
    public AudioClip[] DeathSounds;
    public AudioClip[] PainSounds;
    public AudioClip[] AgressionSounds;


    public void PlayRandomFootstapSound()
    {
        source.PlayOneShot(footSteps[Random.Range(0,footSteps.Length)],Settings.SoundEffectsVolume);    
    }

    public void PlayRandomSwordSound()
    {
        source.PlayOneShot(SwordSounds[Random.Range(0,SwordSounds.Length)], Settings.SoundEffectsVolume);    
    }
    public void PlayRandomSwordSwapSound()
    {
        source.PlayOneShot(SwordSwapSounds[Random.Range(0, SwordSwapSounds.Length)], Settings.SoundEffectsVolume);
    }

    public void PlayRandomDeathSound()
    {
        source.PlayOneShot(DeathSounds[Random.Range(0, DeathSounds.Length)], Settings.SoundEffectsVolume);
    }

    public void PlayRandomAgression()
    {
        source.PlayOneShot(AgressionSounds[Random.Range(0, AgressionSounds.Length)], Settings.SoundEffectsVolume);
    }

    public void PlayRandomPain()
    {
        source.PlayOneShot(PainSounds[Random.Range(0, PainSounds.Length)], Settings.SoundEffectsVolume);
    }


}
