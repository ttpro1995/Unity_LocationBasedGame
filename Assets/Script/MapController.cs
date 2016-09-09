using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MapController : MonoBehaviour {

    public GoogleMap map;
    public Text lat_ui;
    public Text long_ui;
    public Text counter;
    private int c = 0;

        IEnumerator Start()
        {
        counter.text = "Start to init location";
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
                yield break;

            // Start service before querying location
            Input.location.Start(1,0.1f);

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
            counter.text = "waiting";
            yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                print("Timed out");
            counter.text = "Timed out";
            yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determine device location");
            counter.text = "Unable to determine device location";
            yield break;
            }
            else
            {
            // Access granted and location value could be retrieved
            /*
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            // Update gps location for map
            map.centerLocation.latitude = Input.location.lastData.latitude;
            map.centerLocation.longitude = Input.location.lastData.longitude;
            map.markers[0].locations[0].latitude = Input.location.lastData.latitude;
            map.markers[0].locations[0].longitude = Input.location.lastData.longitude;

            // update debug text
            lat_ui.text = "Lat " + Input.location.lastData.latitude;
            long_ui.text = "Long " + Input.location.lastData.longitude;
            counter.text = c.ToString();
            yield return new WaitForSeconds(1f);

            */
            counter.text = "ready";
            }

            // Stop service if there is no need to query location updates continuously
           // Input.location.Stop();
        }

    void Update()
    {
        map.centerLocation.latitude = Input.location.lastData.latitude;
        map.centerLocation.longitude = Input.location.lastData.longitude;
        map.markers[0].locations[0].latitude = Input.location.lastData.latitude;
        map.markers[0].locations[0].longitude = Input.location.lastData.longitude;

        // update debug text
        lat_ui.text = "Lat " + Input.location.lastData.latitude;
        long_ui.text = "Long " + Input.location.lastData.longitude;
    }
    
}
