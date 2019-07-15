using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNoteParameter {
    public int m_lane;
    public int m_position;
    public int m_strength;
    public GhostNoteParameter(int lane, int position, int strength) {
        SetParameter(lane, position, strength);
    }
    public void SetParameter(int lane, int position, int strength) {
        this.m_lane = lane;
        this.m_position = position;
        this.m_strength = strength;
    }
}
