using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class VRPlayer : MonoBehaviour {
    public SteamVR_ControllerManager controllerManager { get; private set; }

	public VRHead head;
    public AsyncSubject<VRStick> lStick = new AsyncSubject<VRStick>();
    public AsyncSubject<VRStick> rStick = new AsyncSubject<VRStick>();
    public ReplaySubject<VRStick> sticks = new ReplaySubject<VRStick>();

    protected virtual void Awake() {
        controllerManager = GetComponent<SteamVR_ControllerManager>();

        Observable.WhenAll(lStick, rStick)
            .Subscribe(_ => sticks.OnCompleted());

        sticks.Subscribe(stick => {
            (stick.isLeft ? lStick : rStick)
                .OnNext(stick);
        });
    }

    public void SetHeadPosition(Vector3 position) {
        var headToPlayer = transform.position - head.transform.position;
        transform.position = headToPlayer + position;
    }

    public Vector3 GetPosition() {
        var pos = head.transform.position;
        pos.y = transform.position.y;
        return pos;
    }
}
