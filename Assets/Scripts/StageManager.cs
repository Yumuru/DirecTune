using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	public StageLaneController m_stageLaneController;

	private void Awake() {
		GetComponentInParent<GameManager>().m_stageManager = this;
	}
}
