

using UnityEngine;

public class PlayAmination : MonoBehaviour {

    public Animator animator;
    public Transform ModelPosition;
    private void Start()
    {
        animator = GameObject.FindObjectOfType<Animator>();
    }

   public void OnTouch  ()
    {
        Debug.Log("Mouse Enter");
        animator.SetBool("PlayAnim", true);
        GetComponent<BoxCollider>().enabled = false;
	}
   

    private void Update()
    {

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
          
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("PlayAnim", false);
        }

        if (ModelPosition == null)
        {
            if (GameObject.FindGameObjectWithTag("ModelPosition"))
            {
                ModelPosition = GameObject.FindGameObjectWithTag("ModelPosition").gameObject.transform;

                gameObject.transform.localPosition = ModelPosition.localPosition;
                gameObject.transform.localEulerAngles = ModelPosition.localEulerAngles;
                gameObject.transform.localScale = ModelPosition.localScale;
                animator.SetBool("Happy", true);
            }
        }
    }

}
