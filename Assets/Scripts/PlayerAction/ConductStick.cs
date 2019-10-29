using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ConductStick : MonoBehaviour {
    GamePlayerMain m_playerMain;
    VRStick m_stick;
    public float m_thresholdSpeed;
    public ParticleSystem[] m_succParticles;
    public bool isDebug;

    void Start() {
        m_playerMain = GetComponentInParent<GamePlayerMain>();
        m_stick = GetComponentInParent<VRStick>();

        SetConductAction();
    }

    // ConductActionを実行するタイミングを設定
    void SetConductAction() {
        var onConduct = new Subject<StageLane.DireLane>();
        this.OnDestroyAsObservable()
            .Subscribe(_ => onConduct.OnCompleted());
        if (isDebug) {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.C))
                .Subscribe(_ => onConduct.OnNext(new StageLane.DireLane() {
                    lane = GhostStageManager.StageLane.m_lanes[0],
                    dot = 0.9f
                }));
        }
        m_stick.UpdateAsObservable()
            .Where(_ => m_stick.device.velocity.sqrMagnitude >= Mathf.Pow(m_thresholdSpeed, 2f))
            .Select(_ => GhostStageManager.StageLane.GetNearestDireLane(
                m_stick.transform.position - m_stick.head.transform.position,
                GhostStageManager.StageLane.transform.position))
            .Subscribe(l => onConduct.OnNext(l));
        onConduct
            .Where(l => l.dot > 0.8f)
            .Select(l => new GhostALane() { ghost = l.lane.GetCanConductGhost(), lane = l.lane })
            .Where(v => v.ghost != null)
            .Where(v => TimingManager.CouldConduct(v.ghost.m_parameter.m_timing))
            .Subscribe(v => ConductGhost(v));
    }

    struct GhostALane {
        public EnemyGhost ghost;
        public LaneParameter lane;
    }

    void ConductGhost(GhostALane ghostALane) {
        var ghost = ghostALane.ghost;
        var lane = ghostALane.lane;
        ghost.Damage();
        GameManager.GameScore.m_numConductedGhost.Value++;
        Instantiate(m_succParticles.RandomGet()
            , lane.m_block[0].transform.position
            , lane.m_block[0].transform.rotation)
            .PlayDestroy();
        if (ghost.m_parameter.m_strength == 2) {
            ghost.goBackAnimPlay(ghost.transform);
        } else {
            Observable.Timer(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ => Destroy(ghost.gameObject));
        }
    }
}
