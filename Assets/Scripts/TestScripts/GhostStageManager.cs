using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GhostStageManager {
    private static GhostStageManager instance;
    public int m_stageStep=-1;
    public MakeStage m_makeStage = new MakeStage();
    GhostNoteParameter m_ghostnoteParameter;
    public static GhostStageManager GetInstance {
        get{
            if (instance == null) {
                instance = new GhostStageManager();
            }
                return instance;
            
        }
    }
    public void PlusStageStep() {
        m_stageStep+=1;
    }
    public void MakeStage(int num) {
        m_makeStage.SetLane(num);
    }
}
