using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGhost : MonoBehaviour
{
    List<GhostNoteParameter> m_ghost = new List<GhostNoteParameter>();
    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            m_ghost.Add(new GhostNoteParameter(0, 1, 0));
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            m_ghost
        }
    }
}
