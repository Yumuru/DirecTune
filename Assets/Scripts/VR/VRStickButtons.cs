using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using static SteamVR_Controller;

public class VRStickButtons {
    public Button applicationMenu;
    public Button touchpad;
    public Button trigger;
    public Button grip;

    public VRStickButtons(VRStick stick) {
        applicationMenu = new Button(stick, ButtonMask.ApplicationMenu);
        touchpad = new Button(stick, ButtonMask.Touchpad);
        trigger = new Button(stick, ButtonMask.Trigger);
        grip = new Button(stick, ButtonMask.Grip);
    }
    
    public class Button {
        public Recog touch;
        public Recog press;
        public Button(VRStick stick, ulong buttonMask) {
            touch = new Recog(
                GetRecog(stick, buttonMask, touchDown),
                GetRecog(stick, buttonMask, touchUp)
            );
            press = new Recog(
                GetRecog(stick, buttonMask, pressDown),
                GetRecog(stick, buttonMask, pressUp)
            );
        }
    }

    public class Recog {
        public IObservable<Unit> down;
        public IObservable<Unit> up;
		public bool isRecog = false;
        public Recog(IObservable<Unit> down, IObservable<Unit> up) {
            this.down = down;
            this.up = up;
			down.Select(_ => true)
				.Merge(up.Select(_ => false))
				.Subscribe(b => isRecog = b);
        }
    }

    static Func<VRStick, ulong, Func<Unit, bool>> pressDown = 
        (stick, buttonMask) => _ => stick.device.GetPressDown(buttonMask);
    static Func<VRStick, ulong, Func<Unit, bool>> pressUp = 
        (stick, buttonMask) => _ => stick.device.GetPressUp(buttonMask);
    static Func<VRStick, ulong, Func<Unit, bool>> touchDown = 
        (stick, buttonMask) => _ => stick.device.GetTouchDown(buttonMask);
    static Func<VRStick, ulong, Func<Unit, bool>> touchUp = 
        (stick, buttonMask) => _ => stick.device.GetTouchUp(buttonMask);

    static IObservable<Unit> GetRecog(VRStick stick, ulong buttonMask, Func<VRStick, ulong, Func<Unit, bool>> judge) {
        var judge_ = judge(stick, buttonMask);
        return stick.UpdateAsObservable().Where(judge_).Publish().RefCount();
    }
}
