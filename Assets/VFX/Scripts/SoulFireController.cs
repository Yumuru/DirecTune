using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SoulFireController : MonoBehaviour
{
    public GameObject[] soulFireSet;
    [Range(0f, 150f)] public float score;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in soulFireSet) {
            item.SetActive(false);
        }
        GameManager.GameScore
            .m_rateScore
            .Subscribe(rate => score = rate * 150f);
    }

    // Update is called once per frame
    void Update(){
        if (score > 100f) {
            soulFireSet[4].SetActive(true);
        } else if(score >= 80f && score < 100f) {
            soulFireSet[3].SetActive(true);
        }else if(score >= 60f && score < 80f) {
            soulFireSet[2].SetActive(true);
        }else if(score >= 40f && score < 60f) {
            soulFireSet[1].SetActive(true);
        }else if(score >= 20f && score < 40f) {
            soulFireSet[0].SetActive(true);
        }
    }
}
