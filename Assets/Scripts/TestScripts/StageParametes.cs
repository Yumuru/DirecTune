﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneParameter {
    public Vector3 m_direction;
    public int m_blockNum;
    public GameObject[] m_block;
    public List<EnemyGhost> m_ghosts = new List<EnemyGhost>();

    public EnemyGhost GetCanConductGhost() {
        foreach (var ghost in m_ghosts) {
            if (ghost.m_canConduct) return ghost;
        }
        return null;
    }
}