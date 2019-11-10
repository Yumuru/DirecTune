using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TimingSequencer {
    public Subject<Timing> m_update = new Subject<Timing>();
    public TimingSequencePart m_current;
    public bool m_isPlaying = false;
    IDisposable m_disposable;
    public TimingSequencer(TimingSequencePart start) {
        this.m_current = start;
        
        var runNext = new Subject<Timing>();
        m_disposable = 
        this.m_update
            .Where(_ => m_isPlaying)
            .Merge(runNext)
            .Where(tim => tim >= m_current.m_timing)
            .Subscribe(tim => {
                m_current.Invoke.OnNext(tim);
                if (m_current.m_next != null) {
                    m_current = m_current.m_next;
                    runNext.OnNext(tim);
                } else {
                    m_isPlaying = false;
                }
            });
    }
    public void Play() => m_isPlaying = true;
    public void Stop() => m_isPlaying = false;
    public void Complete() {
        m_disposable.Dispose();
        m_current.Complete();
    }
    public TimingSequencePart Add(Timing timing) => m_current.Add(timing);
    public TimingSequencePart Add(TimingSequencePart part) => m_current.Add(part);
}

public class TimingSequencePart : SequencePart<TimingSequencePart, Timing> {
    public Timing m_timing;
    public TimingSequencePart(Timing timing) {
        this.m_timing = timing;
    }

    public TimingSequencePart Add(Timing m_timing) => Add(new TimingSequencePart(m_timing));
    public TimingSequencePart Add(TimingSequencePart part) =>
        part.m_timing >= this.m_timing ?
            AddNext(part) :
            AddPrev(part);
    TimingSequencePart AddNext(TimingSequencePart part) =>
        m_next == null || part.m_timing < m_next.m_timing ?
            Append(part) :
            m_next.AddNext(part);
    TimingSequencePart AddPrev(TimingSequencePart part) =>
        m_prev == null || part.m_timing >= m_prev.m_timing ? 
            AppendPrev(part) :
            m_prev.AddPrev(part);
}

public class SequencePart<T, P> where T : SequencePart<T, P> {
    Subject<P> m_invoke = new Subject<P>();
    public ISubject<P> Invoke { get { return m_invoke; } }
    public T m_next;
    public T m_prev;
    public T Append(T part) {
        if (this.m_next != null) {
            this.m_next.m_prev = part;
            part.m_next = this.m_next;
        }
        part.m_prev = (T)this;
        this.m_next = part;
        return part;
    }
    public T AppendPrev(T part) {
        if (this.m_prev != null) {
            this.m_prev.m_next = part;
            part.m_prev = this.m_prev;
        }
        part.m_next = (T)this;
        this.m_prev = part;
        return part;
    }
    public T SetAction(Action<P> m_invoke) {
        this.m_invoke.Subscribe(m_invoke);
        return (T)this;
    }

    public void Complete() {
        this.m_invoke.OnCompleted();
        this.CompleteLine(p => p.m_next);
        this.CompleteLine(p => p.m_prev);
    } 
    void CompleteLine(Func<T, T> getter) {
        var part = getter((T)this);
        if (part != null) {
            part.m_invoke.OnCompleted();
            part.CompleteLine(getter);
        }
    }
}
