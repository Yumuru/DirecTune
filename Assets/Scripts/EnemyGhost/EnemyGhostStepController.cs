using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhostStepController : MonoBehaviour {
    public float stepTime;
    public Subject<Unit> OnGhostStep = new Subject<Unit>();

	void Awake() { 
		GhostManager.EnemyGhostStep = this;
	}
}

public class EnemyGhostStep {
	public EnemyGhostStepController controller;

	public EnemyGhost enemyGhost;
	public int position;
	public bool canConduct = false;

	public EnemyGhostStep(EnemyGhost ghost) {
		controller = GhostManager.EnemyGhostStep;
		enemyGhost = ghost;
	}

    public EnemyGhostStep SetAction(int startPos) {
        position = startPos;
        canConduct = false;
        controller.OnGhostStep
			.TakeWhile(_ => position != 0)
			.Subscribe(_ => {
			StepAnimation();
            position--;
            if (position == 1) {
                canConduct = true;
            }
        });
		controller.OnGhostStep
			.Where(_ => position == 0)
			.Take(1)
			.Subscribe(_ => {
				// OnAttack
			});
        return this;
    }

    public IDisposable StepAnimation() {
		var sPos = enemyGhost.transform.position;
		var ePos = enemyGhost.m_lane.m_block[position].transform.position;
        return enemyGhost
            .Anim(GhostManager.TimeGhostStep)
            .Subscribe(tp => {
                var rate = tp.rate;
				enemyGhost.transform.position = Vector3.Lerp(sPos, ePos, rate)
					+ Vector3.up * Mathf.Sin(Mathf.PI * rate) * 0.5f;
            });
    }
}
