using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameManager_N : MonoBehaviour {
	public static GameManager_N Ins { get; private set; }
	public EnemyGhostManager m_enemyGhostManager;
	public StageManager m_stageManager;
	public TimingManager_N timingManager;

	public Subject<Unit> m_onPlay;

	private void Awake() {
		Ins = this;
	}

	private void Start() {
		this.OnDestroyAsObservable()
			.Subscribe(_ => m_onPlay.OnCompleted());
	}
}
