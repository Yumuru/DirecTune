using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public static Subject<GhostNoteParameter> EmergeGhost { get { return Instance.m_emergeGhost; } }
    public static Subject<Unit> m_ghostStep { get; } = new Subject<Unit>();

    Subject<GhostNoteParameter> m_emergeGhost = new Subject<GhostNoteParameter>();
    void Awake() => Instance = this;
}
