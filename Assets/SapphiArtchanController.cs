using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapphiArtchanController : MonoBehaviour
{
    private Animator myAnimator2;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator2 = GetComponent<Animator>();

        this.myAnimator2.SetFloat("Speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
