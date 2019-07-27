using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GhostManager : MonoBehaviour {
    public static GhostManager Instance { get; set; }

    void Awake() => Instance = this;

    private void Update() {
        
    }
    //これが呼ばれたらゴーストが出現する。
    public static Ghost Emerge(GhostNoteParameter parameter) {
        return new Ghost { 
            m_parameter = parameter
        };
    }
}


#region Methods
public class Ghost {
    public GhostNoteParameter m_parameter;
    //public int m_position = 0;//lane position->GhostNoteParameter.m_box
    public Subject<Unit> OnDestroy = new Subject<Unit>();//気にしなくて良い
    //これが呼ばれたらゴーストを前に進ませる
    public void Step() {
        m_parameter.m_box += 1; 
        if (m_parameter.m_box == TimingManager.StepNum) {
            Debug.Log(Music.Just);//Music.Justはその時のタイミングとかがでつやつ
        }
    }
}
#endregion
