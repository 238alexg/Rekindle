using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MusicPlayer
{
    static readonly float SecondsPerMeasure = 1f / 3f;
    static float LoopTime = 0f;
    static bool MeasureReached = false;
    static bool UsingHappyChords = false;

    static AudioSource Drums = new AudioSource();
    static AudioSource Shakers = new AudioSource();
    static AudioSource Bass = new AudioSource();
    static AudioSource Piano = new AudioSource();
    static AudioSource Horns = new AudioSource();
    static AudioSource MainMelody = new AudioSource();
    static AudioSource CounterMelody = new AudioSource();
    static AudioSource Glock = new AudioSource();

    static AudioClip DrumLoop;
    static AudioClip ShakerLoop;
    static AudioClip HappyBassLoop;
    static AudioClip SadBassLoop;
    static AudioClip HappyPianoLoop;
    static AudioClip SadPianoLoop;
    static AudioClip HappyHornLoop;
    static AudioClip SadHornLoop;
    static AudioClip HappyMainMelodyLoop;
    static AudioClip SadMainMelodyLoop;
    static AudioClip HappyCounterMelodyLoop;
    static AudioClip SadCounterMelodyLoop;
    static AudioClip HappyGlockLoop;
    static AudioClip SadGlockLoop;

    public static void InitializeClips(AudioClipCache audioClips)
    {
        DrumLoop = audioClips.DrumLoop;
        ShakerLoop = audioClips.ShakerLoop;
        HappyBassLoop = audioClips.HappyBassLoop;
        SadBassLoop = audioClips.SadBassLoop;
        HappyPianoLoop = audioClips.HappyPianoLoop;
        SadPianoLoop = audioClips.SadPianoLoop;
        HappyHornLoop = audioClips.HappyHornLoop;
        SadHornLoop = audioClips.SadHornLoop;
        HappyMainMelodyLoop = audioClips.HappyMainMelodyLoop;
        SadMainMelodyLoop = audioClips.SadMainMelodyLoop;
        HappyCounterMelodyLoop = audioClips.HappyCounterMelodyLoop;
        SadCounterMelodyLoop = audioClips.SadCounterMelodyLoop;
        HappyGlockLoop = audioClips.HappyGlockLoop;
        SadGlockLoop = audioClips.SadGlockLoop;

        loop = true;
        Play();
    }

    static AudioSource[] AudioSources = { Drums, Shakers, Bass, Piano, Horns, MainMelody, CounterMelody, Glock };

    public static void TickTransport(float time)
    {
        if (isPlaying)
        {
            LoopTime += time;

            if (LoopTime > SecondsPerMeasure)
                MeasureReached = true;
            else
                MeasureReached = false;

            LoopTime %= SecondsPerMeasure;
        }
    }

    public static bool isPlaying
    {
        get
        {
            return AudioSources[0].isPlaying;
        }
    }

    public static bool loop
    {
        get
        {
            return AudioSources[0].loop;
        }
        set
        {
            foreach (AudioSource source in AudioSources)
                source.loop = value;
        }
    }

    public static float time
    {
        get
        {
            return AudioSources[0].time;
        }
        set
        {
            foreach (AudioSource source in AudioSources)
                source.time = value;
        }
    }

    static void SwapChords()
    {
        float loopTime = time;
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
        foreach (AudioSource source in AudioSources)
        {
            source.time = loopTime;
            source.Play();
        }
    }

    public static IEnumerator SwapChordsAtNextMeasure()
    {
        while (!MeasureReached)
            yield return null;

        SwapChords();
    }

    public static void UpdateMood(float mood)
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

        loop = true;
    }

    public static void Play()
    {
        foreach (AudioSource source in AudioSources)
            source.Play();
    }

}
