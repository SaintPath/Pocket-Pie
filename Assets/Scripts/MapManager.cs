using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map = default;
    [SerializeField]
    private Tilemap trees = default;
    [SerializeField]
    private Tilemap stones = default;

    [SerializeField]
    private ParticleSystem particles = default;

    private Touch touch;
    private PopupManager popupManager;

    private float currCountdownValue;

    private void Start()
    {
        popupManager = GetComponent<PopupManager>();
    }


    void Update()
    {
        #region computer
        if (Input.GetMouseButtonDown(0) && !popupManager.displayingPopup)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            if (trees.GetTile(gridPosition) == null)
            {
                TileBase clickedTile = map.GetTile(gridPosition);
                if (clickedTile != null)
                {
                    particles.transform.position = mousePosition;
                    StartCoroutine(StartCountdown(gridPosition));
                }
            }
            else
            {
                popupManager.DisplayPopup();
            }
            
        }
        #endregion

        #region phone
        if (Input.touchCount > 0 && !popupManager.displayingPopup)
        {
            touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3Int gridPosition = map.WorldToCell(touchPosition);

            if (trees.GetTile(gridPosition) == null)
            {
                TileBase clickedTile = map.GetTile(gridPosition);
                if (clickedTile != null)
                {
                    particles.transform.position = touchPosition;
                    StartCoroutine(StartCountdown(gridPosition));
                }
            }
            else
            {
                popupManager.DisplayPopup();
            }

        }
        #endregion
    }


    public IEnumerator StartCountdown(Vector3Int gridPosition, float countdownValue = 2)
    {

        particles.Play();
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        particles.Stop();
        map.SetTile(gridPosition, null);
        stones.SetTile(gridPosition, null);
    }
}

