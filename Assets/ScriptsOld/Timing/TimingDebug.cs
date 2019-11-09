using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class TimingDebug : MonoBehaviour {
    public Text text;
    void Start() {
        this.UpdateAsObservable()
            .Subscribe(_ => {
                text.text = Music.Just.ToString();
            });
    }
}
