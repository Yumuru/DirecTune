using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UIButton : MonoBehaviour, IUIInput {
	public UIInput uiInput { get; }
	public Renderer renderer;
	public Color m_normalColor, m_aimedColor, m_pushColor;
	public void Start() {
		uiInput.Initialize(this.OnDestroyAsObservable());
		renderer = GetComponent<Renderer>();
		uiInput.onAim
			.Where(_ => !uiInput.isPushed)
			.Select(b => b ? m_aimedColor : m_normalColor)
			.Subscribe(c => {
				renderer.material.color = c;
			});
	}
}
