using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestScript : MonoBehaviour {
    void Start() {
        var waitor = new Waitor();
        waitor.Invoke(invoke => invoke.Subscribe(_ => {
        })).Append().Invoke(invoke => invoke.Subscribe(_ => {

        }));
    }
}
