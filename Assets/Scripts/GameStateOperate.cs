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
        var buttons = stick.buttons;
        var anyPress = buttons.trigger.press.down
            .Merge(buttons.applicationMenu.press.down)
            .Merge(buttons.grip.press.down)
            .Merge(buttons.touchpad.press.down);
        anyPress
			.Where(_ => GameManager.Ins.m_currentState == GameManager.State.Start)
			.Subscribe(_ => {
            GameManager.Ins.m_onPlay.OnNext(Unit.Default);
            gameStartUI.SetActive(false);
        });

		buttons.applicationMenu.press.down
			.Where(_ => buttons.trigger.press.isRecog)
			.Where(_ => GameManager.Ins.m_currentState == GameManager.State.End)
			.Subscribe(_ => {
				GameManager.Ins.SceneReset();
			});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
