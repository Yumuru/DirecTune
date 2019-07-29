using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestMusic : MonoBehaviour {
    [SerializeField]
    AudioSource m_nonMusicSource, m_musicSource;
    MusicPlayer.Clip[] m_clips;
    MusicPlayer m_player;
    void Awake() {
        m_player = new MusicPlayer(m_nonMusicSource, m_musicSource, m_clips);
        GameManager.CurrentPlayer = m_player;
    }
    void Start() {
        var chart = new MusicChart();
        chart.AddGhost(new Timing(3,0,0), 0, 1);
    }
}
