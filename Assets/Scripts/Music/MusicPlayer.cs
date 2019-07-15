using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MusicPlayer {
    GameObject m_gameObject;
    public GameObject GameObject { get { return m_gameObject; } }
    public AudioSource m_source;
    public TimingSequencer m_sequencer;

    public MusicPlayer(AudioSource m_source) {
        this.m_source = m_source;
        m_gameObject = m_source.gameObject;
        m_sequencer = new TimingSequencer(new TimingSequencePart(new Timing(0)));
        m_gameObject.UpdateAsObservable()
            .Select(_ => Music.Just)
            .Subscribe(m_sequencer.m_update);
    }

    public void Play() {
        Music.Play(m_gameObject.name);
        m_sequencer.Play();
    }
    public void Complete() {
        m_sequencer.Complete();
    }
}
