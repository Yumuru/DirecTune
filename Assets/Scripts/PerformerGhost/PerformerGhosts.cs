using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformerGhosts : MonoBehaviour {
    public GameObject m_rangesPerformer;

    public List<PerformerGhost> m_performerGhosts = new List<PerformerGhost>();

    private void Start() {
        var positions = m_rangesPerformer
            .GetComponentsInChildren<Transform>()
            .OrderBy(v => Guid.NewGuid())
            .ToList();
    }
}
