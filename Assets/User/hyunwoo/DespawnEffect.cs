using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.VFX;

public class DespawnEffect : MonoBehaviour
{
   private VisualEffect vfx;

    private void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    private void Update()
        {
    //     // 'aliveParticleCount'를 체크하여 모든 파티클이 사라졌는지 확인
    //     if (vfx.aliveParticleCount !> 0)
    //     {
    //         LeanPool.Despawn(this); // 모든 파티클이 사라졌으면 게임 오브젝트 삭제
    //     }
    }
}
