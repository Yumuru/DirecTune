using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MusicScore : MonoBehaviour {

}

public class Note {
    public int lane;
    public Timing left;



    public static Note Create() {
        return new Note();
    }
}

public static class MusicScoreFunc {
}
