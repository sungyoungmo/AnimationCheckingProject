using UnityEngine;
public interface Ihittable
{
    /// <summary>
    /// 몬스터 및 플레이어 용 Hit 
    /// <br /><br />
    /// 데미지와 때리는 플레이어를 파라미터로 넣어서
    /// hit 시에 때린 플레이어를 돌아보게 하는 함수
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackPlayer"></param>
    void Hit(int damage, Iattackable attackPlayer);

    /// <summary>
    /// 포탑 용 Hit 
    /// <br /><br />
    /// 데미지를 받아와 데미지를 입는 함수
    /// </summary>
    /// <param name="damage"></param>
    void Hit(int damage);
}