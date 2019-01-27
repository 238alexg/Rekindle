using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer
{
    static readonly float SecondsPerMeasure = 1f / 3f;

    static MusicPlayer _Inst;
    static GameObject AudioTransportManager;
    public static MusicPlayer Inst
    {
        get
        {
            if (_Inst == null)
            {
                AudioTransportManager = AudioManager.Inst.gameObject;
                _Inst = new MusicPlayer();
            }
            return _Inst;
        }
    }

    float MeasureTime = 0f;
    float CurrentMood = 0f;
    bool MeasureReached = false;
    bool UsingHappyChords = false;

    AudioSource Drums;
    AudioSource Shakers;
    AudioSource Bass;
    AudioSource Piano;
    AudioSource Horns;
    AudioSource MainMelody;
    AudioSource CounterMelody;
    AudioSource Glock;

    AudioClip DrumLoop;
    AudioClip ShakerLoop;
    AudioClip HappyBassLoop;
    AudioClip SadBassLoop;
    AudioClip HappyPianoLoop;
    AudioClip SadPianoLoop;
    AudioClip HappyHornLoop;
    AudioClip SadHornLoop;
    AudioClip HappyMainMelodyLoop;
    AudioClip SadMainMelodyLoop;
    AudioClip HappyCounterMelodyLoop;
    AudioClip SadCounterMelodyLoop;
    AudioClip HappyGlockLoop;
    AudioClip SadGlockLoop;

    MusicPlayer()
    {
        Drums = AudioTransportManager.AddComponent<AudioSource>();
        Shakers = AudioTransportManager.AddComponent<AudioSource>();
        Bass = AudioTransportManager.AddComponent<AudioSource>();
        Piano = AudioTransportManager.AddComponent<AudioSource>();
        Horns = AudioTransportManager.AddComponent<AudioSource>();
        MainMelody = AudioTransportManager.AddComponent<AudioSource>();
        CounterMelody = AudioTransportManager.AddComponent<AudioSource>();
        Glock = AudioTransportManager.AddComponent<AudioSource>();
    }

    public void InitializeClips(AudioClipCache audioClips)
    {
        Drums.clip = DrumLoop = audioClips.DrumLoop;
        Shakers.clip = ShakerLoop = audioClips.ShakerLoop;
        HappyBassLoop = audioClips.HappyBassLoop;
        Bass.clip = SadBassLoop = audioClips.SadBassLoop;
        HappyPianoLoop = audioClips.HappyPianoLoop;
        Piano.clip = SadPianoLoop = audioClips.SadPianoLoop;
        HappyHornLoop = audioClips.HappyHornLoop;
        Horns.clip = SadHornLoop = audioClips.SadHornLoop;
        HappyMainMelodyLoop = audioClips.HappyMainMelodyLoop;
        MainMelody.clip = SadMainMelodyLoop = audioClips.SadMainMelodyLoop;
        HappyCounterMelodyLoop = audioClips.HappyCounterMelodyLoop;
        CounterMelody.clip = SadCounterMelodyLoop = audioClips.SadCounterMelodyLoop;
        HappyGlockLoop = audioClips.HappyGlockLoop;
        Glock.clip = SadGlockLoop = audioClips.SadGlockLoop;

        IsLooping = true;
        Play();
    }


    public void TickTransport(float time)
    {
        if (IsPlaying)
        {
            MeasureTime += time;

            if (MeasureTime > SecondsPerMeasure)
                MeasureReached = true;
            else
                MeasureReached = false;

            MeasureTime %= SecondsPerMeasure;
        }
    }

    public bool IsPlaying
    {
        get
        {
            return
            Drums.isPlaying ||
            Shakers.isPlaying ||
            Bass.isPlaying ||
            Piano.isPlaying ||
            Horns.isPlaying ||
            MainMelody.isPlaying ||
            CounterMelody.isPlaying ||
            Glock.isPlaying;
        }
    }

    public bool IsLooping
    {
        get
        {
            return
            Drums.loop ||
            Shakers.loop ||
            Bass.loop ||
            Piano.loop ||
            Horns.loop ||
            MainMelody.loop ||
            CounterMelody.loop ||
            Glock.loop;
        }
        set
        {
            Drums.loop =
            Shakers.loop =
            Bass.loop =
            Piano.loop =
            Horns.loop =
            MainMelody.loop =
            CounterMelody.loop =
            Glock.loop = value;
        }
    }

    public float SampleTime
    {
        get
        {
            return Drums.time;
        }
        set
        {
            Drums.time =
            Shakers.time =
            Bass.time =
            Piano.time =
            Horns.time =
            MainMelody.time =
            CounterMelody.time =
            Glock.time = value;
        }
    }

    void SwapChords()
    {
        float loopTime = SampleTime;
        if (UsingHappyChords)
        {
            Bass.clip = SadBassLoop;
            Piano.clip = SadPianoLoop;
            Horns.clip = SadHornLoop;
            MainMelody.clip = SadMainMelodyLoop;
            CounterMelody.clip = SadCounterMelodyLoop;
            Glock.clip = SadGlockLoop;
            UsingHappyChords = false;
        }
        else
        {
            Bass.clip = HappyBassLoop;
            Piano.clip = HappyPianoLoop;
            Horns.clip = HappyHornLoop;
            MainMelody.clip = HappyMainMelodyLoop;
            CounterMelody.clip = HappyCounterMelodyLoop;
            Glock.clip = HappyGlockLoop;
            UsingHappyChords = true;
        }
        SampleTime = loopTime;
        Play();

    }

    public IEnumerator SwapChordsAtNextMeasure()
    {
        while (!MeasureReached)
            yield return null;

        SwapChords();
    }

    public void UpdateMood(float mood)
    {
        // 0f == despondent
        // 1f == good times
        Piano.volume = Mathf.Lerp(1f, 0.25f, mood);
        Drums.volume = mood > 0.2f ? Mathf.Lerp(0f, .75f, mood) : 0f;
        Glock.volume = Bass.volume = Drums.volume;
        Horns.volume = mood > 0.5f ? 0.75f : Mathf.Lerp(0f, 0.75f, 2 * mood);
        MainMelody.volume = Mathf.Lerp(0f, 1f, mood);
        CounterMelody.volume = mood < 0.5f ? Mathf.Lerp(0f, .5f, 2 * mood) : Mathf.Lerp(1f, 0f, mood);
        Shakers.volume = Mathf.Lerp(0f, .75f, mood);

        if (CurrentMood < 0.75f && mood > 0.75f || CurrentMood > 0.75f && mood < 0.75f)
            Application.Inst.StartCoroutine(SwapChordsAtNextMeasure());

        CurrentMood = mood;    
    }

    public void Play()
    {
        Drums.Play();
        Shakers.Play();
        Bass.Play();
        Piano.Play();
        Horns.Play();
        MainMelody.Play();
        CounterMelody.Play();
        Glock.Play();
    }

    public void Pause()
    {
        Drums.Pause();
        Shakers.Pause();
        Bass.Pause();
        Piano.Pause();
        Horns.Pause();
        MainMelody.Pause();
        CounterMelody.Pause();
        Glock.Pause();
    }

}
