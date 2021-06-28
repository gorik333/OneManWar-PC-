using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource;
    
    public static BackgroundMusicController Instance { get; private set; }

    private void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!Instance)
        {
            Instance = this;
        }
        //otherwise, if we do, kill this thing
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void PauseMusic()
    {
        _musicAudioSource.Pause();
    }


    public void UnPauseMusic()
    {
        _musicAudioSource.UnPause();
    }
}
