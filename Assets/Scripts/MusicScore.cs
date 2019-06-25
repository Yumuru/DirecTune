using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using static MusicChart;

public class MusicChart {
    public List<GhostNoteParameter> m_ghostNotes = new List<GhostNoteParameter>();
    public MusicChart() {
    }

    public MusicChart AddGhost(Timing timing, int lane, int strength) {
        return AddGhost(new GhostNoteParameter(timing, lane, strength));
    }
    public MusicChart AddGhost(GhostNoteParameter ghostParameter) {
        m_ghostNotes.Add(ghostParameter);
        return this;
    }
}

public class GhostNoteParameter {
    public Timing m_timing;
    public int m_lane;
    public int m_position;
    public int m_strength;
    public GhostNoteParameter(Timing timing, int lane, int strength) {
        this.m_timing = timing; 
        this.m_lane = lane;
        this.m_strength = strength;
   }
}

public enum GhostPattern {
    Normal,
    Strong
}

public static class MusicChartFunc {
    public static MusicPlayer ReadChart(this MusicPlayer player, MusicChart chart) {
        var seq = player.m_sequencer;
        foreach (var ghostNote in chart.m_ghostNotes) {
            var popTiming = new Timing(ghostNote.m_timing);
            popTiming.Subtract(TimingManager.LaneTimingLength, Music.CurrentSection);
            seq.Add(popTiming).SetAction(tim => {
                ghostNote.m_position = TimingManager.LaneLength;
                GameManager.EmergeGhost.OnNext(ghostNote);
            });
        }
        return player;
    }
}
