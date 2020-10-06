using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class WorldGen : MonoBehaviour
{
    [Header("World Objects")]
    public GameObject stone1;
    public GameObject stone2;
    [Space(20)]
    public GameObject dirt1;
    public GameObject dirt2;
    [Space(20)]
    public GameObject grass;
    [Space(20)]
    [Header("World Settings")]
    public int width;
    [Range(5, 150)]
    public float heightMultiplier;
    public int heightAddition;
    [Range(5, 30)]
    public float smoothness;
    [HideInInspector]
    public long seed;

    GameObject SelectedTile;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    IEnumerator Wait(float time) {
        yield return new WaitForSeconds(time);
    }
    public void Generate()
    {
        seed = 16876;//Random.Range(1000, 100000);
        for (int i = 0; i < width; i++)
        {
            int h = Mathf.RoundToInt(Mathf.PerlinNoise(seed, ( i + transform.position.x ) / smoothness) * heightMultiplier) + heightAddition;
            for (int j = 0; j < h; j++)
            {
                Wait(0.01f);
                int stoneHeightRandomizer = Random.Range(5, 8);
                if (j < h - stoneHeightRandomizer)
                {
                    int randomStone = Random.Range(0, 2);
                    if (randomStone == 0)
                    {
                        SelectedTile = stone1;
                    }
                    else
                    {
                        SelectedTile = stone2;
                    }
                } //Stone creation, randomization and height randomization
                else if (j < h - 1)
                {
                    int randomDirt = Random.Range(0, 1);
                    if (randomDirt == 0)
                    {
                        SelectedTile = dirt1;
                    }
                    else
                    {
                        SelectedTile = dirt2;
                    }
                } // Dirt creation and randomization.
                else
                {
                    SelectedTile = grass;
                } //Grass creation
            GameObject newTile = Instantiate(SelectedTile, new Vector2(0,0), Quaternion.identity) as GameObject;
            newTile.transform.parent = this.gameObject.transform;
            newTile.transform.localPosition= new Vector2(Mathf.RoundToInt(i), Mathf.RoundToInt(j));
            }
            

        }
    }
}