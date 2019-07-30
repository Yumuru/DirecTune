using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class YumuruUtil {
    public static IObservable<TimeParameter> Anim(this Component component, float actTime) {
        var sTime = Time.time;
        var subject = new Subject<TimeParameter>();
        var stop = new Subject<Unit>();
        Observable.Return(Unit.Default)
            .SelectMany(component.UpdateAsObservable())
            .TakeUntil(stop)
            .Subscribe(_ => {
                var time = Time.time - sTime;
                if (time > actTime) {
                    subject.OnNext(new TimeParameter() { time = actTime, rate = 1f });
                    stop.OnNext(Unit.Default);
                }
                subject.OnNext(new TimeParameter() { time = time, rate = time / actTime });
            });
        return subject.TakeUntil(stop);
    }
    public struct TimeParameter {
        public float time;
        public float rate;
    }

    public static IObservable<Unit> PlayDestroy (this ParticleSystem particle) {
        var observable =
        particle.UpdateAsObservable()
            .SkipWhile(_ => particle.isPlaying);
        observable
            .Subscribe(_ => GameObject.Destroy(particle.gameObject));
        return observable;
    }

    public static T RandomGet<T>(this T[] values) {
        return values[UnityEngine.Random.Range(0, values.Length - 1)];
    }
}
