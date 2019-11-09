using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhostManager : MonoBehaviour {
	[SerializeField]
	EnemyGhost_N m_enemyGhost;
	public EnemyGhostStep m_ghostStep;
	public EnemyAttackGuide m_attackGuide;

	private void Awake() {
		GameManager_N.Ins.m_enemyGhostManager = this;
	}

	public EnemyGhost_N SpawnGhost(int laneId) {
		var ghost = Instantiate(m_enemyGhost);
		var lane = GameManager_N.Ins.m_stageManager.stageLaneController.m_stageLanes[laneId];
		lane.m_ghosts.Add(ghost);
		ghost.transform.parent = lane.transform;
		ghost.Initialize(lane, lane.m_num-1);
		ghost.OnDestroyAsObservable()
			.Subscribe(_ => lane.m_ghosts.Remove(ghost));
		m_ghostStep.SetAction(ghost);
		return ghost;
	}
}
