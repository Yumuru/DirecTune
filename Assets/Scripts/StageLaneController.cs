using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StageLaneController : MonoBehaviour {
	[SerializeField]
	StageLane m_stageLanePrefab;
	public List<StageLane> m_stageLanes = new List<StageLane>();

	public Transform m_center;
	public Transform[] m_to;

	private void Awake() {
		GetComponentInParent<StageManager>().m_stageLaneController = this;
	}

	// Start is called before the first frame update
	void Start() {
    }

	[ContextMenu("SetStageLane")]
	void SetStageLane() {
		foreach (var lane in m_stageLanes) { DestroyImmediate(lane.gameObject); }
		m_stageLanes.Clear();
		foreach (var to in m_to) {
			var dire = (to.position - m_center.position).normalized;
			var stageLane = Instantiate(m_stageLanePrefab);
			stageLane.transform.parent = m_center;
			stageLane.transform.position = m_center.position;
			stageLane.Initialize(dire);
			m_stageLanes.Add(stageLane);
		}
	}
}
