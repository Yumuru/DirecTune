using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using static MusicScore;

public class MusicScore {
    public AudioClip[] clips; 
    public List<Note> notes = new List<Note>();
    public MusicScore(AudioClip[] clips) {
        this.clips = clips;
    }

    public enum EnemyPattern {
        Normal,
        Strong
    }

    public MusicScore AddNote(Timing timing, int lane, EnemyPattern pattern) {
        notes.Add(new Note(timing, lane, pattern));
        return this;
    }
}

public class Note {
    public Timing timing;
    public int lane;
    public MusicScore.EnemyPattern pattern;
    public Note(Timing timing, int lane, EnemyPattern pattern) {
        this.timing = timing;
        this.lane = lane;
        this.pattern = pattern;
    }
}

public static class MusicScoreFunc {
}
