using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class file : MonoBehaviour
{
    public float Hiz = 5f;
    public float ZiplamaGucu = 6f;
    public Transform Kamera;
    public TextMeshProUGUI AltinYazisi;
    
    private Rigidbody rb;
    private int toplamAltin = 0;
    private bool yerdeMi = true;

    void Start()
    {
        // CheckpointManager'dan gelen pozisyonu al
        transform.position = CheckpointManager.SonKayitNoktasi;
        
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AltinYazisiGuncelle();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && yerdeMi)
        {
            rb.AddForce(Vector3.up * ZiplamaGucu, ForceMode.Impulse);
            yerdeMi = false;
        }
        HareketMantigi();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Plane" || collision.gameObject.CompareTag("JTrap"))
        {
            yerdeMi = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Plane" || collision.gameObject.CompareTag("JTrap"))
        {
            yerdeMi = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ÖLÜNCE SAHNEYİ YENİDEN YÜKLE (Altınlar gitsin dediğin için)
        if (collision.gameObject.CompareTag("Olum"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ALTIN TOPLAMA
        if (other.gameObject.CompareTag("Altin"))
        {
            toplamAltin++;
            AltinYazisiGuncelle();
            Destroy(other.gameObject);
        }
        
        // Eğer ölüm alanın "Trigger" ise (içinden geçiliyorsa) burası çalışır
        if (other.gameObject.CompareTag("Olum"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            CheckpointManager.SonKayitNoktasi = other.transform.position;
            Debug.Log("Yeni kayıt noktası hafızaya alındı!");
        }
    }

    void HareketMantigi()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");
        Vector3 ileri = Kamera.forward;
        Vector3 sag = Kamera.right;
        ileri.y = 0;
        sag.y = 0;
        Vector3 hareketYonu = (ileri.normalized * dikey + sag.normalized * yatay).normalized;
        transform.Translate(hareketYonu * Hiz * Time.deltaTime, Space.World);
    }

    void AltinYazisiGuncelle()
    {
        if (AltinYazisi != null)
        {
            AltinYazisi.text = "Gold: " + toplamAltin;
        }
    }
}