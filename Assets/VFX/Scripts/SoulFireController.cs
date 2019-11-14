using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SoulFireController : MonoBehaviour
{
    public GameObject[] soulFireSet;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in soulFireSet) {
            item.SetActive(false);
        }
        GameScore gameScore = GameManager.Ins.m_gameScore;
        gameScore.m_score.Subscribe(p => {
            FireActivete(p.m_rate);
        });
    }

    public void FireActivete(float rate) {
        if (rate > 0.8f) {
            soulFireSet[4].SetActive(true);
        } else if (rate >= 0.6f && rate < 0.8f) {
            soulFireSet[3].SetActive(true);
        } else if (rate >= 0.4f && rate < 0.6f) {
            soulFireSet[2].SetActive(true);
        } else if (rate >= 0.2f && rate < 0.4f) {
            soulFireSet[1].SetActive(true);
        } else if (rate < 0.2f) {
            soulFireSet[0].SetActive(true);
        }
    }
}
