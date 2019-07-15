using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeStage : MonoBehaviour
{
    [Tooltip("This is stage parts")]
    [SerializeField]
    private GameObject m_stageBox,m_certain,m_right,m_left,m_playerPos;
    [Tooltip("Stage parts num")]
    [SerializeField]
    int m_stageNum;
    StageParameters[] m_stage = new StageParameters[3];
    private void Start() {
        for (int i = 0; i < 3; i++) {
            m_stage[i] = new StageParameters();
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            SetLane(0);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            SetLane(1);
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            SetLane(2);
        }
    }
    void SetLane(int num) {
        Vector3 certain;
        if (num == 0) {
            //print(m_stage[num].m_block=new GameObject[8]);
            m_stage[num].m_block = new GameObject[m_stageNum];
            m_stage[num].m_laneNo = num;
            certain = m_certain.transform.position;
            for (int i = 0; i < m_stageNum; i++) {
                m_stage[num].m_block[i] = Instantiate(m_stageBox, certain, Quaternion.identity);
                certain.z += 1;
            }
        }
        if (num == 1) {
            m_stage[num].m_block = new GameObject[m_stageNum];
            m_stage[num].m_laneNo = num;
            certain = m_right.transform.position;
            for (int i = 0; i < m_stageNum; i++) {
                m_stage[num].m_block[i] = Instantiate(m_stageBox, certain, Quaternion.identity);
                m_stage[num].m_block[i].transform.rotation = Quaternion.LookRotation(m_playerPos.transform.position - m_stage[num].m_block[i].transform.position, Vector3.up);
                certain += m_stage[num].m_block[i].transform.forward;
                certain.z += 1;
                print(i);
            }
        }
        if (num == 2) {
            m_stage[num].m_block = new GameObject[m_stageNum];
            m_stage[num].m_laneNo = num;
            certain = m_left.transform.position;
            for (int i = 0; i < m_stageNum; i++) {
                m_stage[num].m_block[i] = Instantiate(m_stageBox, certain, Quaternion.identity);
                m_stage[num].m_block[i].transform.rotation = Quaternion.LookRotation(m_playerPos.transform.position - m_stage[num].m_block[i].transform.position, Vector3.up);
                certain += m_stage[num].m_block[i].transform.forward;
                certain.z += 1;
            }
        }
    }

}
