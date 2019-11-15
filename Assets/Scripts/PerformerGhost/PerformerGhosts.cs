using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PerformerGhosts : MonoBehaviour {
    public Point[] m_points;

    private void Start() {
        foreach (var point in m_points) {
            point.Start();
        }
    }

    [Serializable]
    public class Point {
        public GameObject m_LootPoints; 
        public GameObject m_prefab;
        public float m_sRateScore, m_eRateScore;

        public void Start() {
            var points = new LinkedList<Transform>(
                m_LootPoints.GetComponentsInChildren<Transform>()
                .Skip(1)
                .OrderBy(i => Guid.NewGuid())
                .ToArray());

            var num = points.Count();
            var cNum = 0;
            var threshold = 1f / num;
            Action<float> pop = s => {
                while (s >= cNum * threshold) {
                    if (cNum >= num) return;
                    Pop(points.First());
                    points.RemoveFirst();
                    cNum++;
                }
            };
            if (m_sRateScore == 0f && m_eRateScore == 0f) {
                pop(1f);
            } else {
                GameManager.Ins.m_gameScore
                    .m_score
                    .Select(p => p.m_rate)
                    .Where(s => s >= m_sRateScore && s <= m_eRateScore)
                    .Select(s => (s - m_sRateScore) / (m_eRateScore - m_sRateScore))
                    .Subscribe(pop);
            }
        }

        void Pop(Transform transform) {
            var obj = Instantiate(m_prefab);
            obj.transform.parent = transform;
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.transform.localScale = Vector3.one;
			GameManager.Ins.m_onEnd
				.Take(1)
				.Subscribe(_ => {
				Destroy(obj);
			});
        }
    }
}
