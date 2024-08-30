using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;

    public List<PlayerStatus> A_Team_players = new();
    public List<PlayerStatus> B_Team_players = new();

    public List<PlayerStatus> testList = new();

    public List<BuffBase> buffList;
    Dictionary<BuffBase, BuffOrDebuffType> buffOrDebuffDic = new();

    private void Awake()
    {
        instance = this;


        for (int i = 0; i < buffList.Count; i++)
        {
            buffOrDebuffDic.Add(buffList[i], buffList[i]._BuffOrDebuffType);
            print(buffOrDebuffDic.Count);
        }

    }

    /// <summary>
    /// ���� ��ư�� ���� �� Ŀ���� ������Ƽ�� �߰��ؼ� �ѱ��
    /// ������Ƽ�� ���⼭ �޾�
    /// �÷��̾ ���ӿ� ����� �� ȣ���ؼ� ����Ʈ�� �߰�
    /// </summary>
    /// <param name="_player"></param>
    public void AddPlayerList(PlayerStatus _player)
    {
        testList.Add(_player);
    }

    public void AddBuffTest()
    {
        AddBuff(BuffOrDebuffType.MoveSpeed, testList);
    }

    public void AddBuff(BuffOrDebuffType buffType, List<PlayerStatus> playerList)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����
        //buffs |= (byte)(1 << (int)buffType); // �ش� ��Ʈ�� 1�� ����
        BuffBase targetBuff = null;

        foreach (var buff in buffOrDebuffDic)
        {
            if (buff.Value == buffType)
            {
                targetBuff = buff.Key;
            }
        }

        foreach (var player in playerList)
        {
            if (!player.isDead)
            {
                if (targetBuff != null && !HasBuff(buffType,player))
                {
                    print("���������");
                    targetBuff.BuffOrDebuffEffect(player);

                    // 0000 0001�� 1�� 3�̸� 3ĭ �о� 0000 1000���� ����� �װ� ����Ʈ�� �ٲ㼭 
                    // �÷��̾� ������ ���� �� ĭ�� 0�̸� ����ִ´�
                    player.buffs_Or_Debuffs |= (byte)(1 << (int)buffType);
                }
                else
                {
                    print("�������� �ȵ�");
                }
            }
        }
    }

    /// <summary>
    /// ��ü�� ������ ������ �� ���
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="playerList"></param>
    public void RemoveBuff(BuffOrDebuffType buffType, List<PlayerStatus> playerList)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����
        BuffBase targetBuff = null;

        foreach (var buff in buffOrDebuffDic)
        {
            if (buff.Value == buffType)
            {
                targetBuff = buff.Key;
            }
        }

        foreach (var player in playerList)
        {
            if (!player.isDead)
            {
                if (targetBuff != null && HasBuff(buffType, player))
                {
                    print("���� ������");
                    targetBuff.BuffOrDebuffEffect(player);

                    // 0000 0001�� 3�̸� 0000 1000���� ����� ~�� ��� ������ �ؼ�
                    // 1111 0111�� ����� 0101 1000�̴� ����ĭ�� ���� �����ʿ��� 4��° ĭ�� �ִ� 
                    // 1�� 0���� ����������.
                    // ���� 0�̴� �κ��� 0�̴ϱ� �ٲ��� �ʰ� 1�̴� �κ��� ���ļ� 1�� ��ȯ�ϹǷ� �״�� ����ȴ�.
                    player.buffs_Or_Debuffs &= (byte)~(1 << (int)buffType); // �ش� ��Ʈ�� 0���� ����
                }
                else
                {
                    print("�������� �ȵ�");
                }
            }
        }
    }

    /// <summary>
    /// �÷��̾� ������ ���� ������ �� ���(�׾��� ������ �Ұ� ����� ��ȹ)
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="player"></param>
    public void RemoveBuff(BuffOrDebuffType buffType, PlayerStatus player)
    {
        BuffBase targetBuff = null;

        foreach (var buff in buffOrDebuffDic)
        {
            if (buff.Value == buffType)
            {
                targetBuff = buff.Key;
            }
        }

        if (targetBuff != null && HasBuff(buffType, player))
        {
            print("���� ������");
            targetBuff.BuffOrDebuffEffect(player);

            // 0000 0001�� 3�̸� 0000 1000���� ����� ~�� ��� ������ �ؼ�
            // 1111 0111�� ����� 0101 1000�̴� ����ĭ�� ���� �����ʿ��� 4��° ĭ�� �ִ� 
            // 1�� 0���� ����������.
            // ���� 0�̴� �κ��� 0�̴ϱ� �ٲ��� �ʰ� 1�̴� �κ��� ���ļ� 1�� ��ȯ�ϹǷ� �״�� ����ȴ�.
            player.buffs_Or_Debuffs &= (byte)~(1 << (int)buffType); // �ش� ��Ʈ�� 0���� ����
        }
        else
        {
            print("�������� �ȵ�");
        }

    }


    // ���� Ȯ��
    bool HasBuff(BuffOrDebuffType buffType, PlayerStatus _player)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����

        // 0000 0001�� 3�̸� 3ĭ �о� 0000 1000���� ����� ���� �Ѵ� 1�̾�� 1�� 
        return (_player.buffs_Or_Debuffs & (1 << (int)buffType)) != 0; // �ش� ��Ʈ�� 1���� Ȯ��
    }

    
}
