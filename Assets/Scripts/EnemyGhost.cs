using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhost : MonoBehaviour {
	public StageLane m_stageLane;
	TimingManager m_timingManager;

	public Subject<Unit> m_onConducted = new Subject<Unit>();
	public Subject<Unit> m_onFailed = new Subject<Unit>();
	public int m_blockPosition;
	public ParticleSystem m_missEffectPrefab;

	public void Initialize(StageLane stageLane, int blockPosition) {
		m_stageLane = stageLane;
		m_blockPosition = blockPosition;
		transform.position = m_stageLane.m_blocks[m_blockPosition].transform.position;
		transform.localRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
	}

	private void Start() {
		m_onFailed
			.Subscribe(_ => {
				Instantiate(m_missEffectPrefab, transform.position, transform.rotation)
					.PlayDestroy();
			});
		this.OnDestroyAsObservable()
			.Subscribe(_ => {
				m_onConducted.OnCompleted();
				m_onFailed.OnCompleted();
			});
	}
}
