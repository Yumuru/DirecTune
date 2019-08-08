using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class ExtendMethods {
    public static T Do<T>(this T obj, Action action) {
        action(); return obj;
    }
    public static T Do<T>(this T obj, Action<T> action) {
        action(obj); return obj;
    }

    public static IObservable<TimeParameter> Anim(this Component component, float actTime) {
        var subject = new Subject<TimeParameter>();
        var sTime = Time.time;
        var stop = false;
        component.UpdateAsObservable()
            .TakeUntil(component.OnDestroyAsObservable())
            .TakeWhile(_ => !stop)
            .Subscribe(_ => {
                var time = Time.time - sTime;
                if (time > actTime) {
                    subject.OnNext(new TimeParameter() { time = actTime, rate = 1f });
                    stop = true;
                    return;
                }
                subject.OnNext(new TimeParameter() { time = time, rate = time / actTime });
            });
        return subject;
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
