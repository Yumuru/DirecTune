﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;

public class VRStick : MonoBehaviour {
    SteamVR_TrackedObject trackedObject;

    public SteamVR_Controller.Device device { get; private set; }

    public VRPlayer player { get; private set; }
    public UniTask<VRHead> head { get; private set; }
    public AsyncSubject<VRStick> otherSide = new AsyncSubject<VRStick>();
    public VRStickButtons buttons;

    public bool isLeft { get; private set; }

    void Awake() {
        player = transform.parent.GetComponent<VRPlayer>();
        head = new UniTask<VRHead>(async () => await player.head);
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        buttons = new VRStickButtons(this);
    }
    void Start() {
        isLeft = gameObject == player.controllerManager.left;
        player.sticks.Where(stick => isLeft != stick.isLeft)
            .Subscribe(stick => stick.otherSide.OnNext(this));
        device = SteamVR_Controller.Input((int)trackedObject.index);
        this.UpdateAsObservable()
            .Subscribe(_ => 
                device = SteamVR_Controller.Input((int)trackedObject.index));
        player.sticks.OnNext(this);
    }

    public Vector2 GetAxis() => device.GetAxis();
}
