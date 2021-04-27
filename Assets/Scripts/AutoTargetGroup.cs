using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class AutoTargetGroup : MonoBehaviour
{
    private CinemachineTargetGroup _cmTargetGroup;
    public float Radius = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        _cmTargetGroup = GetComponent<CinemachineTargetGroup>();
        while (_cmTargetGroup.m_Targets.Length > 0)
        {
            _cmTargetGroup.RemoveMember(_cmTargetGroup.m_Targets[0].target);
        }

        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (var c in cells)
        {
            _cmTargetGroup.AddMember(c.transform, 1, Radius);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
