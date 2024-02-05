using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AsteroidHit : MonoBehaviour
{
    [SerializeField] private GameObject asteroidExplosion;
    [SerializeField] private GameObject asteroidPopup;

    public void AsteroidDestroyed()
    {
        Instantiate(asteroidExplosion, transform.position, transform.rotation);

        if(GameController.currentGameStatus == GameController.GameState.Playing)
        {
            // calculating score based on the distance
            int distanceFromPlayer = (int)(Vector3.Distance(transform.position, Vector3.zero));

            int asteroidScore = 10 * distanceFromPlayer;

            // add the pop up canvas
            asteroidPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = asteroidScore.ToString();
            GameObject popup = Instantiate(asteroidPopup, transform.position, Quaternion.identity);

            // adjust the scale of the popup
            popup.transform.localScale = new Vector3(transform.localScale.x * distanceFromPlayer / 5,
                transform.localScale.y * distanceFromPlayer / 5,
                transform.localScale.z * distanceFromPlayer / 5);

            GameController.Instance.UpdateScore(asteroidScore);
        }

        Destroy(gameObject);
    }
}
