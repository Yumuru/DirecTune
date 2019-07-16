using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GamePlayerMain : MonoBehaviour {
    public VRPlayer m_vRPlayer;
    public Subject<int> m_OnConducted = new Subject<int>();
}
