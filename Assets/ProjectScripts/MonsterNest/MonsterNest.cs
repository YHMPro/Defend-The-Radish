using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace DTR.Monster
{
    /// <summary>
    /// 怪物窝(依据配置信息来对怪物生成进行控制)
    /// </summary>
    public class MonsterNest : MonoBehaviour
    {


        private void Start()
        {
           
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(MonsterProduct());
            }
        }


        IEnumerator MonsterProduct()
        {
            GameObject go;
            for (int i=0;i<10;i++)
            {
                if(!GoReusePool.Take("Sheep", out go))
                {
                    if(!GoLoad.Take("Prefabs/Monster/Sheep", out go))
                    {
                        continue;
                    }
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
