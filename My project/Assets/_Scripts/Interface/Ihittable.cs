using UnityEngine;
public interface Ihittable
{
    /// <summary>
    /// ���� �� �÷��̾� �� Hit 
    /// <br /><br />
    /// �������� ������ �÷��̾ �Ķ���ͷ� �־
    /// hit �ÿ� ���� �������� �ٿ����� ���� �ϴ� �Լ�
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackPlayer"></param>
    void Hit_Call(int damage, Iattackable attackPlayer);
    // �׳� ����ü Ȥ�� ���Ÿ� ���� �� �Ѿ˿� Iattackable�� �޾Ƽ� ����ϴ� �� ���� ��


    /// <summary>
    /// ��ž �� Hit 
    /// <br /><br />
    /// �������� �޾ƿ� �������� �Դ� �Լ�
    /// </summary>
    /// <param name="damage"></param>
    //void Hit_Call(int damage);
}