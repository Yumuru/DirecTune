using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using static Easing;

public class EnemyGhost : MonoBehaviour {
    public int m_position;
    public GhostNoteParameter m_parameter;
    public bool m_canConduct = false;
    public Subject<Unit> m_onStep = new Subject<Unit>();
    Action m_remove;

    Affector<Transform, IDisposable> stepAnimPlay;
    Affector<Transform, IDisposable> goBackAnimPlay;

    void Start() {
        GeneAnim();
        SetStep();
    }

    // 開発中の自作ライブラリを試用してみました by ユムル
    void GeneAnim() {
        var lane = GhostStageManager.GetInstance.m_makeStage.m_stage[m_parameter.m_lane];
        // 1ステップアニメーションの生成
        var stepAnim = new TimeAffector<Transform>()
            .Append(0f, GhostManager.TimeGhostStep, t => new {
                sPos = t.position,
                ePos = lane.m_block[--m_position].transform.position
            }, (p, a) => a.SetPosition(t => {
                var pos = Vector3.Lerp(p().sValue.sPos, p().sValue.ePos, p().rate);
                var vpos = Vector3.up *
                    Mathf.Sin(p().rate * Mathf.PI) * 2.0f;
                return pos + vpos;
            }));
        stepAnimPlay = Affector.New<Transform>()
            .Append(t => stepAnim.Play(t)
            .TakeUntil(m_onStep)
            .Subscribe());

        var goBackAnim = new TimeAffector<Transform>()
            .Append(0f, GhostManager.TimeGhostStep, t => new {
                sPos = t.position,
                ePos = lane.m_block[lane.m_block.Length-1].transform.position
            }, (p, a) => a.SetPosition(t => {
                var dire = (p().sValue.ePos - p().sValue.sPos).normalized;
                var zpos = Vector3.Lerp(p().sValue.sPos, p().sValue.ePos, InCubic(p().rate));
                var up = Vector3.ProjectOnPlane(Vector3.up, dire);
                return dire;
            }));
    }

    void SetStep() {
        m_onStep
            .TakeWhile(_ => m_position != 0)
            .Subscribe(_ => {
                stepAnimPlay(this.transform);
                if (m_position == 1) {
                    m_canConduct = true;
                }
            });
        m_onStep
            .SkipWhile(_ => m_position != 0)
            .SelectMany(_ => m_onStep)
            .Take(1)
            .Subscribe(_ => {
                m_remove();
                Destroy(gameObject);
            });
    }
    public void Initialize(GhostNoteParameter parameter) {
        m_parameter = parameter;
        var lane = GhostStageManager.GetInstance.m_makeStage.m_stage[parameter.m_lane];
        m_position = lane.m_block.Length-1;
        transform.position = lane.m_block[m_position].transform.position;
        transform.rotation = Quaternion.LookRotation(-lane.m_direction, Vector3.up);
        m_remove = lane.AddGhost(this);

        Instantiate(GhostManager.EmergeParticle, transform.position, transform.rotation).PlayDestroy();
    }
}