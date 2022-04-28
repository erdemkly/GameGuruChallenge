using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    [SerializeField]private GameObject crossObj;
    public int x, y;
    public bool isActive;
    private void OnMouseDown()
    {
        if (isActive) return;
        SetCross(true);
        var neighbors=GridController.Instance.CheckNeighborhood(x,y);
        if (neighbors.Count < 3) return;
        foreach (var neighbor in neighbors)
        {
            neighbor.SetCross(false);
        }
        UIController.Instance.AddMatchCount();
    }

    private void SetCross(bool active)
    {
        crossObj.SetActive(active);
        isActive = active;
    }
}
