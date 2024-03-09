using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installation : MonoBehaviour
{
    [SerializeField] protected int areaWidth;
    [SerializeField] protected int areaHeight;

    public int AreaWidth { get => areaWidth; }
    public int AreaHeight { get => areaHeight; }

    public float maxHp;

    protected float currentHp;
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }

    [SerializeField]
    protected Canvas canvas;
    [SerializeField]
    protected HpBar hpBar;
    public ProgressBar progressBar;

    public enum Type
    {
        Tower,
        TowerBeingBuilt,
        Field,
        Wall
    }
    public Type type;

    [SerializeField]
    protected List<WorkerUnit> workers = new List<WorkerUnit>();
    public List<WorkerUnit> Workers { get; }

    protected virtual void Awake()
    {
        //canvas = GetComponentInChildren<Canvas>();
        //hpBar = canvas.GetComponentInChildren<HpBar>();
    }

    public void GetDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Destroyed();
        }

        hpBar.SetHpBar(currentHp, maxHp);
    }

    public void Destroyed()
    {
        //배치되어 있던 일꾼 모두 해제
        /*foreach (WorkerUnit worker in workers)
        {
            worker.Decollocate();
        }*/

        Debug.Log(gameObject.GetInstanceID());
        //�μ����� �ִϸ��̼�
        GameDB.Instance.tower_Player.Remove(transform);
        Lean.Pool.LeanPool.Despawn(gameObject);
        UI_PanelManager.Instance.PanelReSet();
    }

    public virtual void CollocateWorker(WorkerUnit worker)
    {
        workers.Add(worker);
    }

    public virtual void DecollocateWorker(WorkerUnit worker)
    {
        workers.Remove(worker);
    }
}
