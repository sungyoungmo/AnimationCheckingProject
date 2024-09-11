using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharClass
{
    Samurai,
    Paladine
}

public class PlayerStatus : MonoBehaviour
{
    public PlayerStatus()
    {
        
    }

    private void Awake()
    {
        lastAtk = rootAtk;
        lastAtk_spd = atk_spd;
        lastSpd = rootSpd;
        lastDefArmor = rootDefArmor;
        currentHp = maxHp;
    }

    //  / 100 �ϰ� �ִµ� ���� �ؾ��ϳ�

    public int rootAtk; // ���� ���� ��, ���� ���׷��̵带 ���� ���ϴ� ���ݷ�   / 100
    public int lastAtk; // ���� ���� ��, ���������� ���� ���� ���ݷ�        / 100

    public float atk_spd;   // ���� �ӵ�(�ִϸ��̼ǿ� ���� ���� �ӵ� ����)
    public float lastAtk_spd;   // ���� ���� ��, ���������� ���� ���� ���� �ӵ�

    public float rootSpd;   // �̵� �ӵ�(�ִϸ��̼ǰ� ������ �̵��ӵ� ����)
    public float lastSpd;   // ���� ���� ��, ���������� ���� ���� �̵� �ӵ�

    public int rootDefArmor;    // hit �� ������ ������ ���� ���� ���
    public int lastDefArmor;    // ���� ���� ��, ���������� ���� ���� ����

    public int maxHp;    // ü�� / 100
    public int currentHp;   // ���� ü�� / 100
    public int hpGenerate;  // 5�ʴ� ü�� ȸ����

    public int level;   // ����
    public int exp; // ����ġ(���� �� �ÿ� ����Ʈ�� ����� ��ų ������ �� ��Ÿ�� ���׷��̵� ����)

    public bool isDead; // �÷��̾ �׾���(���� ���� ���� �Ǵܿ� ����� ����)

    public byte buffs_Or_Debuffs;  // �÷��̾ ���� ������

    public int gold;

    public CharClass playerClass;
}