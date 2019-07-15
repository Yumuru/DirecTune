using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GhostManager : MonoBehaviour {
    public static GhostManager Instance { get; set; }

    void Awake() => Instance = this;

    public static Ghost Emerge(GhostNoteParameter parameter) {
        return new Ghost { 
            m_parameter = parameter
        };
    }
}

public class Ghost {
    public GhostNoteParameter m_parameter;
    public int m_position = 0;
    public Subject<Unit> OnDestroy = new Subject<Unit>();
    public void Step() {
        m_position++;
    }
}
