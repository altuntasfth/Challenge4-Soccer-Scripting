using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player { get { return GameObject.Find("Player"); } }
    public Rigidbody playerRb { get { return player.GetComponent<Rigidbody>(); } }

    [SerializeField]
    private GameObject powerup;

    [SerializeField]
    private GameObject enemy;

    private float spawnZMax = 25;
    private float spawnZmin = 15;
    private float spawnX = 10f;
    public int enemyCount;
    public int enemyWave = 1;

    // Update is called once per frame
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(enemyWave); 
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        Vector3 randPos = new Vector3(Random.Range(-spawnX, spawnX), 0, Random.Range(spawnZmin, spawnZMax));
        return randPos;
    }

    private void SpawnEnemyWave(int enemyWaveCount)
    {
        Vector3 powerupOffset = new Vector3(0, 0, -10);
        for (int i = 0; i < enemyWaveCount; i++)
        {
            Instantiate(enemy, GenerateSpawnPos(), enemy.transform.rotation);
        }
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
        {
            Instantiate(powerup, GenerateSpawnPos() + powerupOffset, powerup.transform.rotation);
        }
        enemyWave++;
        ResetPlayerPosition();
    }

    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
}