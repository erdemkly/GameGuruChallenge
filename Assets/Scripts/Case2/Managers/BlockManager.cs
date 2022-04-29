using System;
using System.Collections.Generic;
using Assets.Scripts.Case2.Other;
using DG.Tweening;
using Helper;
using NaughtyAttributes;
using UnityEngine;

namespace Case2.Managers
{
    public class BlockManager : MonoSingleton<BlockManager>
    {
        [SerializeField] private GameObject starPrefab;
        [SerializeField] private Material[] stackColors;
        private float _oldScaleX;
        private GameObject _selectedBlock;
        private float _oldPosX = 0;
        public int count;
        private int _combo;
        [SerializeField]private List<PoolObject> _objectPool;
        
        public const float PerfectTreshold = 0.1f,ScaleZ=3,ScaleX=2;
        private void Start()
        {
            ResetBlocks();
        }

        public void ResetBlocks(){
            _oldPosX = 0;
            _combo=0;
            _selectedBlock = null;
            _oldScaleX = ScaleX;
        }
        public void DropSelectedBlock(){
            if(_selectedBlock == null) return;
            DOTween.Kill("block");
            _selectedBlock.AddComponent<Rigidbody>();
            _selectedBlock = null;
        }
        public GameObject CreateBlock(float x){
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //Set color
            cube.GetComponent<Renderer>().material = stackColors[(count-1) % stackColors.Length];
            cube.transform.parent = transform;
            
            cube.transform.localScale = new Vector3(_oldScaleX, 1, ScaleZ);
            
            //Set pos
            var pos = new Vector3(
                x,
            0,
            count * ScaleZ + LevelManager.Instance.blockOffsetZ
            );
            cube.transform.position = pos;
            return cube;
        }
        [Button()]
        public void NewBlock()
        {
            if(count>=LevelManager.Instance.maxBlockCount){
                _selectedBlock=null;
                return;
            }
            count++;

            var posX = RandomItemGeneric<int>.GetRandom(-3,3);
            _selectedBlock = CreateBlock(posX);
            
            //Set left right animation
            _selectedBlock.transform.DOMoveX(posX*-1, 2f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .SetId("block");
        }
        [Button()]
        public void CutBlock()
        {
            if(_selectedBlock == null) return;
            DOTween.Kill("block");
            var delta = _selectedBlock.transform.position.x - _oldPosX;
            var sign = Mathf.Sign(delta);
            delta = Mathf.Abs(delta);
            
            if (delta > _oldScaleX)
            {
                //Lose
                DropSelectedBlock();
                return;
            }
            _selectedBlock.layer = 6;
            if (delta <= PerfectTreshold)
            {
                //Perfect
                _selectedBlock.transform.position = _selectedBlock.transform.position.SetX(_oldPosX);
                _combo++;
                AudioManager.Instance.Success(_combo*0.1f);

                //Summon collectable star
                var star = Instantiate(starPrefab,
                _selectedBlock.transform.position.SetY(0.5f),
                Quaternion.identity);

                //Set star animation
                star.transform.DOScale(Vector3.zero,0.2f).From(false);

                NewBlock();
                return;
            }
            _combo = 0;
            

            //Fixed part
            var fixedObj = _selectedBlock;

            var fixedScale = fixedObj.transform.localScale;
            fixedScale.x = _oldScaleX - delta;
            fixedObj.transform.localScale = fixedScale;
            
            var fixedPos = fixedObj.transform.position;
            fixedPos.x = _oldPosX + delta / 2 * sign;
            fixedObj.transform.position = fixedPos;

            
            //Falling part

            //Set falling object from pool.
            var fallingObj = _objectPool[count%_objectPool.Count];
            fallingObj.gameObject.SetActive(true);
            
            var fallingScale = fallingObj.transform.localScale;
            fallingScale.x = _oldScaleX - fixedScale.x;
            fallingScale.z = ScaleZ;

            var fallingPos = fixedPos;
            fallingPos.x = fixedPos.x + _oldScaleX / 2 * sign;


            fallingObj.SetPoolBlock(
            fallingPos,
            fallingScale,
            stackColors[(count-1) % stackColors.Length]
            );
            
            
            _oldPosX += delta/2*sign;
            _oldScaleX -= delta;
            NewBlock();

        }
    }
}
