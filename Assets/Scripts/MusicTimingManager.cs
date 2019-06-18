using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTimingManager : MonoBehaviour {
    public static MusicTimingManager Instance { get; private set; }
    void Awake() => Instance = this;

    void Start() {

    }
}
