using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GhostStageManager:MonoBehaviour {
    private static GhostStageManager instance;
    public static GhostStageManager Instance { get { return instance; } }
    public static StageLane StageLane { get { return Instance.m_stageLane; } }
    public int m_stageStep=-1;
    public StageLane m_stageLane;
    private void Awake() {
        instance=this;
    }
}
