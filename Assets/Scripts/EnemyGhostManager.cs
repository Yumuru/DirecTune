using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhostManager : MonoBehaviour {
	[SerializeField]
	EnemyGhost m_enemyGhostPrefab;
	public EnemyGhostStep m_ghostStep;
	public EnemyAttackGuide m_attackGuide;
    public GameObject m_spawnEffect;

	private void Awake() {
		GetComponentInParent<GameManager>().m_enemyGhostManager = this;
	}

	[ContextMenu("SpawnGhost")]
	void Test() {
		SpawnGhost(1);
	}

	public EnemyGhost SpawnGhost(int laneId) {
		var ghost = Instantiate(m_enemyGhostPrefab);
		var lane = GameManager.Ins.m_stageManager.m_stageLaneController.m_stageLanes[laneId];
		lane.m_ghosts.Add(ghost);
		ghost.transform.parent = lane.transform;
		ghost.Initialize(lane, lane.m_num-1);
		ghost.OnDestroyAsObservable()
			.Subscribe(_ => lane.m_ghosts.Remove(ghost));
		m_ghostStep.SetAction(ghost);
		return ghost;
	}
}
