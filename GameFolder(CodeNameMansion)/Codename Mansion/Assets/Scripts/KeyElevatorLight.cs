using UnityEngine;

public class KeyElevatorLight : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Material solvedMaterial;
    [SerializeField] Material notSolvedMaterial;
    [SerializeField] KeyElevator keyElevator;
    private MeshRenderer meshRender;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyElevator.isSolved)
        {
            meshRender.material = solvedMaterial;
        }
        else
        {
            meshRender.material = notSolvedMaterial;
        }
    }
}
