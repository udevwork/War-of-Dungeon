using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

public class SoundFX : MonoBehaviour {

    public AudioSource source;
    public AudioSource MusicSource;

    // Music
    [Header("MUSIC")]
    [SerializeField] private AudioClip[] MenuMusic;
    [SerializeField] private AudioClip[] GameMusic;

    // Sound Effects
    [Header("SOUND EFFECTS")]
    [SerializeField] private AudioClip clicSound;
    [SerializeField] private AudioClip CardOpenSound;
    [SerializeField] private AudioClip CardCloseSound;
    [SerializeField] private AudioClip AlertOneSound;
    [SerializeField] private AudioClip AlertTwoSound;
    [SerializeField] private AudioClip openChest;
    [SerializeField] private AudioClip levelup;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;
    [SerializeField] private AudioClip putOnItem;
    [SerializeField] private AudioClip putOffItem;
    [SerializeField] private AudioClip pickUpPoison;
    [SerializeField] private AudioClip ExlosionClip;
    [SerializeField] private AudioClip ChestOpenClip;


    public static SoundFX play;

    private void Awake()
    {
        Settings.OnMusicVolumeChange += (float obj) => MusicSource.volume = obj;
        Settings.OnSoundEffectsVolumeChange += (float obj) => source.volume = obj;
        LevelModel.Game.OnGameStateChange += (LevelModel.Game.State obj) => PlayMusic(obj);
    }

    private void Start()
    {
        play = this;
        PlayMusic(LevelModel.Game.State.Menu);
    }


   
    public void PlayMusic(LevelModel.Game.State state){
        Debug.Log("MUSIC CHANGE TO : " + state.ToString());
        if(state == LevelModel.Game.State.Level){
            MusicSource.clip = GameMusic[Random.Range(0,GameMusic.Length)];
        }
        if (state == LevelModel.Game.State.Menu)
        {
            MusicSource.clip = MenuMusic[Random.Range(0, MenuMusic.Length)];
        }
        MusicSource.Play();
    }

    public void CustomSound(AudioClip clip){
        source.PlayOneShot(clip);
    }

    public void PickUpPoison()
    {
        source.PlayOneShot(pickUpPoison);
    }

    public void OpenChest()
    {
        source.PlayOneShot(openChest);
    }

    public void playerLevelUp()
    {
        source.PlayOneShot(levelup);
    }
 
    public void startSound()
    {
        source.PlayOneShot(start);
    }
    public void playerWin()
    {
        source.PlayOneShot(win);
    }
    public void playerLose()
    {
        source.PlayOneShot(lose);
    }
    public void Clic()
    {
        source.PlayOneShot(clicSound);
    }

    public void PutOn()
    {
        source.PlayOneShot(putOnItem);
    }

    public void PutOff()
    {
        source.PlayOneShot(putOnItem);
    }
    public void PlayExplosionSound()
    {
        source.PlayOneShot(ExlosionClip);
    }
    public void PlayChestOpenSound()
    {
        source.PlayOneShot(ChestOpenClip);
    }
    public void PlayCardOpenSound()
    {
        source.PlayOneShot(CardOpenSound);
    }
    public void PlayCardCloseSound()
    {
        source.PlayOneShot(CardCloseSound);
    }
    public void PlayAlertOneSound()
    {
        source.PlayOneShot(AlertOneSound);
    }
    public void PlayAlertTwoSound()
    {
        source.PlayOneShot(AlertTwoSound);
    }
}
