using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MusicEx {
    public static float CurrentMusicTime(this Timing timing) =>
        (float)Music.MusicTimeUnit * timing.CurrentMusicalTime;
}
