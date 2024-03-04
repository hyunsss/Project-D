using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneAnimation : MonoBehaviour
{

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Idle", true);
    }
}
