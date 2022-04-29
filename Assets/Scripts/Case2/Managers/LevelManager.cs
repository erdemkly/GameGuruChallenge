using System.Collections;
using System.Collections.Generic;
using Case2.Managers;
using Helper;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private AnimationCurve curve;
    private const float finishModelCap = 0.29417f;
    [SerializeField] private GameObject finishObject;
    private float finishOffsetZ=0;
    public int maxBlockCount;
    public float blockOffsetZ=finishModelCap;
    public void SetLevel(){
        var level = PlayerPrefs.GetInt("Level",0);
        UIManager.Instance.SetLevelCount($"Level {level+1}");
        maxBlockCount = (int)curve.Evaluate(level);
        //Create finish object
        var obj = Instantiate(
            finishObject,
            Vector3.forward*((maxBlockCount*BlockManager.ScaleZ+BlockManager.ScaleZ/2)+finishOffsetZ),
            Quaternion.identity
        );
        
    }
    public void NextLevel(){

        BlockManager.Instance.ResetBlocks();
        var offset = maxBlockCount*BlockManager.ScaleZ+BlockManager.ScaleZ/2+finishModelCap;
        finishOffsetZ += offset;
        blockOffsetZ +=offset;
        
        //Create next levels start block;
        BlockManager.Instance.count = 1;
        var startBlock = BlockManager.Instance.CreateBlock(0);
        startBlock.layer = 6;
        
        var level = PlayerPrefs.GetInt("Level",0);
        PlayerPrefs.SetInt("Level",level+1);

    }
}
