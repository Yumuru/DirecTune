using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Playables;

public class ResultEffect : MonoBehaviour
{
    public Sprite[] letterSprites = new Sprite[5];
    public Image letterImage;
    public Text score;
    public GameObject performerGhost;
    private PlayableDirector m_playableDirctor;

    // Start is called before the first frame update
    void OnEnable(){
        performerGhost.SetActive(false);
        m_playableDirctor = this.GetComponent<PlayableDirector>();
        m_playableDirctor.Play();
        GameScore gameScore = GameManager.Ins.m_gameScore;
        score.text = gameScore.m_score.Value.m_score.ToString();
        SetLetterImage(gameScore.m_score.Value.m_rate);
		GameManager.Ins.m_onEnd.OnNext(Unit.Default);
    }

    void SetLetterImage(float rate){
        if(rate>0.8f){
            letterImage.sprite = letterSprites[4];
        }else if(rate<=0.8f&&rate>0.6f){
            letterImage.sprite = letterSprites[3];
        }else if(rate<=0.6f&&rate>0.4f){
            letterImage.sprite = letterSprites[2];
        }else if(rate<=0.4f&&rate>0.2f){
            letterImage.sprite = letterSprites[1];
        }else{
            letterImage.sprite = letterSprites[0];
        }
    }
}
