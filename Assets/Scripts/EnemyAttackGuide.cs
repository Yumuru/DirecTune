using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackGuide : MonoBehaviour {
	private void Awake() {
		GetComponentInParent<EnemyGhostManager>().m_attackGuide = this;
	}
}
