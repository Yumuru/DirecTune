using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MusicPlayer  {
    public AudioSource m_sourceNonMusic;
    public AudioSource m_sourceMusic;
    public TimingSequencer m_sequencer = new TimingSequencer(new TimingSequencePart(new Timing(0)));
    public Clip[] m_clips;
    AsyncSubject<Unit> m_onStop = new AsyncSubject<Unit>();

    public MusicPlayer (AudioSource sourceNonMusic, AudioSource sourceMusic, Clip[] clips) {
        m_sourceNonMusic = sourceNonMusic;
        m_sourceMusic = sourceMusic;
        m_sourceNonMusic.UpdateAsObservable()
            .Select(_ => Music.Just)
            .Subscribe(m_sequencer.m_update);
        m_clips = clips;
    }

    public void Play() {
        Music.Play(m_sourceNonMusic.name);
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
        GameManager.GameScore.m_rateScore
            .TakeWhile(_ => current < m_clips.Length)
            .Where(s => s >= m_clips[current].m_rateScore)
            .Subscribe(_ => {
                m_sourceMusic.clip = m_clips[current].m_clip;
                m_sourceMusic.timeSamples = Music.TimeSamples;
                m_sourceMusic.Play();
                current++;
            });
        
        Observable.EveryUpdate()
            .Where(_ => Music.IsJustChangedAt(new Timing(39)))
            .Subscribe(_ => {
            disposable.Dispose();
            m_sourceMusic.Stop();
            m_sourceNonMusic.Stop();
            Music.Stop();
        });
    }

    [Serializable]
    public struct Clip {
        public float m_rateScore;
        public AudioClip m_clip;
    }
}
