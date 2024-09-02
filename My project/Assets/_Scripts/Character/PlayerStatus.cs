using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int rootAtk; // ���� ���� ��, ���� ���׷��̵带 ���� ���ϴ� ���ݷ�   / 100
    public int lastAtk; // ���� ���� ��, ���������� ���� ���� ���ݷ�        /100

    public float atk_spd;   // ���� �ӵ�(�ִϸ��̼ǿ� ���� ���� �ӵ� ����)
    public float lastAtk_spd;   // ���� ���� ��, ���������� ���� ���� ���� �ӵ�

    public float rootSpd;   // �̵� �ӵ�(�ִϸ��̼ǰ� ������ �̵��ӵ� ����)
    public float lastSpd;   // ���� ���� ��, ���������� ���� ���� �̵� �ӵ�

    public int rootDefArmor;    // hit �� ������ ������ ���� ���� ���
    public int lastDefArmor;    // ���� ���� ��, ���������� ���� ���� ����

    public int maxHp;    // ü�� / 100
    public int currentHp;   // ���� ü�� / 100
    public int hpGenerate;  // 5�ʴ� ü�� ȸ����

    public int exp; // ����ġ(���� �� �ÿ� ����Ʈ�� ����� ��ų ������ �� ��Ÿ�� ���׷��̵� ����)

    public bool isDead; // �÷��̾ �׾���(���� ���� ���� �Ǵܿ� ����� ����)

    public byte buffs_Or_Debuffs;  // �÷��̾ ���� ������
}