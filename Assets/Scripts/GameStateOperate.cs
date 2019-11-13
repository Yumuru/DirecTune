using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameStateOperate : MonoBehaviour
{
    public GameObject gameStartUI;

    // Start is called before the first frame update
    void Start()
    {
        var stick = GetComponentInParent<VRStick>();
        stick.buttons.trigger.press.down.Subscribe(_ =>
        {
            if (GameManager.Ins.m_currentState != GameManager.State.Start) return;
            GameManager.Ins.m_onPlay.OnNext(Unit.Default);
            gameStartUI.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
