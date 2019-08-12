using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GamePlayerMain : MonoBehaviour {
    VRPlayer m_vRPlayer;

    public LaneParameter m_laneCurrent;

    private void Awake() => m_vRPlayer = GetComponent<VRPlayer>();

    void Start() {
        this.UpdateAsObservable()
            .Subscribe(_ => CheckLane());
    }

    void CheckLane() {
        var min = Mathf.Infinity;
        var lanes = GhostStageManager.GetInstance.m_makeStage.m_stage;
        foreach (var lane in lanes) {
            var angle = Vector3.Angle(m_vRPlayer.head.transform.forward, lane.m_direction);
            if (angle < min) {
                min = angle;
                m_laneCurrent = lane;
            }
        }
    }
}
