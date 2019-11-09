/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ClarinetGhost : MonoBehaviour {
    public PerformerGhost m_performerGhost;

    private void Start() {
        m_performerGhost.Initialize(gameObject);
        m_performerGhost.Play();

        var angle = 20f;
        var leftRightAjust = 1;
        var anim = new TimeAffector<ClarinetGhost>()
            .Append(0f, TimingManager.StepTimingLength.CurrentMusicTime(), cg => new {
                sRot = cg.transform.rotation,
                toRot = Quaternion.AngleAxis(angle * leftRightAjust, Vector3.up)
            }, (para, a) => a
            .Append(cg => {
                var sVal = para().sValue;
                cg.transform.rotation = Quaternion.Lerp(sVal.sRot, sVal.toRot, para().rate);
                leftRightAjust *= -1;
            }));
        var play = Affector.New<ClarinetGhost>()
            .Append(cg => TimingManager.OnStep
            .SelectMany(_ => anim.Play(cg))
            .TakeUntil(this.OnDestroyAsObservable())
            .Subscribe());
        play(this);
    }
}
*/
