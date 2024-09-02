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
    /// 시작 버튼을 누를 때 커스텀 프로퍼티에 추가해서 넘기고
    /// 프로퍼티를 여기서 받아
    /// 플레이어가 게임에 연결될 때 호출해서 리스트에 추가
    /// </summary>
    /// <param name="_player"></param>
    public void AddPlayerList(PlayerStatus _player)
    {
        testList.Add(_player);
    }

    //버튼에서 호출할 적용 시험용 함수
    public void AddBuffTest()
    {
        AddBuff(BuffOrDebuffType.AttackDamage, testList);
        AddBuff(BuffOrDebuffType.MoveSpeed, testList);
    }

    // dictionary에서 버프가 있는 지 찾는 함수
    private BuffBase BuffListSearching(BuffOrDebuffType buffType)
    {
        if (buffDic.TryGetValue(buffType, out BuffBase buff))
        {
            return buff;
        }
        else
        {
            Debug.Log("버프 없음");
            return null;
        }
    }

    public void AddBuff(BuffOrDebuffType buffType, List<PlayerStatus> playerList)
    {
        //캐릭터의 버프 바이트가 되고 팀을 짜서 연결
        //buffs |= (byte)(1 << (int)buffType); // 해당 비트를 1로 설정
        BuffBase targetBuff = null;

        targetBuff = BuffListSearching(buffType);
        

        foreach (var player in playerList)
        {
            if (!player.isDead)
            {
                if (targetBuff != null && !HasBuff(buffType,player))
                {
                    print("버프적용됨");
                    targetBuff.BuffOrDebuffEffect(player);

                    // 0000 0001인 1을 3이면 3칸 밀어 0000 1000으로 만들고 그걸 바이트로 바꿔서 
                    // 플레이어 버프와 비교해 그 칸이 0이면 집어넣는다
                    player.buffs_Or_Debuffs |= (byte)(1 << (int)buffType);
                }
                else
                {
                    //print($"버프 적용 타입: {targetBuff._BuffOrDebuffType} 플레이어 버프 유무: {HasBuff(buffType, player)}");

                    print("버프적용 안됨");
                }
            }
        }
    }

    /// <summary>
    /// 단체의 버프를 해제할 때 사용
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="playerList"></param>
    public void RemoveBuff(BuffOrDebuffType buffType, List<PlayerStatus> playerList)
    {
        //캐릭터의 버프 바이트가 되고 팀을 짜서 연결
        BuffBase targetBuff = null;

        targetBuff = BuffListSearching(buffType);

        foreach (var player in playerList)
        {
            if (!player.isDead)
            {
                if (targetBuff != null && HasBuff(buffType, player))
                {
                    print("버프 해제됨");
                    targetBuff.BuffOrDebuffEffect(player);

                    // 0000 0001을 3이면 0000 1000으로 만들고 ~는 모두 뒤집기 해서
                    // 1111 0111을 만들고 0101 1000이던 버프칸과 비교해 오른쪽에서 4번째 칸에 있는 
                    // 1을 0으로 만들어버린다.
                    // 원래 0이던 부분은 0이니까 바뀌지 않고 1이던 부분은 겹쳐서 1을 반환하므로 그대로 적용된다.
                    player.buffs_Or_Debuffs &= (byte)~(1 << (int)buffType); // 해당 비트를 0으로 설정
                }
                else
                {
                    print("버프해제 안됨");
                }
            }
        }
    }

    /// <summary>
    /// 플레이어 단일의 버프 해제할 때 사용하려고 오버로드한 함수(죽었을 버프를 잃게 사용할 계획)
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="player"></param>
    public void RemoveBuff(BuffOrDebuffType buffType, PlayerStatus player)
    {
        BuffBase targetBuff = null;

        targetBuff = BuffListSearching(buffType);

        if (targetBuff != null && HasBuff(buffType, player))
        {
            print("버프 해제됨");
            targetBuff.BuffOrDebuffEffect(player);

            // 0000 0001을 3이면 0000 1000으로 만들고 ~는 모두 뒤집기 해서
            // 1111 0111을 만들고 0101 1000이던 버프칸과 비교해 오른쪽에서 4번째 칸에 있는 
            // 1을 0으로 만들어버린다.
            // 원래 0이던 부분은 0이니까 바뀌지 않고 1이던 부분은 겹쳐서 1을 반환하므로 그대로 적용된다.
            player.buffs_Or_Debuffs &= (byte)~(1 << (int)buffType); // 해당 비트를 0으로 설정
        }
        else
        {
            print("버프해제 안됨");
        }

    }


    /// <summary>
    /// 해당 플레이어가 버프를 갖고 있는지 확인하는 함수
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="_player"></param>
    /// <returns></returns>
    bool HasBuff(BuffOrDebuffType buffType, PlayerStatus _player)
    {
        //캐릭터의 버프 바이트가 되고 팀을 짜서 연결

        // 0000 0001을 3이면 3칸 밀어 0000 1000으로 만들고 비교해 둘다 1이어야 1을 
        return (_player.buffs_Or_Debuffs & (1 << (int)buffType)) != 0; // 해당 비트가 1인지 확인
    }

    
}
