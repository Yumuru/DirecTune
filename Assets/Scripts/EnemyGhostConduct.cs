using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyGhostConduct : MonoBehaviour {
	VRStick m_stick;
	public float m_thresholdSpeed;
	public ParticleSystem[] m_succParticles;
	public AudioClip m_sucSound;
    public Vector3 effOffset = new Vector3(0f, 1f, 0f);
	private void Awake() {
		m_stick = GetComponentInParent<VRStick>();
	}

	private void Start() {
		var head = m_stick.head;
		var stageManager = GameManager.Ins.m_stageManager;
		var laneController = stageManager.m_stageLaneController;
		var audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = m_sucSound;
		m_stick.UpdateAsObservable()
			.Where(_ => m_stick.device.velocity.sqrMagnitude >= Mathf.Pow(m_thresholdSpeed, 2f))
			.Subscribe(_ => {
				var dire = Vector3.Scale(m_stick.transform.position - head.transform.position, new Vector3(1,0,1)).normalized;
				foreach (var lane in laneController.m_stageLanes) {
					var dot = Vector3.Dot(dire, lane.transform.forward);
                    //print(dot);
                    if (dot > 0.8) {
						var ghost = lane.GetFirstGhost();
						if (ghost == null) return;
						audioSource.Play();
						ghost.m_onConducted.OnNext(Unit.Default);
						Instantiate(m_succParticles.RandomGet()
							, lane.m_blocks[0].transform.position+effOffset
							, lane.m_blocks[0].transform.rotation)
							.PlayDestroy();
					}
				}
			});
	}
}
