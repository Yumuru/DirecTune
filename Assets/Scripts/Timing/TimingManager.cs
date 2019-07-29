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
    static Timing m_laneTimingLength = new Timing();
    public static Subject<Unit> OnStep { get; } = new Subject<Unit>();
    public static Subject<Unit> OnStepAfter { get; } = new Subject<Unit>();

    [SerializeField]
    int m_stepNum;
    public static int StepNum { get { return Instance.m_stepNum; } }
    int m_laneLength;
    
    void Awake() => Instance = this;

    void Start() {
        StartStepEvent();
    }

    void StartStepEvent() {
        var stepTiming = new Timing(0);
        var subject = new Subject<Unit>();
        var subjectAfter = new Subject<Unit>();
        this.UpdateAsObservable()
            .Where(_ => Music.IsJustChangedAt(stepTiming))
            .Subscribe(_ => {
                subject.OnNext(Unit.Default);
                subjectAfter.OnNext(Unit.Default);
            }, () => {
                subject.OnCompleted();
                subjectAfter.OnCompleted();
            });
        subject.Subscribe(_ => stepTiming.Add(StepTimingLength, Music.CurrentSection));
        subject.Subscribe(OnStep);
        subjectAfter.Subscribe(OnStepAfter);
    }

    public static Timing StepTimingLength { get { return Instance.m_stepTimingLength; } }
    public static Timing LaneTimingLength { get { 
        m_laneTimingLength.Set(0, 0, StepTimingLength.CurrentMusicalTime * Instance.m_stepNum);
        m_laneTimingLength.Fix(Music.CurrentSection);
        return m_laneTimingLength;
    } }
    public static int LaneLength { get { return LaneTimingLength.CurrentMusicalTime / StepTimingLength.CurrentMusicalTime; } }
    public static bool CouldConduct(Timing timing) {
        var current = Music.Just.CurrentMusicTime() + (float)Music.TimeSecFromJust;
        var targetTime = timing.CurrentMusicTime();
        return Mathf.Abs(current - targetTime) < Instance.m_canConductedRange;
    }
}
