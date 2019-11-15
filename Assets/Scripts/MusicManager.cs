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
		m_source.Stop();

		GameManager.Ins.m_onPlay.Subscribe(_ => {
			m_source.clip = m_clips[0].m_clip;
			m_source.timeSamples = Music.TimeSamples;
			m_source.Play();
		});

        gameScore.m_score.Subscribe(p => {
            var rate = p.m_rate;
            foreach (var clip in m_clips) {
                if (rate > clip.m_scoreRate) {
                    m_source.clip = clip.m_clip;
                    m_source.timeSamples = Music.TimeSamples;
                    return;
                }
            }
        });

		GameManager.Ins.onReset.Subscribe(_ => {
			m_source.Stop();
		});
    }

    [Serializable]
    public struct AudioAndScore {
        public AudioClip m_clip;
        public float m_scoreRate;
    }
}
