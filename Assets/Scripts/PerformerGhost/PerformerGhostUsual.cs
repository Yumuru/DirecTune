using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PerformerGhostUsual : MonoBehaviour {
    public PerformerGhost m_performerGhost;

    void Awake() {
        GetComponentInParent<PerformerGhosts>().m_performerGhosts.Add(m_performerGhost);
    }
}
