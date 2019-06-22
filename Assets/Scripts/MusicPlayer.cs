using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MusicPlayer {
    GameObject gameObject;
    public AudioSource source;

    public MusicPlayer(AudioSource source) {
        this.source = source;
        gameObject = source.gameObject;
    }

    public void Play() {
        Music.Play(gameObject.name);
        gameObject.UpdateAsObservable()
            .Subscribe(_ => {

            });
    }

    public void ReadScore(MusicScore score) {

    }
}

public class MusicScoreReader {
}

public class Player {
    
}

public class Waitor {
    public Subject<Unit> invoke = new Subject<Unit>();
    public Waitor next;
    public Waitor prev;
    public Waitor Append() => Append(new Waitor());
    public Waitor Append(Waitor next) {
        this.next = next;
        next.prev = this;
        return next;
    }
    public Waitor Invoke(Action<Subject<Unit>> setInvoke) {
        setInvoke(invoke);
        return this;
    }
}
