/*
 * * * * * * * * * * * * * * * * 
 * Author:        ��Ԫ��
 * CreatTime:  2020/11/16 13��51 
 * Description:  ��ɫ������
 * * * * * * * * * * * * * * * * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYLDCharacterBulider
{
    protected ICharacter mCharacter;
    protected HeroName mHeroName;
    protected WeaponType mWeaponType;
    protected Vector3 mSpawnPosition;
    protected string mPrefabsName = "";
    public  HYLDCharacterBulider(ICharacter character,Vector3 spawnPosition, HeroName heroName,WeaponType weaponType)
    {
        mCharacter = character;
        mHeroName = heroName;
        mWeaponType = weaponType;
        mSpawnPosition = spawnPosition;
        
    }
    public void  AddCharacterBaseAttribute()
    {
        //������ɫ����
        CharacterBaseAttribute characterBaseAttribute = FactoryManager.AttributeFactory.GetCharacterBaseAttr(mHeroName);
        mPrefabsName = characterBaseAttribute.PrefabName;
        mCharacter.Attribute = characterBaseAttribute;
    }
    public void AddGameObect()
    {
        //������ɫ��Ϸ����
        //1������ 2��ʵ����
        GameObject hero = FactoryManager.ResourcesAssetFactory.LoadSoldier(mPrefabsName);
        hero.transform.position = mSpawnPosition;
        mCharacter.gameObject = hero;
    }
    public void AddWeapon()
    {
        IWeapon weapon = FactoryManager.WeaponFactory.CreateWeapon(mHeroName);
        mCharacter.Weapon = weapon;
    }
    public ICharacter GetResult()
    {
        return mCharacter;
    }

}
