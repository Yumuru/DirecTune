using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public interface IUIInput {
	UIInput uiInput { get; }
}

public class UIInput {
	public Subject<bool> onAim = new Subject<bool>();
	public Subject<bool> onPush = new Subject<bool>();
	public bool isAimed;
	public bool isPushed;

	public void Initialize(IObservable<Unit> stop) {
		onAim.Subscribe(b => isAimed = b);
		onPush.Subscribe(b => isPushed = b);
		stop.Subscribe(_ => {
			onAim.OnCompleted();
			onPush.OnCompleted();
		});
	}
}
