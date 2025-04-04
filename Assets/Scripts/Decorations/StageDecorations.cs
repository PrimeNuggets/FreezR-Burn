using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageDecorations : MonoBehaviour
{
    [Header("Decorations")]
    [SerializeField] private ObjectNumberPair[] decorations;
    [SerializeField] private int maxProbability = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomValue = Random.Range(0, 1000);
        var sorted = decorations.OrderBy(_ => Random.value).ThenByDescending(pair => pair.num).ToArray();
        bool summonedDecoration = false;
        if (randomValue < 250 && !summonedDecoration) {
            randomValue = Random.Range(0, maxProbability);
            foreach (var pair in sorted)
            {
                if (pair.num < randomValue && !summonedDecoration)
                {
                    Vector3 pos = new Vector3(transform.transform.position.x + pair.obj.transform.position.x, transform.transform.position.y + pair.obj.transform.position.y, 0);
                    UnityEngine.Quaternion rotation = Quaternion.identity;
                    if (pair.randRotation == true)
                    {
                        rotation = new Quaternion(rotation.x, rotation.y, Random.Range(0, 360), rotation.w);
                    }
                    Instantiate(pair.obj, pos, rotation, transform.transform);
                    summonedDecoration = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
