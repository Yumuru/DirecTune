using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ConductStick : MonoBehaviour {
    GamePlayerMain m_playerMain;
    VRStick m_stick;
    public float m_speedThreshold;
    public bool isDebug;

    void Start() {
        m_playerMain = GetComponentInParent<GamePlayerMain>();
        m_stick = GetComponentInParent<VRStick>();
    }

    void SetConductAction() {
        var onConduct = new Subject<Unit>();
        m_stick.UpdateAsObservable()
            .Where(_ => m_stick.device.velocity.sqrMagnitude >= Mathf.Pow(m_speedThreshold, 2f))
            .Subscribe(onConduct);
        if (isDebug) {
            this.UpdateAsObservable()
                .Where(_ => isDebug)
                .Where(_ => Input.GetKeyDown(KeyCode.C))
                .Subscribe(onConduct);
        }
        onConduct.Subscribe(_ => {
                var timing = new Timing(Music.Just);
                if (TimingManager.CouldConduct(timing)) {
                    print("conduct!");
                }
            });
    }
}
