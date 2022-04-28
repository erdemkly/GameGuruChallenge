using System;
using DG.Tweening;
using Helper;
using NaughtyAttributes;
using UnityEngine;

namespace Case2.Managers
{
    public class BlockManager : MonoSingleton<BlockManager>
    {
        [SerializeField] private Material[] stackColors;
        private float _oldScaleX;
        private GameObject _selectedBlock;
        private float _oldPosX = 0;
        private int _count;
        private int _combo;
        
        private const float PerfectTreshold = 0.1f,ScaleZ=3,ScaleX=2;
        private void Start()
        {
            _oldScaleX = ScaleX;
        }
        [Button()]
        public void NewBlock()
        {
            _count++;
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //Set color
            cube.GetComponent<Renderer>().material = stackColors[(_count-1) % stackColors.Length];
            cube.transform.parent = transform;
            
            cube.transform.localScale = new Vector3(_oldScaleX, 1, ScaleZ);
            var pos = cube.transform.position;
            pos.x = RandomItemGeneric<int>.GetRandom(-3,3);
            pos.z = _count * ScaleZ;
            cube.transform.position = pos;
            _selectedBlock = cube;
            
            //Set left right animation
            _selectedBlock.transform.DOMoveX(pos.x*-1, 2f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .SetId("block");
        }
        [Button()]
        public void CutBlock()
        {
            DOTween.Kill("block");
            var delta = _selectedBlock.transform.position.x - _oldPosX;
            var sign = Mathf.Sign(delta);
            delta = Mathf.Abs(delta);
            
            if (delta > _oldScaleX)
            {
                //Lose
                _selectedBlock.AddComponent<Rigidbody>();
                return;
            }
            if (delta <= PerfectTreshold)
            {
                //Perfect
                _selectedBlock.transform.position = _selectedBlock.transform.position.SetX(_oldPosX);
                _combo++;
                AudioManager.Instance.Success(_combo*0.1f);
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
            var fallingObj = Instantiate(_selectedBlock);

            var fallingScale = fallingObj.transform.localScale;
            fallingScale.x = _oldScaleX - fixedScale.x;
            fallingObj.transform.localScale = fallingScale;

            var fallingPos = fallingObj.transform.position;
            fallingPos.x = fixedPos.x + _oldScaleX / 2 * sign;
            fallingObj.transform.position = fallingPos;
            
            fallingObj.AddComponent<Rigidbody>();
            
            _oldPosX += delta/2*sign;
            _oldScaleX -= delta;
            NewBlock();

        }
    }
}
