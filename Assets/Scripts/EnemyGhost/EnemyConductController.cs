using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConductController : MonoBehaviour {
	void Awake() { GameManager.EnemyConduct = this; }
}

public class EnemyConduct {
	public EnemyConduct() {

	}
}
