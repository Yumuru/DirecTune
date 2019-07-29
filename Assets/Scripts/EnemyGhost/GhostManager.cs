using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GhostManager : MonoBehaviour {
    public static GhostManager Instance { get; set; }
    public static EnemyGhost EnemyGhostPrefab { get { return Instance.m_enemyGhostPrefab; } }
    [SerializeField]
    EnemyGhost m_enemyGhostPrefab;

    void Awake() => Instance = this;

    private void Update() {

    }
    //これが呼ばれたらゴーストが出現する。
    public static EnemyGhost Emerge(GhostNoteParameter parameter) {
        var ghost = Instantiate(EnemyGhostPrefab);
        ghost.Initialize(parameter);
        return ghost;
    }
}


