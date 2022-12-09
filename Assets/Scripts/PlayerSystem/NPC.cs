using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VGF.Dialogue;

namespace VGF.NPC
{
    [RequireComponent(typeof(BoxCollider))]
    
    public class NPC : MonoBehaviour
    {
        [Header("按键提示转移到玩家头部")]
        public bool Transport;

        [Header("该NPC是否为玩家")]
        public bool isPlayer;

        private BoxCollider NPCCollider;
        private GameObject KeyBoardTip;

        public NPCITellYou npcITellYou => GetComponent<NPCITellYou>();
        private float height => GetComponentInChildren<SpriteRenderer>().bounds.size.y;

        private Animator animator => GetComponent<Animator>();
        private void Awake()
        {
            gameObject.tag = "NPC";



            if (isPlayer)
                return;


            //KeyBoardTip=Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/KeyBoard"),transform.position+new Vector3(0,height,0),Quaternion.identity,transform);
            // if(Transport)
            //     KeyBoardTip=Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/KeyBoard"),new Vector3(0,2.4f,0),Quaternion.identity,transform);
            // else
            KeyBoardTip = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/KeyBoard"), transform.position + new Vector3(0, deltaY, 0), Quaternion.identity, transform);
            KeyBoardTip.SetActive(false);

            if (NPCCollider == null)
                NPCCollider = GetComponent<BoxCollider>();
            NPCCollider.isTrigger = true;
        }


        private Vector3 position;
        private void PlayerAndRealNPCUpdate()
        {
            try
            {
                var delta = (transform.position - position) / Time.deltaTime;
                //Debug.Log(delta);
                animator.SetFloat("Horizontal", delta.x);
                animator.SetFloat("Vertical", delta.z);
                animator.speed = Mathf.Clamp(Mathf.Max(delta.x, delta.z) * 0.2f, 1f, Mathf.Infinity);

                position = transform.position;
            }
            catch (System.Exception)
            {
                //throw;
            }

            Particle();

        }
        private ParticleSystem particle=>GetComponentInChildren<ParticleSystem>();
        private Terrain terrain=>GameObject.Find("Terrain").GetComponent<Terrain>();
        void Particle(){
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        [Header("按键提示y轴修正值")]
        public float deltaY;
        // Update is called once per frame
        void Update()
        {
            PlayerAndRealNPCUpdate();

            if (isPlayer)
                return;


            if (!isPlayer && Interactive && Input.GetKeyDown(KeyCode.E) && npcITellYou && KeyBoardTip.activeSelf && !DialogueManager.instance.isChatting)
            {
                if (npcITellYou)
                    npcITellYou.Interact();


            }
        }

        private void OnCollisionEnter(Collision other)
        {
        }
        private void OnCollisionExit(Collision other)
        {
        }
        //Fail ： CC和Collider得有一个刚体 （？）

        [SerializeField]
        private bool _interactive;

        public bool Interactive
        {
            get => _interactive && !Mute;
            set
            {
                value = _interactive;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (isPlayer)
                return;

            Player player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                //玩家进入
                if (_interactive)
                {
                    KeyBoardTip.SetActive(true);
                    if (Transport)
                    {
                        KeyBoardTip.transform.SetParent(Player.instance.transform);
                        KeyBoardTip.transform.localPosition = new Vector3(0, 2.4f, 0);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (isPlayer)
                return;


            Player player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                //玩家离开
                if (_interactive)
                {
                    KeyBoardTip.SetActive(false);
                    if (Transport)
                        KeyBoardTip.transform.SetParent(transform);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //这里面会丢帧，而且丢帧率还挺高的，不能用来检测按键

            // if(Input.GetKeyDown(KeyCode.E)&&npcITellYou&&KeyBoardTip.activeSelf)
            //     npcITellYou.Interact();

            // return;
            // Player player=other.gameObject.GetComponent<Player>();
            // if(player){
            //     //玩家待在监测区域内

            // }
        }

        private bool _mute = false;
        /// <summary>
        /// 禁用spriteRenderer（目前看来）
        /// </summary>

        public bool Mute
        {
            get
            {
                return _mute;
            }
            set
            {
                _mute = value;
                if (value)
                {
                    GetComponentInChildren<SpriteRenderer>().enabled = false;
                    //施工中…………………………………………
                }
                else
                {
                    GetComponentInChildren<SpriteRenderer>().enabled = true;
                }
            }
        }

    }
}