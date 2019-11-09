using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackGuide : MonoBehaviour {
	private void Awake() {
		GameManager_N.Ins.m_enemyGhostManager.m_attackGuide = this;
	}
}
