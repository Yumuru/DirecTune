using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[Serializable]
public class PerformerGhost {
    public Animator m_animator;
    [NonSerialized]
    public GameObject m_gameObject;

    Action<bool> SetFun;
    Action<bool> SetPlay;

    public void Initialize(GameObject gameObject) {
        m_gameObject = gameObject;
        SetFun = b => m_animator.SetBool("Fun", b);
        SetPlay = b => m_animator.SetBool("Play", b);
    }

    public void Fun() {
        SetFun(true);
        SetPlay(false);
    }

    public void Play() {
        SetFun(false);
        SetPlay(true);
    }
}
