using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UIButton : MonoBehaviour, IUIInput {
	public UIInput uiInput { get; }
	public Renderer renderer;
	public Material m_normal, m_aimed, m_push;
	enum State {
		Normal,
		Aimed,
		Push
	}
	public void Start() {
		uiInput.Initialize(this.OnDestroyAsObservable());
		renderer = GetComponent<Renderer>();
		var currentState = State.Normal;
		var previousState = currentState;
		uiInput.onAim
			.Subscribe(b => {
				if (b) {
					previousState = currentState;
					currentState = State.Aimed;

				}
			});
	}
}
