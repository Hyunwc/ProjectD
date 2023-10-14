using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //총알 프리팹을 담아둘 변수
    //public GameObject bulletPref;

    //총알 발사하는 힘
    //public float firePower;

    //총 효과 프리팹
    public GameObject shootEffectPref;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Confined;   //마우스 커서가 게임 화면 못 벗어나게
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 좌클릭시 총알 프리팹 생성
        if(Input.GetMouseButtonDown(0))
        {
            //화면 한가운데에서 시작하는 레이 생성
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            //Ray에 맞은 물체를 담아둘 변수
            RaycastHit hit;
            //레이를 발사하고 레이에 맞은 물체는 hit에 저장
            if(Physics.Raycast(ray, out hit))
            {
                //맞은 위치에 맞은 표면의 수직이 되는 각도로 총 효과 프리팹 생성
                GameObject shootEffect = Instantiate(shootEffectPref, hit.point + hit.normal* 0.01f, Quaternion.LookRotation(hit.normal));
                //총알 자국을 맞은 오브젝트의 자식으로 설정
                shootEffect.transform.SetParent(hit.transform);

                //레이에 맞은 물체가 적이라면
                if(hit.transform.tag == "Enemy")
                {
                    //적에게 10만큼 데미지 주라고 전달
                    hit.transform.SendMessage("Damaged", 10);
                }
            }
            

            //플레이어의 위치보다 1 앞에, 벡터가아닌 transform인 이유는 회전할때도 올바른 위치에 생성되게 하기 위함
            //생성 후 bullet 변수에 할당
            //GameObject bullet = Instantiate(bulletPref, transform.position + transform.forward, Quaternion.identity);

            //총알 프리팹이 앞으로 날아가는 순간적인 힘 발생
           // bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);
        }
    }
}
