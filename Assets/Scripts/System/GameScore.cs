using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameScore : MonoBehaviour{
    public int m_maxScore;
    public ReactiveProperty<int> m_numConductedGhost = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> m_numGhost = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> m_score = new ReactiveProperty<int>(0);
    public ReactiveProperty<float> m_rateScore = new ReactiveProperty<float>(0f);

    public void Start() {
        m_numGhost.Subscribe(max => 
        m_numConductedGhost.Subscribe(num => {
            m_rateScore.Value = (float)num / max;
            m_score.Value = (int)(m_rateScore.Value * m_maxScore);
        }));
    }
}
