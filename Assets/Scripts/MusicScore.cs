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

public enum GhostPattern {
    Normal,
    Strong
}

public static class MusicChartFunc {
    public static TimingSequencer ReadChart(this TimingSequencer seq, MusicChart chart) {
        foreach (var ghostNote in chart.m_ghostNotes) {
            var popTiming = new Timing(ghostNote.m_timing);
            popTiming.Subtract(TimingManager.LaneTimingLength, Music.CurrentSection);
            seq.Add(popTiming).SetAction(tim => {
                GameManager.EmergeGhost.OnNext(ghostNote);
            });
        }
        return seq;
    }
}
