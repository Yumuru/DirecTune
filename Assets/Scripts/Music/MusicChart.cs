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
    public static void ReadChart(this TimingSequencer sequencer, MusicChart chart) {
        var laneTimingLength = new Timing(TimingManager.LaneTimingLength);
        var ghostNum = 0;
        foreach (var ghostNote in chart.m_ghostNotes) {
            ghostNum++;
            var popTiming = new Timing(ghostNote.m_timing);
            popTiming.Subtract(laneTimingLength, Music.CurrentSection);
            sequencer.Add(popTiming).SetAction(tim => {
                GameManager.EmergeGhost.OnNext(ghostNote);
            });
        }
        GameManager.GameScore.m_numGhost.Value = ghostNum;
    }
}
