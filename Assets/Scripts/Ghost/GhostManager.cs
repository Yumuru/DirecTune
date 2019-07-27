using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GhostManager : MonoBehaviour {
    public static GhostManager Instance { get; set; }

    void Awake() => Instance = this;

    //これが呼ばれたらゴーストが出現する。
    public static Ghost Emerge(GhostNoteParameter parameter) {
        return new Ghost { 
            m_parameter = parameter
        };
    }
}

public class Ghost {
    public GhostNoteParameter m_parameter;
    public int m_position = 0;//lane position
    public Subject<Unit> OnDestroy = new Subject<Unit>();//気にしなくて良い
    //これが呼ばれたらゴーストを前に進ませる
    public void Step() {
        m_position++;
        if (m_position == TimingManager.StepNum) {
            Debug.Log(Music.Just);//Music.Justはその時のタイミングとかがでつやつ
        }
    }
}
