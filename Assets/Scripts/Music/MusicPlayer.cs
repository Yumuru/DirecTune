using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MusicPlayer  {
    public AudioSource m_nonMusicSource;
    public AudioSource m_musicSource;
    public TimingSequencer m_sequencer = new TimingSequencer(new TimingSequencePart(new Timing(0)));
    public Clip[] m_clips;
    AsyncSubject<Unit> m_onStop = new AsyncSubject<Unit>();

    public MusicPlayer (AudioSource nonMusicSource, AudioSource musicSource, Clip[] clips) {
        m_nonMusicSource.UpdateAsObservable()
            .Select(_ => Music.Just)
            .Subscribe(m_sequencer.m_update);
        m_clips = clips;
    }

    public void Play() {
        Music.Play(m_nonMusicSource.name);
        m_sequencer.Play();
        PlayClips();
    }

    public void Complete() {
        m_sequencer.Complete();
        m_onStop.OnNext(Unit.Default);
    }

    void PlayClips() {
        var current = 0;
        var disposable =
        GameManager.GameScore.m_score
            .Where(score => m_clips[current].m_score >= score)
            .Subscribe(_ => {
                m_musicSource.clip = m_clips[current].m_clip;
                m_musicSource.timeSamples = Music.TimeSamples;
                m_musicSource.Play();
                current++;
            });
        m_onStop.Subscribe(_ => {
            disposable.Dispose();
            m_musicSource.Stop();
        });
    }

    [Serializable]
    public struct Clip {
        public float m_score;
        public AudioClip m_clip;
    }
}
