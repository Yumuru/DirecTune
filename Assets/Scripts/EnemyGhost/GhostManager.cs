using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GhostManager : MonoBehaviour {
    public static GhostManager Instance { get; set; }
    public static EnemyGhost EnemyGhostPrefab { get { return Instance.m_enemyGhostPrefab; } }
    public static float TimeGhostStep { get { return Instance.m_timeGhostStep; } }
    public static ParticleSystem EmergeParticle { get { return Instance.m_emergeParticle; } }
    [SerializeField]
    EnemyGhost m_enemyGhostPrefab;
    [SerializeField]
    float m_timeGhostStep;
    [SerializeField]
    ParticleSystem m_emergeParticle;

    void Awake() => Instance = this;

    //これが呼ばれたらゴーストが出現する。
    public static EnemyGhost Emerge(GhostNoteParameter parameter) {
        var ghost = Instantiate(EnemyGhostPrefab);
        ghost.Initialize(parameter);
        return ghost;
    }
}


