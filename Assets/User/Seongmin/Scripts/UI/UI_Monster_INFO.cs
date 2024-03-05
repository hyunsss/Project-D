using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UI_Monster_INFO : MonoBehaviour
{
    public TextMeshProUGUI damage       =null;
    public TextMeshProUGUI HP           = null;
    public TextMeshProUGUI speed        = null;
    public TextMeshProUGUI monsterName  = null;

    public void SetINFO(Monster monster)
    {
        damage.text = monster.MonsterData.MonsterDamage.ToString();
        HP.text = monster.MonsterData.MonsterHp.ToString();
        speed.text = monster.MonsterData.MonsterSpeed.ToString();
        monsterName.text = monster.MonsterData.MonsterName.ToString();
    }
}
