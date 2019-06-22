using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestScript : MonoBehaviour {
    void Start() {
        var checkAndDo = new CheckAndDo();
        for (int i = 0; i < 10; i++) {
            var num = i;
            checkAndDo.Append(() => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ => {
                    Debug.Log(num);
                });
        }
        this.UpdateAsObservable()
            .Subscribe(checkAndDo.GeneratePlayer(0).invoke);
    }
}
