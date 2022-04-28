using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Helper
{
    public static class EHelper
    {
        public static Vector3 ClampVector3(this Vector3 position, Vector3 min, Vector3 max)
        {
            position.x = Mathf.Clamp(position.x, min.x, max.x);
            position.y = Mathf.Clamp(position.y, min.y, max.y);
            position.z = Mathf.Clamp(position.z, min.z, max.z);

            return position;
        } 
        public static Vector2 ClampVector2(this Vector2 position, Vector2 min, Vector2 max)
        {
            position.x = Mathf.Clamp(position.x, min.x, max.x);
            position.y = Mathf.Clamp(position.y, min.y, max.y);

            return position;
        }
        public static float GetDirection(this Vector2 vector,Vector2 direction)
        {
            vector.x *= direction.x;
            vector.y *= direction.y;
            return vector.x+vector.y;
        }

        /// <summary>
        /// return (v.x,v.y,0)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 ToVector3Z0(this Vector2 v)
        {
            return new Vector3(v.x, v.y, 0);
        }
        
        /// <summary>
        /// return (v.x,0,v.y)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 ToVector3Y0(this Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }
        
        /// <summary>
        /// return (0,v.x,v.y)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 ToVector3X0(this Vector2 v)
        {
            return new Vector3(0, v.x, v.y);
        }

        /// <summary>
        /// return ({x},v.y,v.z)
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 SetX(this Vector3 v,float x)
        {
            return new Vector3(x, v.y, v.z);
        }
        /// <summary>
        /// return (v.x,{y},v.z)
        /// </summary>
        /// <param name="v"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector3 SetY(this Vector3 v,float y)
        {
            return new Vector3(v.x, y, v.z);
        }
        /// <summary>
        /// return (v.x,v.y,{z})
        /// </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 SetZ(this Vector3 v,float z)
        {
            return new Vector3(v.x, v.y, z);
        }
        
        public static Vector3 ToInt(this Vector3 v)
        {
            return new Vector3((int)v.x, (int)v.y, (int)v.z);
        } 
        public static Vector2 ToInt(this Vector2 v)
        {
            return new Vector2((int)v.x, (int)v.y);
        }

        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
        {
            var randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
            NavMesh.SamplePosition(randDirection, out var navHit, dist, layerMask);
            return navHit.position;
        }
        public static int LayerMaskToLayer(this LayerMask layerMask)
        {
            var layerNumber = 0;
            var layer = layerMask.value;
            while (layer > 0)
            {
                layer = layer >> 1;
                layerNumber++;
            }
            return layerNumber - 1;
        }
        public static float Clamp0360(this float eulerAngles)
        {
            var result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
            
            if (result < 0)
            {
                result += 180f;
            }
            return result;
        }


    }
    public static class RandomItemGeneric<T>
    {
        public static T GetRandom(params T[] array)
        {
            var rand = Random.Range(0, array.Length);
            return array[rand];
        }

        public static T[] GetRandomMultiple(int count, bool canRepeat = true, params T[] array)
        {
            var result = new List<T>();
            count = Mathf.Clamp(count, 0, array.Length);
            for (int i = 0; i < count; i++)
            {
                T randItem;
                do
                {
                    randItem = GetRandom(array);
                }
                while (!canRepeat && result.Contains(randItem));

                result.Add(randItem);
            }
            return result.ToArray();
        }
        public static T[] GetMixedArray(params T[] array)
        {
            var result = array;
            for (int i = 0; i < array.Length; i++)
            {
                var rand = Random.Range(0, result.Length);
                var tmp = result[i];
                result[i] = result[rand];
                result[rand] = tmp;
            }
            return result;
        }
    }
}
