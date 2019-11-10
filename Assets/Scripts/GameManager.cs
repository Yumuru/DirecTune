using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour {
	[SerializeField]
	public static GameManager Ins;
	public EnemyGhostManager m_enemyGhostManager;
	public StageManager m_stageManager;
	public TimingManager timingManager;
	public GameScore m_gameScore;
	public MusicManager m_musicManager;

	public Subject<Unit> m_onPlay = new Subject<Unit>();

	private void Awake() {
		Ins = this;
	}

	private void Start() {
		this.UpdateAsObservable()
			.Where(_ => Input.GetKeyDown(KeyCode.P))
			.Subscribe(_ => {
				Music.Play("Music");
				m_onPlay.OnNext(Unit.Default);
			});
		this.OnDestroyAsObservable()
			.Subscribe(_ => m_onPlay.OnCompleted());
	}
}
