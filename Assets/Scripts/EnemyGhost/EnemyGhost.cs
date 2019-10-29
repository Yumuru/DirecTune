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
    public Transform m_direBaseJumpBack;
    public Action Remove { get; private set; }
    public Renderer m_renderer;
    public Animator m_animator;

    public Affector<Transform, IDisposable> stepAnimPlay;
    public Affector<Transform, IDisposable> goBackAnimPlay;
    public Affector<EnemyGhost, IDisposable> attackAnimPlay;

    void Start() {
        GeneAnim();
        SetStep();
    }

    // 開発中の自作ライブラリを試用してみました by ユムル
    void GeneAnim() {
        var lane = GhostStageManager.StageLane.m_lanes[m_parameter.m_lane];
        // 1ステップアニメーションの生成
        var stepAnim = new TimeAffector<Transform>()
            .Append(0f, GhostManager.TimeGhostStep, t => new {
                sPos = t.position,
                ePos = lane.m_block[--m_position].transform.position
            }, (p, a) => a.SetPosition(t => {
                var pos = Vector3.Lerp(p().sValue.sPos, p().sValue.ePos, p().rate);
                var vpos = Vector3.up *
                    Mathf.Sin(p().rate * Mathf.PI) * 0.5f;
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
                var jumpDire = Vector3.Scale(
                        (m_direBaseJumpBack.position - transform.position).normalized, 
                        new Vector3(UnityEngine.Random.value > 0.5f ? 1f : 0f, 1f, 1f))
                    + new Vector3(
                        UnityEngine.Random.value,
                        UnityEngine.Random.value,
                        0f) * 0.04f;
                var up = Vector3.ProjectOnPlane(jumpDire, dire);
                return dire;
            }));
        goBackAnimPlay = Affector.New<Transform>()
            .Append(t => goBackAnim.Play(t)
            .TakeUntil(m_onStep)
            .DoOnCompleted(() => Destroy(gameObject))
            .Subscribe());

        var player = GameManager.GamePlayer;
        var materials = m_renderer.materials;
        var attackAnim = new TimeAffector<GameObject>()
                .Append(0f, GhostManager.TimeGhostStep, go => go.transform.position,
                (para, a) => a.Append(go => {
                    var pos = Vector3.Lerp(para().sValue, player.transform.position, para().rate);
                    go.transform.position = pos;
                    foreach (var mat in materials) {
                        var color = mat.color;
                        color.a = 1f - para().rate;
                        mat.color = color;
                    }
                }));
        attackAnimPlay = Affector.New<EnemyGhost>()
            .Append(g => attackAnim.Play(g.gameObject)
            .DoOnCompleted(() => Destroy(gameObject))
            .Subscribe());
    }

    public void Damage() {
        Remove();
        m_animator.SetTrigger("Damage");
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
                Remove();
                attackAnimPlay(this);
            });
    }

    public void Initialize(GhostNoteParameter parameter) {
        m_parameter = parameter;
        var lane = GhostStageManager.StageLane.m_lanes[parameter.m_lane];
        m_position = lane.m_block.Length-1;
        transform.position = lane.m_block[m_position].transform.position;
        transform.rotation = Quaternion.LookRotation(-lane.m_direction, Vector3.up);
        Remove = lane.AddGhost(this);

        Instantiate(GhostManager.EmergeParticle, transform.position, transform.rotation).PlayDestroy();
    }
}