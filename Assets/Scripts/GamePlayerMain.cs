using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GamePlayerMain : MonoBehaviour {
    VRPlayer m_vRPlayer;

    private void Awake() => m_vRPlayer = GetComponent<VRPlayer>();

    void Start() {
    }
}
