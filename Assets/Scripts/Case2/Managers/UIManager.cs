using System.Collections;
using System.Collections.Generic;
using Helper;
using TMPro;
using UnityEngine;

namespace Case2.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private TextMeshProUGUI txtLevelCount;

        public void SetLevelCount(string txt){
            txtLevelCount.text = txt;
        }
    }
}