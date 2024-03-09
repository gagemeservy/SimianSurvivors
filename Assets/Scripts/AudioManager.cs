using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip enemyDeath1;
    public AudioClip enemyDeath2;
    public AudioClip Music;
    public AudioClip LevelUpGetItem;
    public AudioClip LevelDown;

    public AudioClip vine;
    public AudioClip skeleton;
    public AudioClip vine2;
    public AudioClip coconut;
    public AudioClip banana;
    public AudioClip grub;
    public AudioClip quicksand;

    public bool playSFXbool = true;

    private void Start()
    {
        GameObject[] audioPlayer = GameObject.FindGameObjectsWithTag("Audio");

        if (audioPlayer.Length > 1)
        {
            Destroy(audioPlayer[1]);
        }

        musicSource.clip = Music;

        if (!musicSource.isPlaying)
        {
            musicSource.Play();
            GameObject.DontDestroyOnLoad(audioPlayer[0]);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        //if ((SFXSource.isPlaying == false) && (playSFXbool == true)) 
        //{
            SFXSource.PlayOneShot(clip);
        //}
    }

    public void ToggleMusic()
    {
        if(musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        else if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void ToggleSFX()
    {
        playSFXbool = !playSFXbool;
    }


    /*public void PlayReverseSFX(AudioClip audioClip)
    {
        SFXSource.pitch = -1;
        SFXSource.loop = true;
        SFXSource.clip = audioClip;
        SFXSource.Play();
        StartCoroutine(StopLoop());
    }

    public IEnumerator StopLoop()
    {
        yield return new WaitForSeconds(1f);
        SFXSource.loop = false;
    }*/
}
