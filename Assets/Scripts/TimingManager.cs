using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TimingManager : MonoBehaviour {
    public static TimingManager Instance { get; private set; }

    [SerializeField]
    public float m_canConductedRange;

    [SerializeField]
    Timing m_stepTimingLength;
    Timing m_laneTimingLength;
    [SerializeField]
    int m_stepNum;
    int m_laneLength;
    
    void Awake() => Instance = this;

    public static Timing StepTimingLength { get { return Instance.m_stepTimingLength; } }
    public static Timing LaneTimingLength { get { 
        var timing = new Timing(0, 0, StepTimingLength.CurrentMusicalTime * Instance.m_stepNum);
        timing.Fix(Music.CurrentSection);
        return timing;
     } }
    public static int LaneLength { get { return LaneTimingLength.CurrentMusicalTime / StepTimingLength.CurrentMusicalTime; } }
    public static bool CouldConduct(Timing timing) {
        var current = Music.AudioTimeSec;
        var targetTime = (Music.CurrentSection.StartTimeSamples) / 1000f + timing.CurrentMusicTime();
        return Mathf.Abs(current - targetTime) < Instance.m_canConductedRange;
    }
}
