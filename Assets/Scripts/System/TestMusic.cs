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
            .AddGhost(new Timing(3, 0, 0), 1, 1)
            .AddGhost(new Timing(3, 2, 0), 2, 1)
            .AddGhost(new Timing(4, 0, 0), 1, 1)
            .AddGhost(new Timing(4, 2, 0), 0, 1)
            .AddGhost(new Timing(5, 0, 0), 2, 1)
            .AddGhost(new Timing(5, 2, 0), 0, 1)
            .AddGhost(new Timing(6, 0, 0), 1, 1)
            .AddGhost(new Timing(6, 2, 0), 2, 1)
            .AddGhost(new Timing(7, 0, 0), 1, 1)
            .AddGhost(new Timing(7, 1, 0), 2, 1)
            .AddGhost(new Timing(7, 2, 0), 1, 1)
            .AddGhost(new Timing(7, 3, 0), 0, 1)
            .AddGhost(new Timing(8, 0, 0), 2, 1)
            .AddGhost(new Timing(8, 2, 0), 1, 1)
            .AddGhost(new Timing(9, 0, 0), 1, 1)
            .AddGhost(new Timing(9, 2, 0), 0, 1)
            .AddGhost(new Timing(10, 0, 0), 1, 1)
            .AddGhost(new Timing(10, 2, 0), 0, 1)
            .AddGhost(new Timing(11, 0, 0), 1, 1)
            .AddGhost(new Timing(11, 2, 0), 2, 1)
            .AddGhost(new Timing(11, 3, 0), 1, 1)
            .AddGhost(new Timing(12, 0, 0), 2, 1)
            .AddGhost(new Timing(12, 2, 0), 1, 1)
            .AddGhost(new Timing(13, 0, 0), 0, 1)
            .AddGhost(new Timing(13, 2, 0), 1, 1)
            .AddGhost(new Timing(14, 0, 0), 0, 1)
            .AddGhost(new Timing(14, 2, 0), 1, 1)
            .AddGhost(new Timing(15, 0, 0), 0, 1)
            .AddGhost(new Timing(15, 2, 0), 1, 1)
            .AddGhost(new Timing(15, 3, 0), 2, 1)
            .AddGhost(new Timing(16, 0, 0), 1, 1)
            .AddGhost(new Timing(16, 2, 0), 0, 1)
            .AddGhost(new Timing(17, 0, 0), 1, 1)
            .AddGhost(new Timing(17, 2, 0), 2, 1)
            .AddGhost(new Timing(18, 0, 0), 1, 1)
            .AddGhost(new Timing(18, 2, 0), 0, 1)
            .AddGhost(new Timing(19, 0, 0), 2, 1)
            .AddGhost(new Timing(19, 2, 0), 1, 1)
            .AddGhost(new Timing(19, 3, 0), 2, 1)

            .AddGhost(new Timing(20, 0, 0), 0, 1)
            .AddGhost(new Timing(20, 2, 0), 2, 1)
            .AddGhost(new Timing(21, 0, 0), 1, 1)
            .AddGhost(new Timing(21, 2, 0), 0, 1)
            .AddGhost(new Timing(21, 3, 0), 1, 1)
            .AddGhost(new Timing(22, 0, 0), 2, 1)
            .AddGhost(new Timing(22, 2, 0), 0, 1)
            .AddGhost(new Timing(23, 0, 0), 1, 1)
            .AddGhost(new Timing(23, 2, 0), 2, 1)
            .AddGhost(new Timing(23, 3, 0), 1, 1)

            .AddGhost(new Timing(24, 0, 0), 0, 1)
            .AddGhost(new Timing(24, 2, 0), 0, 1)
            .AddGhost(new Timing(24, 3, 0), 2, 1)
            .AddGhost(new Timing(25, 0, 0), 1, 1)
            .AddGhost(new Timing(26, 0, 0), 2, 1)
            .AddGhost(new Timing(26, 2, 0), 2, 1)
            .AddGhost(new Timing(26, 3, 0), 1, 1)
            .AddGhost(new Timing(27, 0, 0), 2, 1)
            .AddGhost(new Timing(28, 0, 0), 0, 1)
            .AddGhost(new Timing(28, 2, 0), 0, 1)
            .AddGhost(new Timing(28, 3, 0), 2, 1)
            .AddGhost(new Timing(29, 0, 0), 1, 1)
            .AddGhost(new Timing(30, 0, 0), 2, 1)
            .AddGhost(new Timing(30, 2, 0), 2, 1)
            .AddGhost(new Timing(30, 3, 0), 1, 1)
            .AddGhost(new Timing(31, 0, 0), 2, 1)

            .AddGhost(new Timing(32, 0, 0), 1, 1)
            .AddGhost(new Timing(32, 1, 0), 0, 1)
            .AddGhost(new Timing(32, 2, 0), 1, 1)
            .AddGhost(new Timing(32, 3, 0), 0, 1)
            .AddGhost(new Timing(33, 0, 0), 1, 1)
            .AddGhost(new Timing(33, 1, 0), 2, 1)
            .AddGhost(new Timing(33, 2, 0), 1, 1)
            .AddGhost(new Timing(33, 3, 0), 2, 1)
            .AddGhost(new Timing(34, 0, 0), 1, 1)
            .AddGhost(new Timing(34, 1, 0), 0, 1)
            .AddGhost(new Timing(34, 2, 0), 1, 1)
            .AddGhost(new Timing(34, 3, 0), 0, 1)
            .AddGhost(new Timing(35, 0, 0), 1, 1)
            .AddGhost(new Timing(35, 1, 0), 2, 1)
            .AddGhost(new Timing(35, 2, 0), 1, 1)
            .AddGhost(new Timing(35, 3, 0), 2, 1)
            .AddGhost(new Timing(34, 0, 0), 1, 1)
            .AddGhost(new Timing(34, 1, 0), 0, 1)
            .AddGhost(new Timing(34, 2, 0), 1, 1)
            .AddGhost(new Timing(34, 3, 0), 0, 1)
            .AddGhost(new Timing(35, 0, 0), 1, 1)
            .AddGhost(new Timing(35, 1, 0), 2, 1)
            .AddGhost(new Timing(35, 2, 0), 1, 1)
            .AddGhost(new Timing(35, 3, 0), 2, 1)
            .AddGhost(new Timing(36, 0, 0), 1, 1)
            .AddGhost(new Timing(36, 1, 0), 0, 1)
            .AddGhost(new Timing(36, 2, 0), 1, 1)
            .AddGhost(new Timing(36, 3, 0), 0, 1)
            .AddGhost(new Timing(37, 0, 0), 1, 1)
            .AddGhost(new Timing(37, 1, 0), 2, 1)
            .AddGhost(new Timing(37, 2, 0), 1, 1)
            .AddGhost(new Timing(37, 3, 0), 2, 1)
            .AddGhost(new Timing(38, 0, 0), 0, 1)
            .AddGhost(new Timing(38, 2, 0), 1, 1)
            .AddGhost(new Timing(39, 0, 0), 2, 1)
            .AddGhost(new Timing(39, 1, 0), 2, 1)
            .AddGhost(new Timing(39, 2, 0), 2, 1)
            .AddGhost(new Timing(39, 3, 0), 1, 1)

            .AddGhost(new Timing(40, 0, 0), 2, 1)
            .AddGhost(new Timing(40, 2, 0), 2, 1)
            .AddGhost(new Timing(40, 3, 0), 1, 1)
            .AddGhost(new Timing(41, 0, 0), 0, 1)
            .AddGhost(new Timing(41, 2, 0), 0, 1)
            .AddGhost(new Timing(41, 3, 0), 0, 1)
            .AddGhost(new Timing(42, 0, 0), 2, 1)
            .AddGhost(new Timing(42, 2, 0), 2, 1)
            .AddGhost(new Timing(42, 3, 0), 1, 1)
            .AddGhost(new Timing(43, 0, 0), 0, 1)
            .AddGhost(new Timing(43, 2, 0), 0, 1)
            .AddGhost(new Timing(43, 3, 0), 0, 1)
            .AddGhost(new Timing(44, 0, 0), 2, 1)
            .AddGhost(new Timing(44, 2, 0), 2, 1)
            .AddGhost(new Timing(44, 3, 0), 1, 1)
            .AddGhost(new Timing(45, 0, 0), 0, 1)
            .AddGhost(new Timing(45, 2, 0), 0, 1)
            .AddGhost(new Timing(45, 3, 0), 0, 1)
            .AddGhost(new Timing(46, 0, 0), 0, 1)
            .AddGhost(new Timing(46, 2, 0), 1, 1)
            .AddGhost(new Timing(47, 0, 0), 2, 1)
            .AddGhost(new Timing(48, 0, 0), 1, 1);
        m_player.m_sequencer.ReadChart(chart);
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.P))
            .Subscribe(_ => m_player.Play());
    }
}
