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
        if (!SFXSource.isPlaying) 
        {
            SFXSource.PlayOneShot(clip);
        }
        
    }
}
