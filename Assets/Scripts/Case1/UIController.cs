using System;
using System.Collections;
using System.Collections.Generic;
using Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController>
{
    [SerializeField] private TextMeshProUGUI txtMatchCount;
    [SerializeField] private Button btnGenerateGrid;
    [SerializeField] private TMP_InputField inpGridSize;
    private int _matchCount;

    private void Start()
    {
        btnGenerateGrid.onClick.AddListener(() => {
            GridController.Instance.n = Convert.ToInt32(inpGridSize.text);
            GridController.Instance.GenerateGrid();
        });
    }
    public void AddMatchCount()
    {
        _matchCount++;
        txtMatchCount.text = $"{_matchCount}";
    }
}
