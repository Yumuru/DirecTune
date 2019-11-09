using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	public static UIManager Ins { get; private set; }
	private void Awake() {
		Ins = this;
	}
}
