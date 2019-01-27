using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer
{
    static readonly float SecondsPerMeasure = 1f / 3f;

    WaitForSeconds WaitForMeasure = new WaitForSeconds(SecondsPerMeasure);

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
    AudioSource BassH, BassS;
    AudioSource PianoH, PianoS;
    AudioSource HornsH, HornsS;
    AudioSource MainMelodyH, MainMelodyS;
    AudioSource CounterMelodyH, CounterMelodyS;
    AudioSource GlockH, GlockS;

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

        BassH = AudioTransportManager.AddComponent<AudioSource>();
        PianoH = AudioTransportManager.AddComponent<AudioSource>();
        HornsH = AudioTransportManager.AddComponent<AudioSource>();
        MainMelodyH = AudioTransportManager.AddComponent<AudioSource>();
        CounterMelodyH = AudioTransportManager.AddComponent<AudioSource>();
        GlockH = AudioTransportManager.AddComponent<AudioSource>();

        BassS = AudioTransportManager.AddComponent<AudioSource>();
        PianoS = AudioTransportManager.AddComponent<AudioSource>();
        HornsS = AudioTransportManager.AddComponent<AudioSource>();
        MainMelodyS = AudioTransportManager.AddComponent<AudioSource>();
        CounterMelodyS = AudioTransportManager.AddComponent<AudioSource>();
        GlockS = AudioTransportManager.AddComponent<AudioSource>();
    }

    public void InitializeClips(AudioClipCache audioClips)
    {
        Drums.clip = DrumLoop = audioClips.DrumLoop;
        Shakers.clip = ShakerLoop = audioClips.ShakerLoop;
        BassH.clip = HappyBassLoop = audioClips.HappyBassLoop;
        BassS.clip = SadBassLoop = audioClips.SadBassLoop;
        PianoH.clip = HappyPianoLoop = audioClips.HappyPianoLoop;
        PianoS.clip = SadPianoLoop = audioClips.SadPianoLoop;
        HornsH.clip = HappyHornLoop = audioClips.HappyHornLoop;
        HornsS.clip = SadHornLoop = audioClips.SadHornLoop;
        MainMelodyH.clip = audioClips.HappyMainMelodyLoop;
        MainMelodyS.clip = SadMainMelodyLoop = audioClips.SadMainMelodyLoop;
        CounterMelodyH.clip = HappyCounterMelodyLoop = audioClips.HappyCounterMelodyLoop;
        CounterMelodyS.clip = SadCounterMelodyLoop = audioClips.SadCounterMelodyLoop;
        GlockH.clip = HappyGlockLoop = audioClips.HappyGlockLoop;
        GlockS.clip = SadGlockLoop = audioClips.SadGlockLoop;

        BassS.clip = SadBassLoop;
        PianoS.clip = SadPianoLoop;
        HornsS.clip = SadHornLoop;
        MainMelodyS.clip = SadMainMelodyLoop;
        CounterMelodyS.clip = SadCounterMelodyLoop;
        GlockS.clip = SadGlockLoop;

        BassH.clip = HappyBassLoop;
        BassH.volume = 0f;
        PianoH.clip = HappyPianoLoop;
        PianoH.volume = 0f;
        HornsH.clip = HappyHornLoop;
        HornsH.volume = 0f;
        MainMelodyH.clip = HappyMainMelodyLoop;
        MainMelodyH.volume = 0f;
        CounterMelodyH.clip = HappyCounterMelodyLoop;
        CounterMelodyH.volume = 0f;
        GlockH.clip = HappyGlockLoop;
        GlockH.volume = 0f;

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
            BassS.isPlaying ||
            BassH.isPlaying ||
            PianoS.isPlaying ||
            PianoH.isPlaying ||
            HornsS.isPlaying ||
            HornsH.isPlaying ||
            MainMelodyS.isPlaying ||
            MainMelodyH.isPlaying ||
            CounterMelodyS.isPlaying ||
            CounterMelodyH.isPlaying ||
            GlockS.isPlaying ||
            GlockH.isPlaying;
        }
    }

    public bool IsLooping
    {
        get
        {
            return
            Drums.loop ||
            Shakers.loop ||
            BassS.loop ||
            BassH.loop ||
            PianoS.loop ||
            PianoH.loop ||
            HornsS.loop ||
            HornsH.loop ||
            MainMelodyS.loop ||
            MainMelodyH.loop ||
            CounterMelodyS.loop ||
            CounterMelodyH.loop ||
            GlockS.loop ||
            GlockH.loop;
        }
        set
        {
            Drums.loop =
            Shakers.loop =
            BassS.loop =
            BassH.loop =
            PianoS.loop =
            PianoH.loop =
            HornsS.loop =
            HornsH.loop =
            MainMelodyS.loop =
            MainMelodyH.loop =
            CounterMelodyS.loop =
            CounterMelodyH.loop =
            GlockS.loop =
            GlockH.loop = value;
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
            BassS.time =
            BassH.time =
            PianoS.time =
            PianoH.time =
            HornsS.time =
            HornsH.time =
            MainMelodyS.time =
            MainMelodyH.time =
            CounterMelodyS.time =
            CounterMelodyH.time =
            GlockS.time =
            GlockH.time = value;
        }
    }

    void SwapChordTrack(AudioSource chordPlayer1, AudioSource chordPlayer2)
    {
        var volume1 = chordPlayer1.volume;
        var volume2 = chordPlayer2.volume;

        AudioManager.Inst.StartCoroutine(FadeTo(volume1 > 0 ? 0 : volume2, chordPlayer1));
        AudioManager.Inst.StartCoroutine(FadeTo(volume2 > 0 ? 0 : volume1, chordPlayer2));
    }

    bool VolumeIsFading = false;

    IEnumerator FadeTo(float amplitude, AudioSource sourceToFade)
    {
        var startVolume = sourceToFade.volume;
        float lerpAmt = 0f;
        VolumeIsFading = true;
        while (!MeasureReached)
        {
            sourceToFade.volume = Mathf.Lerp(startVolume, amplitude, lerpAmt += Time.deltaTime);
            yield return null;
        }
        VolumeIsFading = false;
    }

    void SwapChords()
    {
        float loopTime = SampleTime;

        SwapChordTrack(BassS, BassH);
        SwapChordTrack(PianoS, PianoH);
        SwapChordTrack(HornsS, HornsH);
        SwapChordTrack(MainMelodyS, MainMelodyH);
        SwapChordTrack(CounterMelodyS, CounterMelodyH);
        SwapChordTrack(GlockS, GlockH);

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
        if (VolumeIsFading)
            return;

        Drums.volume = mood > 0.2f ? Mathf.Lerp(0f, .75f, mood * mood) : 0f;
        Shakers.volume = mood > 0 ? Mathf.Lerp(0f, .5f, mood * mood) : 0f;
        if (UsingHappyChords)
        {
            PianoH.volume = Mathf.Lerp(1f, 0.25f, mood);
            GlockH.volume = BassH.volume = Drums.volume;
            HornsH.volume = mood > 0.5f ? 0.75f : Mathf.Lerp(0f, 0.75f, 2 * mood);
            MainMelodyH.volume = Mathf.Lerp(.25f, 1f, mood);
            CounterMelodyH.volume = mood < 0.5f ? Mathf.Lerp(0f, .5f, 2 * mood) : Mathf.Lerp(1f, 0f, mood);
        }
        else
        {
            PianoS.volume = Mathf.Lerp(1f, 0.25f, mood);
            GlockS.volume = BassS.volume = Drums.volume;
            HornsS.volume = mood > 0.5f ? 0.75f : Mathf.Lerp(0f, 0.75f, 2 * mood);
            MainMelodyS.volume = Mathf.Lerp(.25f, 1f, mood);
            CounterMelodyS.volume = mood < 0.5f ? Mathf.Lerp(0f, .5f, 2 * mood) : Mathf.Lerp(1f, 0f, mood);
        }

        if (CurrentMood < 0.75f && mood > 0.75f || CurrentMood > 0.75f && mood < 0.75f)
            Application.Inst.StartCoroutine(SwapChordsAtNextMeasure());

        CurrentMood = mood;    
    }

    public void Play()
    {
        Drums.Play();
        Shakers.Play();
        BassS.Play();
        BassH.Play();
        PianoS.Play();
        PianoH.Play();
        HornsS.Play();
        HornsH.Play();
        MainMelodyS.Play();
        MainMelodyH.Play();
        CounterMelodyS.Play();
        CounterMelodyH.Play();
        GlockS.Play();
        GlockH.Play();
    }

    public void Pause()
    {
        Drums.Pause();
        Shakers.Pause();
        BassS.Pause();
        BassH.Pause();
        PianoS.Pause();
        PianoH.Pause();
        HornsS.Pause();
        HornsH.Pause();
        MainMelodyS.Pause();
        MainMelodyH.Pause();
        CounterMelodyS.Pause();
        CounterMelodyH.Pause();
        GlockS.Pause();
        GlockH.Pause();
    }

}
