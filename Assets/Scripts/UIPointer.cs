using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UIPointer : MonoBehaviour {
	VRStick m_stick;
	public LineRenderer m_lineRenderer;
	private void Awake() {
		m_stick = GetComponentInParent<VRStick>();
		m_lineRenderer = GetComponentInChildren<LineRenderer>();
	}
	private void Start() {
		UIInput lastInput = null;
		
		// Aim
		this.UpdateAsObservable()
			.Subscribe(_ => {
				Ray ray = new Ray(transform.position, transform.forward);
				var layer = LayerMask.NameToLayer("UI");
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, layer)) {
					var input = hit.collider.GetComponent<IUIInput>();
					if (input == null) return;
					if (lastInput != input.uiInput) {
						lastInput.onAim.OnNext(false);
						input.uiInput.onAim.OnNext(true);
						lastInput = input.uiInput;
					}
				}
				if (lastInput != null) {
					lastInput.onAim.OnNext(false);
				}
			});

		// Push
		UIInput pushingInput = null;
		var press = m_stick.buttons.trigger.press;
		press.down.Subscribe(_ => {
			pushingInput = lastInput;
			pushingInput.onPush.OnNext(true);
		});
		press.up.Subscribe(_ => {
			if (pushingInput == null) return;
			pushingInput.onPush.OnNext(false);
			pushingInput = null;
		});
	}
}
