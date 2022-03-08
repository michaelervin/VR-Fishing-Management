using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PondChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject proximityPoint;
    [SerializeField] private float proximityDistance = 0.5f;
    [SerializeField] private GameObject handL;
    [SerializeField] private GameObject handR;
    [SerializeField] private GameObject truckHandle;
    public Object targetScene;

    private void Update()
    {
        if (Vector3.Distance(handL.transform.position,
                proximityPoint.transform.position) <= proximityDistance ||
            Vector3.Distance(handR.transform.position,
                proximityPoint.transform.position) <= proximityDistance)
        {
            SceneManager.LoadScene(targetScene.name);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(proximityPoint.transform.position, proximityDistance);
    }
}