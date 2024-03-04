using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{   
    #region Test Buttons
    public Transform vfxpoint;

    public Button vfx1;
    public Button vfx2;
    public Button vfx3;
    public Button vfx4;
    public Button vfx5;
    public Button vfx6;

    public void SetVFXButtons() {
        vfx1.onClick.AddListener(() => VFXPlay(vfxpoint, VFXDir.Electricity));
        vfx2.onClick.AddListener(() => VFXPlay(vfxpoint, VFXDir.Explosion));
        vfx3.onClick.AddListener(() => VFXPlay(vfxpoint, VFXDir.Heal));
        vfx4.onClick.AddListener(() => VFXPlay(vfxpoint, VFXDir.Shield_VFX));
        vfx5.onClick.AddListener(() => VFXPlay(vfxpoint, VFXDir.Spark));
        vfx6.onClick.AddListener(() => VFXPlay(vfxpoint, VFXDir.SwordSlash));
    }

    #endregion
    /*
        Todo List : 
        1. VFX 매니저 추가 수정 필요 ( 파티클이 실행되었을 때 방향, 크기 등 세분 조정 필요 )
        3. 업적 클래스 다양하게 추가하고 모든 업적을 보여줄 수 있는 탭을 만들기. 업적이 달성 될 떄 마다 업데이트 해주는 팝업 창 만들기.
        4. 메인메뉴 버튼들 상호작용 적용하기 
        5. 기획서 제작하기
    */
    


    public static VFXManager Instance;
    public Transform shotpoint;
    public enum VFXDir { Electricity, Explosion, Heal, Shield_VFX, Spark, SwordSlash}
    
    public List<GameObject> visualEffects = new List<GameObject>();

    void Awake() {
        Instance = this;
        SetVFXButtons();
    }

    public void VFXPlay(Transform trans, VFXDir key) {

        GameObject effectObject = visualEffects[(int)key];

        LeanPool.Spawn(effectObject, shotpoint.position, Quaternion.identity, null);
        VisualEffect effect = effectObject.GetComponent<VisualEffect>();

        effect.Play();
    }

}
