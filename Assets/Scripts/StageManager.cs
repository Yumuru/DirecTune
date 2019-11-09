using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	[HideInInspector]
	public StageLaneController stageLaneController;

	private void Awake() {
		GameManager_N.Ins.m_stageManager = this;
	}
}
