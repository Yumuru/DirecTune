using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameScore {
    public int m_maxScore;
    public ReactiveProperty<int> m_score = new ReactiveProperty<int>();
    public ReactiveProperty<float> m_rateScore = new ReactiveProperty<float>();
    public Subject<Unit> m_onDestroy = new Subject<Unit>();

    public GameScore() {
        m_score
            .TakeUntil(m_onDestroy)
            .Subscribe(s =>
            m_rateScore.Value = (float)m_maxScore / s);
    }
}
