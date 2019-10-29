using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLane : MonoBehaviour
{
    [Tooltip("This is stage parts")]
    [SerializeField]
    private GameObject m_blocks,m_center,m_right,m_left;
    [SerializeField]
    float m_startRadius, m_lengthStep;
    [Tooltip("Stage parts num")]
    [SerializeField]
    public LaneParameter[] m_lanes = new LaneParameter[3];

    #region Add after

    #endregion

    #region MonobehaviorCallbacks


    private void Start() {
        var dires = new GameObject[] { m_left, m_center, m_right }
            .Select(o => o.transform.position - transform.position)
            .Select(v => v.normalized)
            .ToArray();
        for (int i = 0; i < 3; i++) {
            m_lanes[i] = new LaneParameter();
            SetLane(i, dires[i]);
        }
    }
    private void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.M)){
            print(GhostStageManager.GetInstance.m_lanesStep);
            GhostStageManager.GetInstance.PlusStageStep();
            GhostStageManager.GetInstance.MakeStage(GhostStageManager.GetInstance.m_lanesStep);
        }
        */
    }

    #endregion

    #region Method Call

    public void SetLane(int num, Vector3 direction) {
        m_lanes[num].m_direction = direction;
        m_lanes[num].m_block = new GameObject[TimingManager.LaneLength];
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        for (int i = 0; i < TimingManager.LaneLength; i++) {
            var pos = transform.position + direction * (m_startRadius + 
                m_lengthStep * i);
            m_lanes[num].m_block[i] = Instantiate(m_blocks, pos, rotation);
        }
    }

    #endregion

    public struct DireLane {
        public LaneParameter lane;
        public float dot;
    }

    public DireLane GetNearestDireLane(Vector3 dire, Vector3 baseP) {
        DireLane nearestLane;
        nearestLane.lane = null;
        nearestLane.dot = -1f;
        foreach (var lane in m_lanes) {
            var dot = Vector3.Dot(
                dire,
                lane.m_block[0].transform.position - baseP);
            if (nearestLane.dot > dot) {
                nearestLane.lane = lane;
                nearestLane.dot = dot;
            }
        }
        return nearestLane;
    }
}
