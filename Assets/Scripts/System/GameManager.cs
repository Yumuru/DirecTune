using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public static Subject<GhostNoteParameter> EmergeGhost { get { return Instance.m_emergeGhost; } }
    public static Subject<Unit> GhostStep { get; } = new Subject<Unit>();
    public static MusicPlayer CurrentPlayer { get; set; }
    public static GamePlayerMain GamePlayer { get { return Instance.m_gamePlayer; } }
    public static GameScore GameScore { get { return Instance.m_gameScore; } }

    Subject<GhostNoteParameter> m_emergeGhost = new Subject<GhostNoteParameter>();
    public GameScore m_gameScore;
    public GamePlayerMain m_gamePlayer;
    void Awake() {
        Instance = this;
    }

    private void Start() {
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.R))
            .Subscribe(_ =>
                SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }
}
