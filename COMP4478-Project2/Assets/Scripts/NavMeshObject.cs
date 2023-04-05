using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavMeshObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateNavMesh());
    }

    // Update is called once per frame
    private IEnumerator UpdateNavMesh()
    {
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<NavMeshSurface>().BuildNavMeshAsync();
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
