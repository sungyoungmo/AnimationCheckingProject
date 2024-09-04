using UnityEngine;
public interface Ihittable
{
    /// <summary>
    /// ���� �� �÷��̾� �� Hit 
    /// <br /><br />
    /// �������� ������ �÷��̾ �Ķ���ͷ� �־
    /// hit �ÿ� ���� �÷��̾ ���ƺ��� �ϴ� �Լ�
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackPlayer"></param>
    void Hit(int damage, Iattackable attackPlayer);

    /// <summary>
    /// ��ž �� Hit 
    /// <br /><br />
    /// �������� �޾ƿ� �������� �Դ� �Լ�
    /// </summary>
    /// <param name="damage"></param>
    void Hit(int damage);
}