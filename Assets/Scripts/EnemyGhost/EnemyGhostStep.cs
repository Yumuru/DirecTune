using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class EnemyGhostStep : MonoBehaviour {
    public float stepTime;

    public Subject<Unit> OnGhostStep = new Subject<Unit>();
    public class Param {
        public int position;
        public bool canConduct = false;
    }
    public Param SetAction(EnemyGhost ghost, int startPos) {
        var param = new Param();
        param.position = startPos;
        param.canConduct = false;
        Sequence seq = null;
        OnGhostStep.Subscribe(_ => {
            seq?.Complete();
            seq = StepAnimation(ghost, param);
            param.position--;
            if (param.position == 0) {
                param.canConduct = true;
            }
        });
        return param;
    }

    public Sequence StepAnimation(EnemyGhost ghost, Param param) {
        var movep = DOTween.Sequence()
            .Join(ghost.transform.DOMove(
                ghost.m_lane.m_block[param.position].transform.position,
                stepTime));
        var movey = DOTween.Sequence()
            .Join(ghost.transform.DOMoveY(
                ghost.transform.position.y + 0.5f,
                stepTime).SetEase(Ease.OutSine))
            .Append(ghost.transform.DOMoveY(
                ghost.transform.position.y,
                stepTime).SetEase(Ease.InSine));
        return DOTween.Sequence()
            .Join(movep)
            .Join(movey).Play();
    }
}
