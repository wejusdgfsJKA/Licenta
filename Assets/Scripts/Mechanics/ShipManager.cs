using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{

    [SerializeField] Transform playerPrefab, aiPrefab;
    public static ShipManager Instance { get; private set; }
    public HashSet<Transform> Ships { get; protected set; } = new();
    [SerializeField] int nrOfTeams = 2;
    [SerializeField] List<Transform> spawns = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void RegisterShip(Transform ship)
    {
        Ships.Add(ship);
    }
    public void ShipDestroyed(Transform ship)
    {
        Ships.Remove(ship);
        if (Ships.Count == 1)
        {
            //game over
            foreach (var e in Ships)
            {
                GameManager.Instance.GameOver(e);
                break;
            }
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
            Instantiate(playerPrefab, spawns[index].position, spawns[index].rotation);
            index++;
        }
        while (index < nrOfTeams)
        {
            Instantiate(aiPrefab, spawns[index].position, spawns[index].rotation);
            index++;
        }
    }
}
