using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{

    /*
        Todo List : 
        1. VFX 매니저 추가 수정 필요 ( 파티클이 실행되었을 때 방향, 크기 등 세분 조정 필요 )
        2. 게임 저장 및 불러오기 기능 필요
        3. 업적 클래스 다양하게 추가하고 모든 업적을 보여줄 수 있는 탭을 만들기. 업적이 달성 될 떄 마다 업데이트 해주는 팝업 창 만들기.
    */



    public static VFXManager Instance;
    public Transform shotpoint;
    public enum VFXDir { Explosion }
    
    public List<GameObject> visualEffects = new List<GameObject>();

    void Awake() {
        Instance = this;
    }

    public void VFXPlay(Transform trans, VFXDir key) {

        GameObject effectObject = visualEffects[(int)key];

        LeanPool.Spawn(effectObject, shotpoint.position, Quaternion.identity, null);
        VisualEffect effect = effectObject.GetComponent<VisualEffect>();

        effect.Play();
    }

}
