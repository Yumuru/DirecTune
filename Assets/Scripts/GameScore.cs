using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameScore : MonoBehaviour{
    public int m_maxScore;
    public ReactiveProperty<int> m_numConductedGhost = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> m_numGhost = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> m_score = new ReactiveProperty<int>(0);
    public ReactiveProperty<float> m_rateScore = new ReactiveProperty<float>(0f);

    public Text text1;
    public Text text2;

    void Awake() {
        GetComponentInParent<GameManager>().m_gameScore = this;
    }

    public void Start() {
        m_numConductedGhost.Subscribe(num => {
            m_rateScore.Value = (float)num / m_numGhost.Value;
            m_score.Value = (int)(m_rateScore.Value * m_maxScore);
        });
        m_numConductedGhost.Value = 0;

        m_score.Subscribe(s => {
            text1.text = s.ToString();
            text2.text = s.ToString();
        });
        m_rateScore.Value = 0f;
        m_score.Value = 0;
    }
}
