using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLane : MonoBehaviour {
	[SerializeField]
	GameObject m_firstBlockPrefab, m_blockPrefab;
	public List<GameObject> m_blocks = new List<GameObject>();
	public int m_num = 9;
	public float m_offset, m_interval;
	public List<EnemyGhost> m_ghosts = new List<EnemyGhost>();
	public void Initialize(Vector3 direction) {
		transform.localRotation = Quaternion.LookRotation(
			direction, Vector3.up);

		foreach (var block in m_blocks) { DestroyImmediate(block.gameObject); }
		m_blocks.Clear();
		for (int i = 0; i < m_num; i++) {
			var block = i == 0 ?
				Instantiate(m_firstBlockPrefab) :
				Instantiate(m_blockPrefab);
			block.transform.parent = transform;
			block.transform.localPosition = Vector3.forward *
				(m_offset + m_interval * i);
			block.transform.localRotation = Quaternion.identity;
			m_blocks.Add(block);
		}
	}

	public EnemyGhost GetFirstGhost() {
		foreach (var ghost in m_ghosts) {
			if (ghost.m_blockPosition == 0) return ghost;
		}
		return null;
	}
}
