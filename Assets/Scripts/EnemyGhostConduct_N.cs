﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhostConduct_N : MonoBehaviour {
	VRStick m_stick;
	public float m_thresholdSpeed;
	public ParticleSystem[] m_succParticles;
	public bool isDebug;
	private void Awake() {
		m_stick = GetComponentInParent<VRStick>();
	}

	private void Start() {
		var head = m_stick.head;
		var stageManager = GameManager_N.Ins.m_stageManager;
		var laneController = stageManager.m_stageLaneController;
		m_stick.UpdateAsObservable()
			.Where(_ => m_stick.device.velocity.sqrMagnitude >= Mathf.Pow(m_thresholdSpeed, 2f))
			.Subscribe(_ => {
				var dire = m_stick.transform.position - head.transform.position;
				foreach (var lane in laneController.m_stageLanes) {
					var dot = Vector3.Dot(dire, lane.transform.forward);
					if (dot > 0.8) {
						var ghost = lane.GetFirstGhost();
						if (ghost == null) return;
						ghost.m_onConducted.OnNext(Unit.Default);
						Instantiate(m_succParticles.RandomGet()
							, lane.m_blocks[0].transform.position
							, lane.m_blocks[0].transform.rotation)
							.PlayDestroy();
					}
				}
			});
	}
}
