using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameManager_N : MonoBehaviour {
	[SerializeField]
	public static GameManager_N Ins;
	public EnemyGhostManager m_enemyGhostManager;
	public StageManager m_stageManager;
	public TimingManager_N timingManager;

	public Subject<Unit> m_onPlay = new Subject<Unit>();

	private void Awake() {
		Ins = this;
	}

	private void Start() {
		this.OnDestroyAsObservable()
			.Subscribe(_ => m_onPlay.OnCompleted());
	}
}
