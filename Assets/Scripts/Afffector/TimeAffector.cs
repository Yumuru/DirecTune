using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimeAffector<T> {
    Affector<T, T> affector = v => v;
    Func<Action<T>> updater = () => v => { };
    public float sTime, eTime;

    float time;

    public IObservable<T> Play(T attract) {
        var time = 0f;
        var stop = false;
        var update = updater();
        return Observable.EveryUpdate()
            .TakeWhile(_ => !stop)
            .Select(_ => {
                if (time > eTime) {
                    time = eTime;
                    stop = true;
                }
                this.time = time;
                update(attract);
                var val =  affector(attract);
                time += Time.deltaTime;
                return val;
            });
    }

    public TimeAffector<T> Append<V>(float sTime, float eTime, Func<T, V> sValue, Func<Getter<Parameter<V>>, Affector<T, T>, Affector<T, T>> generateAffector) {
        if (sTime < this.sTime) this.sTime = sTime;
        if (eTime > this.eTime) this.eTime = eTime;
        Parameter<V> parameter = new Parameter<V>();
        var affector = this.affector;
        var updater = this.updater;
        this.updater = () => {
            var update = updater();
            V value = default(V);
            update += v => {
                parameter.time = time;
                parameter.rate = 
                    time <= sTime ? 0f :
                    time >= eTime ? 1f :
                    (time - sTime) / (eTime - sTime);
                if (time <= sTime) {
                    value = sValue(v);
                }
                parameter.sValue = value;
            };
            return update;
        };
        this.affector = affector.Append(generateAffector(() => parameter, Affector.New<T>()));
        return this;
    }

    public class Parameter<V> {
        public V sValue;
        public float time;
        public float rate;
    }
}
