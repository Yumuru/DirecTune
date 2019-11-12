using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ConductStick : MonoBehaviour {
    GamePlayerMain m_playerMain;
    VRStick m_stick;
    public float m_thresholdSpeed;
    public ParticleSystem[] m_succParticles;
    public bool isDebug;

    public AudioClip sucSound;
    AudioSource audioSource;

    void Start() {
        m_playerMain = GetComponentInParent<GamePlayerMain>();
        m_stick = GetComponentInParent<VRStick>();

        SetConductAction();

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = sucSound;
    }

    void SetConductAction() {
        var onConduct = new Subject<Unit>();
        this.OnDestroyAsObservable()
            .Subscribe(_ => onConduct.OnCompleted());
        if (isDebug) {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.C))
                .Subscribe(_ => onConduct.OnNext(Unit.Default));
        }
        m_stick.UpdateAsObservable()
            .Where(_ => m_stick.device.velocity.sqrMagnitude >= Mathf.Pow(m_thresholdSpeed, 2f))
            .Subscribe(_ => onConduct.OnNext(Unit.Default));
        onConduct
            .Select(_ => m_playerMain.m_laneCurrent.GetCanConductGhost())
            .Where(v => v != null)
            .Where(ghost => TimingManager.CouldConduct(ghost.m_parameter.m_timing))
            .Subscribe(ghost => ConductGhost(ghost));
    }

    void ConductGhost(EnemyGhost ghost) {
        var lane = m_playerMain.m_laneCurrent;
        ghost.Damage();
        GameManager.GameScore.m_numConductedGhost.Value++;
        Instantiate(m_succParticles.RandomGet()
            , lane.m_block[0].transform.position
            , lane.m_block[0].transform.rotation)
            .PlayDestroy();
        if (ghost.m_parameter.m_strength == 2) {
            ghost.goBackAnimPlay(ghost.transform);
        } else {
            Observable.Timer(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ => Destroy(ghost.gameObject));
        }


        audioSource.Play();

    }
}
