using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;

    public List<PlayerController> A_Team_players = new();
    public List<PlayerController> B_Team_players = new();


    private void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// 플레이어가 게임에 연결될 때 호출해서 리스트에 추가
    /// </summary>
    /// <param name="_player"></param>
    public void AddPlayerList(PlayerController _player)
    {
        
        
    }


    public void AddBuff(BuffType buffType)
    {
        //캐릭터의 버프 바이트가 되고 팀을 짜서 연결
        //buffs |= (byte)(1 << (int)buffType); // 해당 비트를 1로 설정
    }

    // 버프 제거
    public void RemoveBuff(BuffType buffType)
    {
        //캐릭터의 버프 바이트가 되고 팀을 짜서 연결
        //buffs &= (byte)~(1 << (int)buffType); // 해당 비트를 0으로 설정
    }

    // 버프 확인
    public bool HasBuff(BuffType buffType)
    {
        //캐릭터의 버프 바이트가 되고 팀을 짜서 연결
        //return (buffs & (1 << (int)buffType)) != 0; // 해당 비트가 1인지 확인
        return false;
    }

}
