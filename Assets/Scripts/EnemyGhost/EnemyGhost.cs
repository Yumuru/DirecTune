using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class EnemyGhost : MonoBehaviour {
    public int m_position;
    public GhostNoteParameter m_parameter;
    public bool m_canConduct = false;
    public Subject<Unit> m_onStep = new Subject<Unit>();
    //public int m_position = 0;//lane position->GhostNoteParameter.m_box
    void SetStep() {
        m_onStep
            .TakeWhile(_ => m_position != 0)
            .Subscribe(_ => {
                var lane = GhostStageManager.GetInstance.m_makeStage.m_stage[m_parameter.m_lane];
                var cPos = transform.position;
                var nextPos = lane.m_block[--m_position].transform.position;
                this.Anim(GhostManager.TimeGhostStep) // Yumuruさんによるコンポーネント拡張メソッド
                    .Subscribe(time => {
                        var pos = Vector3.Lerp(cPos, nextPos, time.rate);
                        var vpos = Vector3.up *
                            Mathf.Sin(time.rate * Mathf.PI) * 0.2f;
                        transform.position = pos + vpos;
                    });
                if (m_position == 1) {
                    m_canConduct = true;
                }
            });
        m_onStep
            .SkipWhile(_ => m_position != 0)
            .SelectMany(m_onStep.Skip(1).Take(1))
            .Subscribe(_ => {
                GhostStageManager.GetInstance.m_makeStage.m_stage[m_parameter.m_lane].m_ghosts.Remove(this);
                Destroy(gameObject);
            });
    }
    public void Initialize(GhostNoteParameter parameter) {
        m_parameter = parameter;
        var lane = GhostStageManager.GetInstance.m_makeStage.m_stage[parameter.m_lane];
        m_position = lane.m_block.Length-1;
        transform.position = lane.m_block[m_position].transform.position;
        transform.rotation = Quaternion.LookRotation(-lane.m_direction, Vector3.up);
        lane.m_ghosts.Add(this);

        Instantiate(GhostManager.EmergeParticle, transform.position, transform.rotation).PlayDestroy();

        SetStep();
    }
}