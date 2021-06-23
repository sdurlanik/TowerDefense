using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;



    [HideInInspector] 
    public GameObject turret;

    [HideInInspector] 
    public TurretBlueprint turretBlueprint;

    [HideInInspector] 
    public bool isUpgraded = false;
    
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
 
        
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        
       BuildTurret(buildManager.GetTurretToBuild());

    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject) Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        //Eski kuleyi sil
        Destroy(turret);
        
        // Yeni kuleyi ekle
        GameObject _turret = (GameObject) Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
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

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
            
        }

    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
