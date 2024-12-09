using UnityEngine;

public class AsteriodSpawner : MonoBehaviour
{
    public Asteriod asteriodPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Asteriod asteriod = Instantiate(asteriodPrefab, spawnPoint, rotation);
            asteriod.size = Random.Range(asteriod.minSize, asteriod.maxSize);
            
            asteriod.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
