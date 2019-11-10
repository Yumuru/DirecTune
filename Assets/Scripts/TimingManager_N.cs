using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TimingManager_N : MonoBehaviour {
	public Timing m_current = new Timing(0);
	public Timing m_stepLength;
	public Subject<Unit> m_onStep = new Subject<Unit>();
	private void Awake() {
		GetComponentInParent<GameManager_N>().timingManager = this;
	}

	private void Start() {
		var start = new Timing(m_current);
		Timing next = new Timing();
		GameManager_N.Ins.m_onPlay.Subscribe(_ => {
			m_current.Copy(start);
			next.Copy(m_current);
			next.Add(m_stepLength, Music.CurrentSection);
		});

		this.UpdateAsObservable()
			.Where(_ => Music.Just >= next)
			.Subscribe(_ => {
				m_current.Add(m_stepLength, Music.CurrentSection);
				next.Add(m_stepLength, Music.CurrentSection);
				m_onStep.OnNext(Unit.Default);
			});

		this.OnDestroyAsObservable()
			.Subscribe(_ => m_onStep.OnCompleted());
	}
}
