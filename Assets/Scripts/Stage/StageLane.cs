using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLane : MonoBehaviour
{
    [Tooltip("This is stage parts")]
    [SerializeField]
    private GameObject m_blocks,m_center,m_right,m_left,m_makestageParts;
    [SerializeField]
    float m_startRadius, m_lengthStep;
    [Tooltip("Stage parts num")]
    [SerializeField]
    int m_stageNum;
    public LaneParameter[] m_stage = new LaneParameter[3];

    #region Add after

    Vector3 position, direction;
    List<EnemyGhost> ghosts= new List<EnemyGhost>();

    #endregion

    #region MonobehaviorCallbacks


    private void Start() {
        var dires = new GameObject[] { m_left, m_center, m_right }
            .Select(o => o.transform.position - transform.position)
            .Select(v => v.normalized)
            .ToArray();
        for (int i = 0; i < 3; i++) {
            m_stage[i] = new LaneParameter();
            SetLane(i, dires[i]);
        }
    }
    private void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.M)){
            print(GhostStageManager.GetInstance.m_stageStep);
            GhostStageManager.GetInstance.PlusStageStep();
            GhostStageManager.GetInstance.MakeStage(GhostStageManager.GetInstance.m_stageStep);
        }
        */
    }

    #endregion

    #region Method Call

    public void SetLane(int num, Vector3 direction) {
        m_stage[num].m_direction = direction;
        m_stage[num].m_block = new GameObject[TimingManager.LaneLength];
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        for (int i = 0; i < TimingManager.LaneLength; i++) {
            var pos = direction * (m_startRadius + 
                m_lengthStep * i);
            m_stage[num].m_block[i] = Instantiate(m_blocks, pos, rotation);
        }
    }

    #endregion

}
