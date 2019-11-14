using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhost : MonoBehaviour {
	public StageLane m_stageLane;
	TimingManager m_timingManager;

    public float high = 0.5f;

	public float m_missTime;
	public Subject<Unit> m_onConducted = new Subject<Unit>();
	public Subject<Unit> m_onFailed = new Subject<Unit>();
	public int m_blockPosition;
	public ParticleSystem m_missEffectPrefab;

	public void Initialize(StageLane stageLane, int blockPosition) {
		m_stageLane = stageLane;
		m_blockPosition = blockPosition;
		transform.position = m_stageLane.m_blocks[m_blockPosition].transform.position;
        var pos = new Vector3(transform.position.x, transform.position.y + high,transform.position.z);
        transform.position = pos;
		transform.localRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
	}

    private void Start() {
        m_onConducted
            .Subscribe(_ => {
                Destroy(gameObject);
            });
		m_onFailed
			.Subscribe(_ => {
				Instantiate(m_missEffectPrefab)
					.Do(o => o.transform.position = transform.position)
					.PlayDestroy();
				Observable.Timer(TimeSpan.FromSeconds(m_missTime))
					.Subscribe(_2 => Destroy(gameObject));
			});
		this.OnDestroyAsObservable()
			.Subscribe(_ => {
				m_onConducted.OnCompleted();
				m_onFailed.OnCompleted();
			});
	}
}
