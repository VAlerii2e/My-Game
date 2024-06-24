using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; // ������ ��
    public float spawnInterval = 5f; // �������� �� ������� ��
    public Transform background; // ��� �� Transform

    private Vector2 spawnAreaMin;
    private Vector2 spawnAreaMax;

    void Start()
    {
        CalculateSpawnArea();
        StartCoroutine(SpawnFood());
    }

    private void CalculateSpawnArea()
    {
        if (background == null)
        {
            Debug.LogError("Background is not assigned.");
            return;
        }

        SpriteRenderer backgroundRenderer = background.GetComponent<SpriteRenderer>();
        if (backgroundRenderer == null)
        {
            Debug.LogError("Background does not have a SpriteRenderer component.");
            return;
        }

        // ��������� ��� ������ ������ �� ����� ������ ������� ����
        Vector3 backgroundSize = backgroundRenderer.bounds.size;

        // ��������� �� Vector2 ��� ��������� ������������ ��������
        Vector2 backgroundPosition2D = new Vector2(background.position.x, background.position.y);
        Vector2 backgroundSize2D = new Vector2(backgroundSize.x, backgroundSize.y);

        spawnAreaMin = backgroundPosition2D - backgroundSize2D / 2;
        spawnAreaMax = backgroundPosition2D + backgroundSize2D / 2;
    }

    IEnumerator SpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
