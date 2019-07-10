using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNoteParameter {
    public Timing m_timing;
    public int m_lane;
    public int m_strength;
    public GhostNoteParameter(Timing timing, int lane, int strength) {
        this.m_timing = timing; 
        this.m_lane = lane;
        this.m_strength = strength;
   }
}
