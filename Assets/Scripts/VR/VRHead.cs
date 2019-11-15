using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;

public class VRHead : MonoBehaviour {
    public VRPlayer player { get; private set; }

    private void Awake() {
        player = transform.GetComponentInParent<VRPlayer>();
		player.head = UniTask.Run(() => this);
    }
}
