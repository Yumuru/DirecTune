using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    GameObject[] m_lane;
    [SerializeField]
    GameObject m_setObject;
    GameStatus m_statusnum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            SetObject();
        }
        print(m_statusnum.m_lanenum);
    }
    void SetObject() {
        print(m_statusnum.m_lanenum);
        for (int i = 0; i < m_statusnum.m_lanenum; i++) {
            print(111);
            m_lane[i] = m_setObject.gameObject;
        }
    }
}
