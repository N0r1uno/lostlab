using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public List<SpawnBurst> bursts;
    // Start is called before the first frame update
    void Start()
    {
        ResetSpawn();
        SpawnAll();
    }


    public void ResetSpawn()
    {
        foreach (SpawnBurst burst in bursts)
            burst.Reset();
    }

    public void SpawnAll()
    {
        foreach (SpawnBurst burst in bursts)
            burst.SpawnAt(this.transform.position);
    }

    [System.Serializable]
    public class SpawnBurst
    {
        public int count;
        public GameObject prefab;
        private GameObject[] spawned;

        public void Reset()
        {
            if (spawned != null)
            foreach (GameObject spawn in spawned)
                if (spawn != null)
                    Destroy(spawn);
            spawned = new GameObject[count];
        }

        public void SpawnAt(Vector2 position)
        {
            for (int i = 0; i < spawned.Length; i++)
                spawned[i] = Instantiate(prefab, position, Quaternion.identity, null);
        }
    }
}
