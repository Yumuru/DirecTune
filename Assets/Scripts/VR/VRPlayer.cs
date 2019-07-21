using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class VRPlayer : MonoBehaviour {
    public SteamVR_ControllerManager controllerManager { get; private set; }

    public VRHead head { get; set; }
    public AsyncSubject<VRStick> lStick = new AsyncSubject<VRStick>();
    public AsyncSubject<VRStick> rStick = new AsyncSubject<VRStick>();
    public ReplaySubject<VRStick> sticks = new ReplaySubject<VRStick>();

    public Rigidbody rigid;
    public CapsuleCollider collider;

    protected virtual void Awake() {
        controllerManager = GetComponent<SteamVR_ControllerManager>();

        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

        Observable.WhenAll(lStick, rStick)
            .Subscribe(_ => sticks.OnCompleted());

        sticks.Subscribe(stick => {
            (stick.isLeft ? lStick : rStick)
                .OnNext(stick);
        });
    }

    private void Start() {
        this.UpdateAsObservable()
            .Where(_ => collider != null)
            .Subscribe(_ => {
                var pos = head.transform.position;
                pos.y = transform.position.y;
                pos.y += collider.height / 2f;
                pos = transform.InverseTransformPoint(pos);
                collider.center = pos;
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
