using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Ghost : MonoBehaviour
{
    GhostNoteParameter m_noteParameter;
    Vector3 m_myposition;
    float m_time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        m_myposition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            m_myposition += transform.rotation * new Vector3(0, 0, 1);
            this.transform.position = m_myposition;
        }
    }
}
