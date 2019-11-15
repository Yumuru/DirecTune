using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using UniRx.Triggers;

public class UIButton : MonoBehaviour, IUIInput {
	public UIInput uiInput { get; }
	public Renderer renderer;
	public Material m_normal, m_aimed, m_push;
	public UnityEvent m_pushEvent = new UnityEvent();
	public State currentState;
	public enum State {
		Normal,
		Aimed,
		Push
	}
	public void Start() {
		uiInput.Initialize(this.OnDestroyAsObservable());
		renderer = GetComponent<Renderer>();
		uiInput.onAim
			.Subscribe(b => {
				if (uiInput.isPushed) return;
				if (b) {
					currentState = State.Aimed;
					renderer.material = m_aimed;
				} else {
					currentState = State.Normal;
					renderer.material = m_normal;
				}
			});
		uiInput.onPush
			.Subscribe(b => {
				if (b) {
					currentState = State.Push;
					renderer.material = m_push;
				} else if (uiInput.isAimed) {
					m_pushEvent.Invoke();
					currentState = State.Aimed;
					renderer.material = m_aimed;
				} else {
					currentState = State.Normal;
					renderer.material = m_normal;
				}
			});
	}
}
