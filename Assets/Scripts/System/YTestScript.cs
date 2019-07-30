using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class YTestScript : MonoBehaviour {
    public AudioSource m_music;
    public MusicPlayer m_player;

    public ParticleSystem m_particle;

    void Start() {
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.T))
            .Subscribe(_ => {
                Instantiate(m_particle, transform.position, transform.rotation)
                    .PlayDestroy();
            });
    }

    void EmergeAndStepGhost() {
        GameManager.EmergeGhost.Subscribe(ghostNoteParameter => {
            var ghost = GhostManager.Emerge(ghostNoteParameter);
            ghost.UpdateAsObservable()
                .Where(_ => Music.IsJustChanged).Take(1)
                .SelectMany(TimingManager.OnStep)
                .TakeUntil(ghost.OnDestroyAsObservable())
                .Subscribe(ghost.m_onStep);
        }).AddTo(gameObject);
    }

    void TestMusicPlayer() {
        var chart = new MusicChart();
        chart
            .AddGhost(new Timing(3, 0, 0), 0, 0)
            .AddGhost(new Timing(3, 1, 0), 0, 0)
            .AddGhost(new Timing(3, 2, 0), 2, 0)
            .AddGhost(new Timing(3, 3, 0), 2, 0)
            .AddGhost(new Timing(4, 0, 0), 0, 0)
            .AddGhost(new Timing(4, 1, 0), 0, 0)
            .AddGhost(new Timing(4, 2, 0), 2, 0)
            .AddGhost(new Timing(4, 3, 0), 2, 0)
            .AddGhost(new Timing(5, 0, 0), 0, 0)
            .AddGhost(new Timing(5, 1, 0), 0, 0)
            .AddGhost(new Timing(5, 2, 0), 0, 0)
            .AddGhost(new Timing(5, 3, 0), 0, 0)
            .AddGhost(new Timing(6, 0, 0), 0, 0)
            .AddGhost(new Timing(6, 1, 0), 0, 0)
            .AddGhost(new Timing(6, 2, 0), 0, 0)
            .AddGhost(new Timing(6, 3, 0), 0, 0)
            .AddGhost(new Timing(7, 0, 0), 2, 0)
            .AddGhost(new Timing(7, 1, 0), 2, 0)
            .AddGhost(new Timing(7, 2, 0), 2, 0)
            .AddGhost(new Timing(7, 3, 0), 2, 0)
            .AddGhost(new Timing(8, 3, 0), 0, 0)
            .AddGhost(new Timing(8, 3, 0), 0, 0)
            .AddGhost(new Timing(8, 3, 0), 0, 0)
            .AddGhost(new Timing(8, 3, 0), 0, 0);
        m_player.m_sequencer.ReadChart(chart);
        
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.P))
            .Subscribe(_ => {
                m_player.Play();
            });
    }
}
