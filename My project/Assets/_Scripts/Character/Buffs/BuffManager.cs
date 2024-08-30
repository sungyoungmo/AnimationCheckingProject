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
    /// �÷��̾ ���ӿ� ����� �� ȣ���ؼ� ����Ʈ�� �߰�
    /// </summary>
    /// <param name="_player"></param>
    public void AddPlayerList(PlayerController _player)
    {
        
        
    }


    public void AddBuff(BuffType buffType)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����
        //buffs |= (byte)(1 << (int)buffType); // �ش� ��Ʈ�� 1�� ����
    }

    // ���� ����
    public void RemoveBuff(BuffType buffType)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����
        //buffs &= (byte)~(1 << (int)buffType); // �ش� ��Ʈ�� 0���� ����
    }

    // ���� Ȯ��
    public bool HasBuff(BuffType buffType)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����
        //return (buffs & (1 << (int)buffType)) != 0; // �ش� ��Ʈ�� 1���� Ȯ��
        return false;
    }

}
