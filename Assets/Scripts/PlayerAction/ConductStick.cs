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
    public bool isDebug;
    Subject<Unit> m_onDestroy = new Subject<Unit>();

    void Start() {
        m_playerMain = GetComponentInParent<GamePlayerMain>();
        m_stick = GetComponentInParent<VRStick>();

        SetConductAction();
    }

    void SetConductAction() {
        var onConduct = new Subject<Unit>();
        this.OnDestroyAsObservable()
            .Subscribe(_ => onConduct.OnCompleted());
        if (isDebug) {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.C))
                .Subscribe(_ => onConduct.OnNext(Unit.Default));
        }
        m_stick.UpdateAsObservable()
            .Where(_ => m_stick.device.velocity.sqrMagnitude >= Mathf.Pow(m_thresholdSpeed, 2f))
            .Subscribe(_ => onConduct.OnNext(Unit.Default));
        onConduct.Subscribe(_ => {
            var timing = new Timing(Music.Just);
            if (TimingManager.CouldConduct(timing)) {
                print(timing.ToString() + " : conduct!");
            }
        });
    }
}
