using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarphonesController : MonoBehaviour{

    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float maxBattery = 100.0f;  //バッテリー最大値
    [SerializeField] float maxSpeed = 28.0f;  //地上最大速度
    [SerializeField] float maxBoost = 20.0f;  //ブースト最大速度
    [SerializeField] float charge = 1.0f;  //バッテリー回復速度
    //[SerializeField] float standByConsumption = 1.0f;  //バッテリー自然消費量
    [SerializeField] float remoteConsumption = 30.0f;  //遠隔操作の消費量
    [SerializeField] float runForce= 22.0f;  //地上移動加速速度
    [SerializeField] float boostForce = 10.0f;  //ブースト加速度
    [SerializeField] float boostConsum = 1.0f; //ブースト消費量
    //[SerializeField] float boostRecover = 3.0f; //ブースト回復量
    [SerializeField] float boostLimit = 300.0f;  //最大ブースト残量
    [SerializeField] float sirentTime = 100.0f;  //サイレント機能
    [SerializeField] float batteryRemnant = 0f;  //バッテリー残量
    [SerializeField] float boostRemnant = 0f;  //ブースト残量
    private bool chargeOn = false;  //充電可否
    private bool silentMode = false;  //サイレント機能
    private bool furnitureSounds = false;  //家具の音出し
    private bool boostOn = true;  //ブーストの可否
    //private bool boostRecoverStart = true;  //ブースト自動回復フラグ
    public AudioClip landingSounds;  //着地音格納
    public AudioClip jetSounds;　　//ジェット音格納
    public AudioClip searchAPSounds;　//人間側索敵用音格納
    public AudioClip warning;  //人間接近時格納用

    Photon.Pun.PhotonView view;
    bool isMine;
    public GameObject[] otherObjects;
    public Camera myCamera;

    void Start(){
        view = GetComponent<Photon.Pun.PhotonView>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        batteryRemnant = maxBattery;
        boostRemnant = boostLimit;
    }

    
    void Update(){
        // 所有権が自分になければカメラと動作フラグを切る
        if (view.IsMine)
        {
            isMine = true;
            foreach (var obj in otherObjects)
            {
                obj.SetActive(true);
            }
            myCamera.enabled = true;
        }
        else
        {
            isMine = false;
            foreach(var obj in otherObjects)
            {
                obj.SetActive(false);
            }
            myCamera.enabled = false;
        }

        if (!view.IsMine)
        {
            return;
        }

        BoostRecoverFlag();
        //BoostRecoverStart();

        //if(chargeOn == false){
        //    StartCoroutine("StandByConsumption");
        //}

        if(chargeOn == true){
            StartCoroutine("BatteryChage");
        }

        if(Input.GetMouseButton(0)){
            JetAudio();
        }
    }


    private void FixedUpdate(){
        if (!view.IsMine)
        {
            return;
        }
            Move();
    }


    //WASDで移動
    void Move() {
        if (Input.GetKey(KeyCode.W)) {
            if (rb.velocity.magnitude < maxSpeed) {
                rb.AddForce(0, 0, runForce);
            }
        }

        if (Input.GetKey(KeyCode.S)) {
            if (rb.velocity.magnitude <= maxSpeed) {
                rb.AddForce(0, 0, runForce * -1);
            }
        }

        if (Input.GetKey(KeyCode.D)) {
            if (this.rb.velocity.magnitude < maxSpeed) {
                rb.AddForce(runForce, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            if (rb.velocity.magnitude < maxSpeed) {
                rb.AddForce(runForce * -1, 0, 0);
            }
        }

        //マウス左クリックでブースト
        if (Input.GetMouseButton(0)) {
            if (boostOn == true) {
                //boostRecoverStart = false;

                //ブーストで上昇、ブースト残量減る
                if (rb.velocity.magnitude < maxBoost) {
                    rb.AddForce(0, boostForce, 0);
                    boostRemnant -= boostConsum;

                    //残量０でブースト不可
                    if (boostRemnant <= 0) {
                        boostOn = false;
                    }
                }
            }
        }
    }


    
    //遠隔操作によるバッテリー減少
    void FurnitureSounds(){
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            batteryRemnant -= remoteConsumption;
        }

    }


    //時間経過でバッテリー減少
    //private IEnumerator StandByConsumption() {
    //    chargeOn = false;
    //    batteryRemnant -= standByConsumption;

    //    yield return new WaitForSeconds(2.0f);

    //    chargeOn = true;
    //}



    //バッテリー回復
    private IEnumerator BatteryChage(){
        chargeOn = false;
        if(batteryRemnant <= maxBattery){
            batteryRemnant += charge;

            yield return new WaitForSeconds(0.1f);

            chargeOn = true;
        }
    }


    //ブースト自動回復フラグtrue
    void BoostRecoverFlag() {
        if (Input.GetMouseButtonUp(0)) {
            boostOn = true;
            //boostRecoverStart = true;
        }
    }

    ////ブースト自動回復
    //private IEnumerator BoostRecoverStart(){
    //    boostRecoverStart = false;
    //    //ブースト回復フラグtrueでブースト残量自動回復
    //    if (boostRecoverStart == true) {
    //        if(boostRemnant < boostLimit) {
    //            boostRemnant += boostRecover;
    //        }
    //    }

    //    yield return new WaitForSeconds(0.1f);

    //    boostRecoverStart = false;
    //}


    //ブースト音再生
    void JetAudio() {
        audioSource.PlayOneShot(jetSounds);
    }


    private void OnCollisionEnter(Collision col) {       
        //着地音再生、着地時にブースト回復,
        if (col.gameObject.tag == "Floor") {
            audioSource.PlayOneShot(landingSounds);
        }
        //着地時にブースト回復
        boostRemnant = boostLimit;
    }

    private void OnTriggerStay(Collider col) {
        //ドアなどのトリガーに触れた状態で右クリックで使用
        if (col.gameObject.tag == "Furniture") {
            if (Input.GetMouseButtonDown(1)){

            }
        }

        //充電器なら充電
        if(col.gameObject.tag == "Chager"){
            if(Input.GetMouseButton(1)){
                chargeOn = true;
            }
        }
    }


    private void OnTriggerEnter(Collider col) {
        //近くに人間がいるならアラートを鳴らす
        if(col.gameObject.tag == "Human"){
            audioSource.PlayOneShot(warning);
        }
    }
}
　