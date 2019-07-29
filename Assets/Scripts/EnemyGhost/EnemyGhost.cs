using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class EnemyGhost : MonoBehaviour {
    public int m_position;
    public GhostNoteParameter m_parameter;
    //public int m_position = 0;//lane position->GhostNoteParameter.m_box
    public void Step() {
        var lane = GhostStageManager.GetInstance.m_makeStage.m_stage[m_parameter.m_lane];
        transform.position = lane.m_block[++m_position].transform.position;
        // this.m_parameter.m_ghostprefab.transform.position=
        //if (m_position== TimingManager.StepNum) {
        //    Debug.Log(Music.Just);//Music.Justはその時のタイミングとかがでつやつ
        //}
    }
    public void Initialize(GhostNoteParameter parameter) {
        m_parameter = parameter;
        var lane = GhostStageManager.GetInstance.m_makeStage.m_stage[parameter.m_lane];
        transform.position = lane.m_block[0].transform.position;
    }
}