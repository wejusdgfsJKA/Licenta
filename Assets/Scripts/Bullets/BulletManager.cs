using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; protected set; }
    Pool<Bullet> pool = new();
    [SerializeField] Bullet prefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public Bullet GetBullet(Transform parent)
    {
        var b = pool.Get();
        if (b == null)
        {
            b = Instantiate(prefab);
        }
        b.Parent = parent;
        b.transform.position = parent.position;
        b.transform.rotation = parent.rotation;
        b.gameObject.SetActive(true);
        return b;
    }
    public void AddBulletToPool(Bullet bullet)
    {
        pool.Release(bullet);
    }
}
