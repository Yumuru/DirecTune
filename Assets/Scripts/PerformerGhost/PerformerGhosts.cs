using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PerformerGhosts : MonoBehaviour {
    public Point m_pointDrumGhosts;
    public Point m_pointClarinetGhosts;

    public List<PerformerGhost> m_performerGhosts = new List<PerformerGhost>();

    private void Start() {
        m_pointDrumGhosts.Start();
        m_pointClarinetGhosts.Start();
    }

    [Serializable]
    public class Point {
        public List<GameObject> m_points; 
        public GameObject m_prefab;
        public float m_sRateScore, m_eRateScore;

        public void Start() {
            var points = new LinkedList<Transform>(m_points
                .Select(g => g.transform)
                .OrderBy(i => Guid.NewGuid())
                .ToArray());

            var num = m_points.Count();
            var cNum = 0;
            var threshold = 1f / num;
            GameManager.GameScore
                .m_rateScore
                .Where(s => s >= m_sRateScore && s <= m_eRateScore)
                .Select(s => (s - m_sRateScore) / (m_eRateScore - m_sRateScore))
                .Subscribe(s => {
                    while (s > cNum * threshold) {
                        Pop(points.First());
                        points.RemoveFirst();
                        cNum++;
                    }
                });
        }

        void Pop(Transform transform) {
            var obj = Instantiate(m_prefab);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }
    }
}
