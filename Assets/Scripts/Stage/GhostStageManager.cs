using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GhostStageManager:MonoBehaviour {
    private static GhostStageManager instance;
    public int m_stageStep=-1;
    public StageLane m_makeStage;
    private void Awake() {
        instance=this;
    }
    public static GhostStageManager GetInstance {
        get{
                return instance;
        }
    }
    public void PlusStageStep() {
        m_stageStep+=1;
    }
    public void MakeStage(int num) {
        //m_makeStage.SetLane(num);
    }
}
