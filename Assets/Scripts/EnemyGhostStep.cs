using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhostStep : MonoBehaviour {
	TimingManager m_timingManager;
	public float m_jumpHeight;
	private void Awake() {
		GetComponentInParent<EnemyGhostManager>().m_ghostStep = this;
	}

	private void Start() {
		m_timingManager = GameManager.Ins.timingManager;
	}

	public void SetAction(EnemyGhost ghost) {
		var lane = ghost.m_stageLane;
		m_timingManager.m_onStep
			.TakeUntil(ghost.OnDestroyAsObservable())
			.TakeUntil(ghost.m_onConducted)
			.TakeWhile(_ => ghost.m_blockPosition >= 0)
			.Subscribe(_ => {
				var sPos = lane.m_blocks[ghost.m_blockPosition].transform.position;
				ghost.m_blockPosition--;
				if (ghost.m_blockPosition < 0) return;
				var ePos = lane.m_blocks[ghost.m_blockPosition].transform.position;
				ghost.Anim(m_timingManager.m_stepLength.CurrentMusicTime())
					.TakeUntil(ghost.m_onConducted)
					.Subscribe(para => {
						var rate = para.rate;
						var pos = Vector3.Lerp(sPos, ePos, rate);
						pos += Vector3.up * Mathf.Sin(Mathf.PI * rate) * m_jumpHeight;
						ghost.transform.position = pos;
					});
			});
		m_timingManager.m_onStep
            .TakeUntil(ghost.m_onConducted)
            .TakeUntil(ghost.m_onFailed)
			.Where(_ => ghost.m_blockPosition < 0)
			.Take(1)
			.Subscribe(_ => ghost.m_onFailed.OnNext(Unit.Default));
	}
}
