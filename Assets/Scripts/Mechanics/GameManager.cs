using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Pool<Bullet> pool = new();
    [SerializeField] Bullet prefab;
    [SerializeField] Transform playerPrefab, aiPrefab;
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] TextMeshPro gameOverText;
    public static GameManager Instance { get; private set; }
    [field: SerializeField] public List<Transform> Stars { get; protected set; } = new();
    public HashSet<Transform> ships { get; protected set; } = new();
    [SerializeField] int nrOfTeams = 2;
    [SerializeField] List<Transform> spawns = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        SpawnShips();
    }
    void SpawnShips()
    {
        int index = 0;
        if (GlobalConfig.Player)
        {
            nrOfTeams--;
            Instantiate(playerPrefab, spawns[index].position, spawns[index].rotation);
            index++;
        }
        while (index < nrOfTeams)
        {
            Instantiate(aiPrefab, spawns[index].position, spawns[index].rotation);
            index++;
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
    public void RegisterShip(Transform ship)
    {
        ships.Add(ship);
    }
    public void ShipDestroyed(Transform ship)
    {
        ships.Remove(ship);
        if (ships.Count == 1)
        {
            //game over
            gameOverScreen.gameObject.SetActive(true);
            gameOverText.text = $"Game over!\n {ships.GetEnumerator().Current} won!";
        }
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
