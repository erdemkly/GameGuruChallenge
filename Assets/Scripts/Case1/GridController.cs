using System;
using System.Collections.Generic;
using System.Linq;
using Helper;
using UnityEngine;
public class GridController : MonoSingleton<GridController>
{
    private List<GameObject> _objectPool;
    [SerializeField] private GameObject gridElementPrefab;
    private GridElement[,] _allGridElements;
    public int n;

    private void Start()
    {
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        CameraController.Instance.SetOrthographicSize(n);
        DisablePoolObjects();
        _allGridElements = new GridElement[n, n];
        var pos = Vector2.zero;

        //Set controller pivot 
        var offset = (float) n / 2 - 0.5f;
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var gridElementObject = _objectPool.FirstOrDefault(x => !x.activeSelf);
                if (gridElementObject != null)
                {
                    gridElementObject.transform.position = new Vector3(i - offset, j - offset);
                    gridElementObject.SetActive(true);
                }
                else
                {
                    gridElementObject = Instantiate(gridElementPrefab, new Vector3(i - offset, j - offset), Quaternion.identity, transform);
                    _objectPool.Add(gridElementObject);
                }
                var gridElement = gridElementObject.GetComponent<GridElement>();
                _allGridElements[i, j] = gridElement;
                gridElement.x = i;
                gridElement.y = j;
            }
        }
    }
    private void DisablePoolObjects()
    {
        _objectPool ??= new List<GameObject>();
        for (var index = 0; index < _objectPool.Count; index++)
        {
            var obj = _objectPool[index];
            if (obj == null)
            {
                _objectPool.RemoveAt(index);
                index--;
                continue;
            }
            obj.SetActive(false);
        }
    }

    private static readonly Vector2Int[] NeighborIndexes =
    {
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1)
    };

    public List<GridElement> CheckNeighborhood(int x, int y,List<GridElement> oldNeighbors=null)
    {
        oldNeighbors ??= new List<GridElement>();
        foreach (var index in NeighborIndexes)
        {
            //Check neighbor coordinates are out of the bound of the array
            if (x + index.x >= n || x + index.x < 0 || y + index.y >= n || y + index.y < 0) continue;
            var neighbor = _allGridElements[x + index.x, y + index.y];
            if (neighbor == null || !neighbor.isActive || oldNeighbors.Contains(neighbor)) continue;
            oldNeighbors.Add(neighbor);
            CheckNeighborhood(neighbor.x,neighbor.y,oldNeighbors);
        }
        return oldNeighbors;

    }
}
