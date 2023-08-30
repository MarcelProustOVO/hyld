/*
 * * * * * * * * * * * * * * * * 
 * Author:        ��Ԫ��
 * CreatTime:  2020/11/16 13��51 
 * Description:  ��ɫ����ָ����
 * * * * * * * * * * * * * * * * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYLDCharacterBuilderDirector
{
    public static ICharacter Construct(HYLDCharacterBulider bulider)
    {
        bulider.AddCharacterBaseAttribute();//�������
        bulider.AddGameObect();//���Ӣ��
        bulider.AddWeapon();//�������

        return bulider.GetResult();
    }
}
