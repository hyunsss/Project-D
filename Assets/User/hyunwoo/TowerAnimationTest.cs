using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerAnimationTest : MonoBehaviour
{
    public Button ActiveButton;
    public Button BrokenButton;
    public Button IdleButton;

    public List<Animator> animators = new List<Animator>();


    void Start() {
        BrokenButton.onClick.AddListener(() => SetAnimValue(2));
        ActiveButton.onClick.AddListener(() => SetAnimValue(1));
        IdleButton.onClick.AddListener(() => SetAnimValue(0));
    }

    private void SetAnimValue(int value)
    {
        foreach (var anim in animators)
        {
            anim.SetInteger("State", value);
        }
    }
    
}
