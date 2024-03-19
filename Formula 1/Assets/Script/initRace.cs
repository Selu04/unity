using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CarData
{
    public string teamName;     // Nombre del equipo
    public Sprite carImage;     // Imagen del coche
    [HideInInspector] public int startingPosition; // Posición de salida del equipo (no visible en el inspector)
}

public class ReplaceEmptyObjectsWithCars : MonoBehaviour
{
    public CarData[] carDataArray;  // Array de datos de los coches
    public GameObject[] emptyObjects; // Array de objetos vacíos

    void Start()
    {
        // Verifica que los arrays no estén vacíos y tengan la misma longitud
        if (carDataArray != null && carDataArray.Length > 0 && emptyObjects != null && emptyObjects.Length == carDataArray.Length)
        {
            int[] startingPositions = GetStartingPositions();

            for (int i = 0; i < carDataArray.Length; i++)
            {
                CarData carData = carDataArray[i];
                GameObject emptyObject = emptyObjects[i];
                int startingPosition = startingPositions[i];

                if (carData != null && emptyObject != null)
                {
                    // Obtiene la posición, escala y rotación del objeto vacío
                    Vector3 position = emptyObject.transform.position;
                    Vector3 scale = emptyObject.transform.localScale;
                    Quaternion rotation = emptyObject.transform.rotation;

                    // Ajusta la posición del coche según la posición elegida por el usuario
                    position.y += startingPosition * 2; // Ajuste arbitrario, puedes cambiarlo según tus necesidades

                    // Crea una nueva imagen en la posición, escala y rotación del objeto vacío
                    GameObject carObject = new GameObject(carData.teamName);
                    Image carImageComponent = carObject.AddComponent<Image>();
                    carImageComponent.sprite = carData.carImage;
                    carObject.transform.position = position;
                    carObject.transform.localScale = scale;
                    carObject.transform.rotation = rotation;

                    // Asegúrate de que la imagen tenga un componente rectTransform para controlar su tamaño y posición
                    RectTransform rectTransform = carObject.GetComponent<RectTransform>();
                    if (rectTransform == null)
                        rectTransform = carObject.AddComponent<RectTransform>();

                    // Destruye el objeto vacío
                    Destroy(emptyObject);
                }
                else
                {
                    Debug.LogError("Datos de coche incompletos o objeto vacío no asignado en el índice: " + i);
                }
            }
        }
        else
        {
            Debug.LogError("Arrays de datos de coches y de objetos vacíos no tienen la misma longitud o son nulos.");
        }
    }

    // Método para obtener las posiciones de salida elegidas por el usuario
    int[] GetStartingPositions()
    {
        int[] startingPositions = new int[carDataArray.Length];

        for (int i = 0; i < startingPositions.Length; i++)
        {
            startingPositions[i] = Random.Range(1, 5); // Posiciones aleatorias del 1 al 4
        }

        return startingPositions;
    }
}