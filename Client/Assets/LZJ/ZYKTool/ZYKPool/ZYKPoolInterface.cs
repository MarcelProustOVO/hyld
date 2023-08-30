using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZYKTool.Pool
{
    public interface ZYKPoolInterface
    {

        void OnInstantiateObject();

        void OnDesteryObject();

    }
    public interface IReusable
    {

        //ȡ��ʱ�����
        void OnSpawn();

        //���յ���
        void OnUnSpawn();

    }
}