using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction { front, back, left, right } /*创建方向的枚举*/

public class Player : MonoBehaviour, ICanTakeDamage
{

    private direction m_Direction; /*引用方向的枚举*/

    [SerializeField]
    private LayerMask m_LayerMask;

    private Camera m_Camera; /*创建摄像机*/
    private RaycastHit m_Hit; /*射线碰撞*/
    private Ray m_CameraRay; /*射线*/

    private Vector3 m_MouseDown;
    private Vector3 m_MouseDirection;
    private Vector3 m_MouseOrigin;

    private int _hP = 5;
    public int HP { get { return _hP; } set { _hP = value; } }

    private bool downDrift = false;
    private bool m_IsFront, m_IsLeft, m_IsRight, m_IsBack;

    private int _recordFollowers; /*创建一个记录跟随者*/
    public int RecordFollowers { get { return _recordFollowers; } private set { _recordFollowers = value; } }

    void Awake()
    {
        m_Camera = Camera.main;
    }

    void Start()
    {
        _recordFollowers = 0;
    }

    void Update()
    {
        if (GameManager.Instance.Paused || GameManager.Instance.GameOverBool) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 v = Input.mousePosition;
            v.z = 10;
            m_MouseDown = m_Camera.ScreenToWorldPoint(v);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUpTrue();
            if (_recordFollowers + 1 <= transform.position.z)
            {
                _recordFollowers++;
            }
        }
        RayToGround();  /*射线检测至地面的向量*/
        RayDetection();  /*射线检测障碍物*/
        ClampFrame(); /*限定行动范围 */
    }

    private void OnMouseDown() {
        m_CameraRay = m_Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(m_CameraRay, out m_Hit, 100))
        {
            m_MouseOrigin = m_Hit.point;
        }
    }

    private void OnMouseUpTrue() {
        if (m_MouseDirection.sqrMagnitude < 0.2 && !m_IsFront)
        {
            m_Direction = direction.front;
        }

        switch (m_Direction)
        {
            case direction.front:
                transform.eulerAngles = new Vector3(0, 0, 0);
                if (!m_IsFront)
                {
                    transform.position += Vector3.forward;
                }
                break;
            case direction.back:
                transform.eulerAngles = new Vector3(0, 180, 0);
                if (!m_IsBack)
                {
                    transform.position += Vector3.back;
                }
                break;
            case direction.left:
                transform.eulerAngles = new Vector3(0, 270, 0);
                if (!m_IsLeft)
                {
                    transform.position += Vector3.left;
                }
                break;
            case direction.right:
                transform.eulerAngles = new Vector3(0, 90, 0);
                if (!m_IsRight)
                {
                    transform.position += Vector3.right;
                }
                break;
        }
    }

    private void RayToGround()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 v = Input.mousePosition;
            v.z = 10;
            v = m_Camera.ScreenToWorldPoint(v);

            m_MouseDirection = v - m_MouseDown; /*鼠标向量等于新的鼠标位置减去一开始所点击的鼠标原点*/

            if (m_MouseDirection.sqrMagnitude > 0.2)
            {
                float angleZ = Vector3.Dot(Vector3.forward, m_MouseDirection.normalized);
                float angleX = Vector3.Dot(Vector3.right, m_MouseDirection.normalized);

                if (angleX > 0.5f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 90, 0), 500f * Time.deltaTime);
                    m_Direction = direction.right;
                }
                else if (angleX < -0.5f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 270, 0), 500f * Time.deltaTime);
                    m_Direction = direction.left;
                }
                else if (angleZ > 0.5f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), 500f * Time.deltaTime);
                    m_Direction = direction.front;
                }
                else if (angleZ < -0.5f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), 500f * Time.deltaTime);
                    m_Direction = direction.back;
                }
            }
            Vector3 hitPoint = v;
        }
    }

    private void RayDetection() {
        m_IsFront = Physics.Linecast(transform.position, transform.position + Vector3.forward, out m_Hit, m_LayerMask);
        m_IsLeft = Physics.Linecast(transform.position, transform.position + Vector3.left, out m_Hit, m_LayerMask);
        m_IsRight = Physics.Linecast(transform.position, transform.position + Vector3.right, out m_Hit, m_LayerMask);
        m_IsBack = Physics.Linecast(transform.position, transform.position + Vector3.back, out m_Hit, m_LayerMask);

        RaycastHit[] down = Physics.RaycastAll(transform.position, Vector3.down, 2f, m_LayerMask);

        if (down.Length > 0)
        {
            int i = down.Length;
            for (i = 0; i < down.Length; i++)
            {
                Debug.Log(down[i].transform.name);
                if (down[i].transform.tag == "Drift")
                {
                    downDrift = true;
                    transform.parent = down[i].transform;
                    return;
                }
            }
            if (!downDrift || down[0].transform.tag == "Water")
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, Time.deltaTime * 5);
            }
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            downDrift = false;
            transform.parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.back);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    }

    private void ClampFrame()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7, 7),
                                         transform.position.y,
                                         Mathf.Clamp(transform.position.z, _recordFollowers - 3, _recordFollowers + 1));
    }

    public void Damage(int damage, GameObject initiator)
    {
            HP -= damage;
            Debug.Log(HP);
            if (HP <= 0)
            {
                LevelDirector.Instance.OnGameOver();
                KillPlayer();
            }
    }

    private void KillPlayer()
    {
        Destroy(this.gameObject);
    }
}
