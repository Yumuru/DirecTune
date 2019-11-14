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
	public GhostSpownManager m_ghostSpawnManager;
    public GameObject m_pressMessageUI;

	public Subject<Unit> m_onPlay = new Subject<Unit>();
    public enum State
    {
        Start,
        Play,
        End
    }
    public State m_currentState = State.Start;

	public Subject<Unit> m_onEnd = new Subject<Unit>();
	private void Awake() {
		Ins = this;
	}

	private void Start() {
        m_onPlay.Subscribe(_ => {
            Music.Play("Music");
            m_musicManager.m_source.Play();
            State m_correntState = State.Play;
			m_ghostSpawnManager.m_playableDirector.Play();
            m_pressMessageUI.SetActive(false);
        });
		this.UpdateAsObservable()
			.Where(_ => Input.GetKeyDown(KeyCode.P))
			.Subscribe(_ => {
				m_onPlay.OnNext(Unit.Default);
			});
		this.OnDestroyAsObservable()
			.Subscribe(_ => m_onPlay.OnCompleted());
	}
    public void Restart() { }
    public void SceneReset()
    {
        
    }
}
