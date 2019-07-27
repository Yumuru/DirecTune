using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhost : MonoBehaviour
{
    public GameObject m_prefab_ghost, m_target;
    MakeStage m_stage = new MakeStage();
    //GhostNoteParameter m_ghost = new GhostNoteParameter();
    private void Start(){
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            //Instantiate(m_prefab_ghost, , Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            var ghost = Instantiate(m_prefab_ghost, this.transform.position + new Vector3(0, 0, 10), Quaternion.identity);
            //Íghost.transform.rotation = Quaternion.FromToRotation(ghost.gameObject.transform.position, m_target.transform.position);
            ghost.transform.rotation = Quaternion.LookRotation(m_target.transform.position - ghost.gameObject.transform.position, Vector3.up);
        }
    }
}
