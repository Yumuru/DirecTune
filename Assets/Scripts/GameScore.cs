using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameScore : MonoBehaviour{
    public int m_maxScore;
    public int m_numGhost;
    public ReactiveProperty<int> m_numConductedGhost = new ReactiveProperty<int>(0);
    //public ReactiveProperty<int> m_numGhost = new ReactiveProperty<int>(0);
    public ReactiveProperty<ScoreParameter> m_score = new ReactiveProperty<ScoreParameter>(new ScoreParameter(){ m_score = 0, m_rate = 0f});


    void Awake() {
        GetComponentInParent<GameManager>().m_gameScore = this;
    }

    public void Start() {
        m_numConductedGhost.Subscribe(num => {
            var rate = (float)num / m_numGhost;
            var score = (int)(rate * m_maxScore);
            m_score.Value = new ScoreParameter(){ m_rate = rate, m_score = score};
        });
        m_numConductedGhost.Value = 0;
    }

    public struct ScoreParameter {
        public int m_score;
        public float m_rate;
    }
}
