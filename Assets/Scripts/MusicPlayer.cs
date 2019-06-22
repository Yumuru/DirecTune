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
    public static CheckAndDo Read(MusicScore score) {
        var checkAndDo = new CheckAndDo();
        foreach (var note in score.notes) {
            var timing = note.timing;
            checkAndDo.Append(() => Music.IsJustChangedAt(timing));
        }
        return checkAndDo;
    }
}

public class CheckAndDo {
    public Part start, last;
    List<Part> parts = new List<Part>();

    public IObservable<Unit> Append(Func<bool> wait) {
        var part = new Part(wait);
        if (start == null) {
            start = part;
        } else {
            last.next = part;
        }
        last = part;
        return part.subject;
    }
    public Player GeneratePlayer(int pos) {
        var part = start;
        while (pos-- > 0) part = part.next;
        return new Player(part);
    }

    public class Player {
        public Subject<Unit> invoke = new Subject<Unit>();
        public Part current;
        public Player(Part start) {
            this.current = start;
            invoke
                .TakeWhile(_ => current != null)
                .Where(_ => current.wait())
                .Subscribe(_ => {
                    current.subject.OnNext(Unit.Default);
                    current = current.next;
                });
        }
    }

    public class Waitor {
        public Subject<Unit> next = new Subject<Unit>();
        public Subject<Unit> prev = new Subject<Unit>();
    }
}
