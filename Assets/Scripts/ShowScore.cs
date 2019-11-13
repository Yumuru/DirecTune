using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ShowScore : MonoBehaviour
{
    private Text m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        var gameScore = GameManager.Ins.m_gameScore;
        m_scoreText = GetComponent<Text>();
        gameScore.m_score.Subscribe(p => {
            m_scoreText.text = p.m_score.ToString();
        });
    }

}
