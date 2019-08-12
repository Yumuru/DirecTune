using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNoteParameter {
    public Timing m_timing;
    public int m_lane;
    public int m_strength;
    //public GameObject m_ghostprefab;
    public GhostNoteParameter(Timing timing, int lane, int strength) {
        SetParameter(timing, lane, strength);
    }
    public void SetParameter(Timing timing, int lane, int strength) {
        this.m_timing = timing;
        this.m_lane = lane;
        this.m_strength = strength;
    }
}
