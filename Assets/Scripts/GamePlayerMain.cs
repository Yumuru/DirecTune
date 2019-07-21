using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GamePlayerMain : MonoBehaviour {
    public VRPlayer m_vRPlayer;
    
    public Lanes lanes;
    public Lane m_currentLane;

    void Start() {
        this.UpdateAsObservable()
            .Subscribe(_ => CheckLane());
    }

    void CheckLane() {
        var min = Mathf.Infinity;
        foreach (var lane in lanes.lanes) {
            var toLane = lane.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, toLane);
            if (angle < min) {
                min = angle;
                m_currentLane = lane;
            }
        }
    }
}

public class Lanes {
    public List<Lane> lanes = new List<Lane>();
}

public class Lane {
    public Transform transform;
}
