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
    public enum State
    {
        Start,
        Play,
        End
    }
    public State m_correntState = State.Start;

	private void Awake() {
		Ins = this;
	}

	private void Start() {
        m_onPlay.Subscribe(_ => {
            Music.Play("Music");
            State m_correntState = State.Play;
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
