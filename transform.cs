using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transform : MonoBehaviour
{
    public float Speed = 0;
    public Image image;
    private new  Rigidbody2D rigidbody2D;
    
    private Button button;
    private bool isJump = false;
    public float Jumpforce = 300;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClick);
        
    }
    public void ShowInfo(string  info)
    {
      
    }
    public void ButtonClick()
    {
        Debug.Log("1111");

        this.GetComponent<Image>().color = Color.red;
       
    }
    void Update()
    {
        Move();
        Jump();
    }
  
    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
     

        Vector3 dir = new Vector3(horizontal, 0, 0);

        transform.Translate(dir * Time.deltaTime * Speed);

    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& isJump )
        {
           
            rigidbody2D.AddForce(Vector2.up * Jumpforce);
            print("1");
        }

    }
    private void OnCollisionEnter2D (Collision2D  collision)
    {
        if(collision.gameObject.tag == "Groud")
        {
            isJump = true;
            
        }
    }

}
