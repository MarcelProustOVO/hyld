using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZYKPoolManger))]
public class ZYKGame : LZJSingleModen<ZYKGame>
{
    ZYKPoolManger poolManger;
    /// <summary>
    ///     ���������������ʵ������Prefabs������
    ///     poolManger.InstantiateObject();
    //      poolManger.DesteryObject();
    /// </summary>

    void Start()
    {
        poolManger = ZYKPoolManger.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //poolManger.InstantiateObject("Bullet", gameObject.transform);
    }
}
