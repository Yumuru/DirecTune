using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MusicManager : MonoBehaviour {
    public AudioSource m_source;
    public AudioAndScore[] m_clips;

    void Awake() {
        GetComponentInParent<GameManager>().m_musicManager = this;
    }

    void Start() {
        var gameScore = GameManager.Ins.m_gameScore;

        gameScore.m_rateScore.Subscribe(rate => {
            foreach (var clip in m_clips) {
                if (rate > clip.m_scoreRate) {
                    m_source.clip = clip.m_clip;
                    m_source.timeSamples = Music.TimeSamples;
                    return;
                }
            }
        });
    }

    [Serializable]
    public struct AudioAndScore {
        public AudioClip m_clip;
        public float m_scoreRate;
    }
}
