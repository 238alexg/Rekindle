using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClipCache AudioClips;

    static AudioManager _Inst;

    public static AudioManager Inst
    {
        get
        {
            return _Inst;
        }
        private set
        {
            if(_Inst == null)
            {
                _Inst = value;
                SFXPlayer = _Inst.gameObject.AddComponent<AudioSource>();
            }
        }
    }

    static AudioSource SFXPlayer;

    public void Start()
    {
        Inst = this;
        MusicPlayer.Inst.InitializeClips(AudioClips);
    }

    public void Update()
    {
        MusicPlayer.Inst.TickTransport(Time.deltaTime);
    }

    public void PlayEffect(AudioClip clip)
    {
        Inst.PlayEffect(clip, 1f, 1f);
    }

    public void PlayEffect(AudioClip clip, 
                                float frequency, float amplitude, 
                                float pitchJitter = 0f, float ampJitter = 0f)
    {
        float pitch = frequency + Random.Range(-pitchJitter, pitchJitter);
        float amp = amplitude + Random.Range(-ampJitter, ampJitter);
        SFXPlayer.pitch = pitch;
        SFXPlayer.PlayOneShot(clip, amp);
    }

}
