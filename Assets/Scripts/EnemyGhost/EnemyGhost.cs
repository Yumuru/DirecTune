using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using static Easing;

public class EnemyGhost : MonoBehaviour {
    // Write at this place Parameter
    public int m_position;
    public Renderer m_renderer;
    public Animator m_animator;
    public LaneParameter m_lane;

    public EnemyGhostStep m_stepParam;

    void Awake() {
        m_stepParam = new EnemyGhostStep(this).SetAction(8);
    }

    // Write Other Pace parameter
    public Transform m_direBaseJumpBack;

    // It maybe deleted parameter
    public GhostNoteParameter m_parameter;
    public Action Remove { get; private set; }

    public void Initialize(GhostNoteParameter parameter) {
        m_parameter = parameter;
        var lane = GhostStageManager.StageLane.m_lanes[parameter.m_lane];
        m_position = lane.m_block.Length-1;
        transform.position = lane.m_block[m_position].transform.position;
        transform.rotation = Quaternion.LookRotation(-lane.m_direction, Vector3.up);
        Remove = lane.AddGhost(this);

        Instantiate(GhostManager.EmergeParticle, transform.position, transform.rotation).PlayDestroy();
    }
}