using UnityEngine;
using TMPro;

public class PopupDamage : MonoBehaviour
{
    public float moveSpeed = 1f;        // seberapa cepat teks bergerak ke atas
    public float fadeSpeed = 2f;        // seberapa cepat teks menghilang
    public float lifeTime = 1.5f;       // durasi sebelum dihancurkan
    private TextMeshProUGUI textMesh;      // komponen TextMeshPro
    private Color textColor;            // warna awal teks

    void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textColor = textMesh.color;
    }

    void Update()
    {
        // Gerakkan teks ke atas
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // Kurangi alpha (buat fade out)
        textColor.a -= fadeSpeed * Time.deltaTime;
        textMesh.color = textColor;

        // Hancurkan objek setelah habis waktu
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    // Opsional: supaya bisa ubah angka saat dipanggil
    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }
}
