using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installation : MonoBehaviour
{
    [SerializeField] protected int areaWidth;
    [SerializeField] protected int areaHeight;

    public int AreaWidth { get => areaWidth; }
    public int AreaHeight { get => areaHeight; }


    protected float maxHp;
    public float MaxHp { get { return maxHp; } }

    protected float currentHp;
    public float CurrentHp { get { return currentHp; } }

    protected Canvas canvas;
    protected HpBar hpBar;

    public enum Type
    {
        Tower,
        TowerBeingBuilt,
        Field
    }
    public Type type;

    [SerializeField]
    protected List<WorkerUnit> workers = new List<WorkerUnit>();
    public List<WorkerUnit> Workers { get; }

    protected virtual void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        hpBar = canvas.GetComponentInChildren<HpBar>();
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
        //�μ����� �ִϸ��̼�
        Lean.Pool.LeanPool.Despawn(gameObject);
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
