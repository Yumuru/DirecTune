using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameScore : MonoBehaviour {
    public ReactiveProperty<int> m_score = new ReactiveProperty<int>();

    void Start() {
    }
}
