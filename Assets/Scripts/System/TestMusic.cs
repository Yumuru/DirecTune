using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestMusic : MonoBehaviour {
    [SerializeField]
    AudioSource m_sourceNonMusic, m_sourceMusic;
    [SerializeField]
    MusicPlayer.Clip[] m_clips;
    MusicPlayer m_player;
    void Awake() {
        m_player = new MusicPlayer(m_sourceNonMusic, m_sourceMusic, m_clips);
        GameManager.CurrentPlayer = m_player;
    }

    void Start() {
        var chart = new MusicChart();
        chart
            .AddGhost(new Timing(3, 0, 0), 0, 1)
            .AddGhost(new Timing(3, 1, 0), 0, 1)
            .AddGhost(new Timing(3, 2, 0), 0, 1)
            .AddGhost(new Timing(3, 3, 0), 0, 1)
            .AddGhost(new Timing(4, 0, 0), 0, 1)
            .AddGhost(new Timing(4, 1, 0), 0, 1)
            .AddGhost(new Timing(4, 2, 0), 0, 1)
            .AddGhost(new Timing(4, 3, 0), 0, 1);
        m_player.m_sequencer.ReadChart(chart);
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.P))
            .Subscribe(_ => m_player.Play());
    }
}
