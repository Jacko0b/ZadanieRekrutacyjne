using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] private GameObject destroyedVersion;
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        Instantiate(destroyedVersion, transform.position, transform.rotation);
    }
}
