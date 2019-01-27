using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClipCache AudioClips;

    public void Start()
    {
        MusicPlayer.InitializeClips(AudioClips);
    }

    public void Update()
    {
        MusicPlayer.TickTransport(Time.deltaTime);
    }

    static readonly AudioSource SFXPlayer = new AudioSource();

    public static void PlayEffect(AudioClip clip, 
                                float frequency = 1f, float amplitude=1f, 
                                float pitchJitter = 0f, float ampJitter = 0f)
    {
        float pitch = frequency + UnityEngine.Random.Range(-pitchJitter, pitchJitter);
        float amp = amplitude + UnityEngine.Random.Range(-ampJitter, ampJitter);
        SFXPlayer.PlayOneShot(clip);
    }

}
