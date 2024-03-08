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
    public float CurrentHp { get { return currentHp; } }

    [SerializeField]
    protected Canvas canvas;
    [SerializeField]
    protected HpBar hpBar;

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
        Debug.Log(gameObject.GetInstanceID());
        //�μ����� �ִϸ��̼�
        GameDB.Instance.tower_Player.Remove(transform);
        Lean.Pool.LeanPool.Despawn(this);
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
