using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    
    
    [Header("Fonksiyonel")]

    public GameObject turret;
    
    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;
    
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
        
        if (turret != null)
        {
            Debug.Log("Yerleştirilemez");
            return;
        }
        
        buildManager.BuildTurretOn(this);

    }


    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (!buildManager.CanBuild)
        {
            return;
        }
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
