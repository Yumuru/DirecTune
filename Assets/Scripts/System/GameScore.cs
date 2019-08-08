using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameScore {
    public int m_maxScore;
    public ReactiveProperty<int> m_score = new ReactiveProperty<int>();
    public IObservable<float> m_rateScore;

    public GameScore() {
        m_rateScore = m_score
            .Select(s => (float)m_maxScore / s)
            .Publish().RefCount();
    }
}
