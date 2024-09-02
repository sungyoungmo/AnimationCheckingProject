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
    public Dictionary<BuffOrDebuffType, BuffBase> buffDic = new();

    private void Awake()
    {
        instance = this;

        foreach (var buff in buffList)
        {
            buffDic.Add(buff._BuffOrDebuffType, buff);
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

    //��ư���� ȣ���� ���� ����� �Լ�
    public void AddBuffTest()
    {
        AddBuff(BuffOrDebuffType.AttackDamage, testList);
        AddBuff(BuffOrDebuffType.MoveSpeed, testList);
    }

    // dictionary���� ������ �ִ� �� ã�� �Լ�
    private BuffBase BuffListSearching(BuffOrDebuffType buffType)
    {
        if (buffDic.TryGetValue(buffType, out BuffBase buff))
        {
            return buff;
        }
        else
        {
            Debug.Log("���� ����");
            return null;
        }
    }

    public void AddBuff(BuffOrDebuffType buffType, List<PlayerStatus> playerList)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����
        //buffs |= (byte)(1 << (int)buffType); // �ش� ��Ʈ�� 1�� ����
        BuffBase targetBuff = null;

        targetBuff = BuffListSearching(buffType);
        

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
                    //print($"���� ���� Ÿ��: {targetBuff._BuffOrDebuffType} �÷��̾� ���� ����: {HasBuff(buffType, player)}");

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

        targetBuff = BuffListSearching(buffType);

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
    /// �÷��̾� ������ ���� ������ �� ����Ϸ��� �����ε��� �Լ�(�׾��� ������ �Ұ� ����� ��ȹ)
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="player"></param>
    public void RemoveBuff(BuffOrDebuffType buffType, PlayerStatus player)
    {
        BuffBase targetBuff = null;

        targetBuff = BuffListSearching(buffType);

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


    /// <summary>
    /// �ش� �÷��̾ ������ ���� �ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="_player"></param>
    /// <returns></returns>
    bool HasBuff(BuffOrDebuffType buffType, PlayerStatus _player)
    {
        //ĳ������ ���� ����Ʈ�� �ǰ� ���� ¥�� ����

        // 0000 0001�� 3�̸� 3ĭ �о� 0000 1000���� ����� ���� �Ѵ� 1�̾�� 1�� 
        return (_player.buffs_Or_Debuffs & (1 << (int)buffType)) != 0; // �ش� ��Ʈ�� 1���� Ȯ��
    }

    
}
