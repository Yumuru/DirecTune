using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNoteParameter {
    public Timing m_timing;
    public int m_lane;
    public int m_box;
    public int m_strength;
    public GhostNoteParameter(Timing timing, int lane, int strength) {
        SetParameter(timing, lane, strength);
    }
    public GhostNoteParameter(Timing timing,int lane,int strength,int box) {
        SetParameter(timing, lane, strength, box);
    }
    public void SetParameter(Timing timing, int lane, int strength) {
        this.m_timing = timing;
        this.m_lane = lane;
        this.m_strength = strength;
    }
    public void SetParameter(Timing timing,int lane,int strength,int box) {
        this.m_timing = timing;
        this.m_lane = lane;
        this.m_strength = strength;
        this.m_box = box;
    }
}
