using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class UI_Enemy_Index : MonoBehaviour
{
    public List<TextMeshProUGUI>        enemyIndex ;
    float                               speed =50f;

    private void Awake()
    {
        
    }
    private void Update()
    {
        foreach (var enemy in enemyIndex)
        {

            Vector3 newPos = enemy.rectTransform.position + Vector3.right * speed * Time.deltaTime;
            enemy.rectTransform.position = newPos;
            if(enemy.rectTransform.position.x > 2200f)
            {
                enemy.rectTransform.position = new Vector3(enemy.rectTransform.position.x - 2200f, enemy.rectTransform.position.y, enemy.rectTransform.position.z);
            }
        }
    }
}
