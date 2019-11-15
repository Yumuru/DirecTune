using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhost : MonoBehaviour {
    public int m_laneId;
    void Start() {
        GameManager.Ins.m_enemyGhostManager.SpawnGhost(m_laneId);
        Destroy(gameObject);
    }
}
