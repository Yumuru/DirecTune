using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public static Subject<GhostNoteParameter> EmergeGhost { get { return Instance.m_emergeGhost; } }
    public static Subject<Unit> GhostStep { get; } = new Subject<Unit>();
    public static MusicPlayer CurrentPlayer { get; set; }
    public static GameScore GameScore { get; } = new GameScore();

    Subject<GhostNoteParameter> m_emergeGhost = new Subject<GhostNoteParameter>();
    public GameScore m_gameScore;
    void Awake() {
        Instance = this;
    }
}
