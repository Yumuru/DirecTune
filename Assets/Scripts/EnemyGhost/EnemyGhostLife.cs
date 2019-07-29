using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhostLife : MonoBehaviour {

    private void Start() {
        GameManager.EmergeGhost.Subscribe(para => {
            var ghost = GhostManager.Emerge(para);
            ghost.UpdateAsObservable()
                .Where(_ => Music.IsJustChanged).Take(1)
                .SelectMany(TimingManager.OnStep)
                .TakeUntil(ghost.OnDestroyAsObservable())
                .Subscribe(ghost.m_onStep);
        });
    }
}
