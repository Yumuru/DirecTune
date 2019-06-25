using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestScript : MonoBehaviour {
    public AudioSource m_music;

    void Start() {
        TestMusicPlayer();
    }

    void TestMusicPlayer() {
        var player = new MusicPlayer(m_music);
        var chart = new MusicChart();
        chart
            .AddGhost(new Timing(3, 0, 0), 0, 0)
            .AddGhost(new Timing(3, 1, 0), 1, 0)
            .AddGhost(new Timing(3, 2, 0), 2, 0)
            .AddGhost(new Timing(3, 3, 0), 0, 0)
            .AddGhost(new Timing(4, 0, 0), 2, 0)
            .AddGhost(new Timing(4, 1, 0), 2, 0)
            .AddGhost(new Timing(4, 2, 0), 2, 0)
            .AddGhost(new Timing(4, 3, 0), 2, 0)
            .AddGhost(new Timing(5, 0, 0), 2, 0)
            .AddGhost(new Timing(5, 1, 0), 2, 0)
            .AddGhost(new Timing(5, 2, 0), 2, 0);
        player.ReadChart(chart);
        GameManager.EmergeGhost.Subscribe(enemy => {
        }).AddTo(gameObject);

        GameManager.EmergeGhost.Subscribe(_ => print("Test"));

        
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.P))
            .Subscribe(_ => {
                player.Play();
            });
    }
}
