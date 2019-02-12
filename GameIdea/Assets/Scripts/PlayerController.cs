using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void Update()
    {
        // This method only works for one directional ray, sphere cast would be more efficient
        RaycastHit hit;
        Ray landingRay0 = new Ray(transform.position, Vector3.forward);
        Ray landingRay1 = new Ray(transform.position, Vector3.back);
        Ray landingRay2 = new Ray(transform.position, Vector3.left);
        Ray landingRay3 = new Ray(transform.position, Vector3.right);
        if (Physics.Raycast(landingRay0, out hit, 0.5f) || Physics.Raycast(landingRay1, out hit, 0.5f) || Physics.Raycast(landingRay2, out hit, 0.5f) || Physics.Raycast(landingRay3, out hit, 0.5f))
        {
            if (hit.collider.tag == "Fixed")
            {
                winText.text = "You Lost";
                rb.constraints = RigidbodyConstraints.FreezeAll;

            }
        }
    }
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown("space") && GetComponent<Rigidbody>().transform.position.y <= 0.5f)
        {
            Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
            rb.AddForce(jump);
        }  

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
           SetCountText();
        }
    }
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "WallBreak") {
            Destroy(col.gameObject);
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
