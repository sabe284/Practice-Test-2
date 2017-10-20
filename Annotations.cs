using UnityEngine; // using this to access classes in unity
using Pathfinding.Serialization.JsonFx; // using this for the deserialization

public class Sketch : MonoBehaviour {
    public GameObject myPrefab; // defining public objects (e.g. to use spincube)
    public string _WebsiteURL = "http://infomgmt192.azurewebsites.net/tables/Products?zumo-api-version=2.0.0"; // defining website and table where we get data from

    void Start () { // what will happen from the start when play begins
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL); //to request to get data from the website defined before

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse)) //if the request from above has no response will return = exit
        {
            return; // return = exit
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Product[] products = JsonReader.Deserialize<Product[]>(jsonResponse); // reading the data from the website and classifying it

        //----------------------
        //YOU WILL NEED TO DECLARE SOME VARIABLES HERE SIMILAR TO THE CREATIVE CODING TUTORIAL

        int i = 0; // initially start with 0 cubes
        int totalCubes = 30; // will have 30 cubes in total
        float totalDistance = 2.9f; // the distance between all the cubes
        //----------------------

        //We can now loop through the array of objects and access each object individually
        foreach (Product product in products) // what to do for each instance of the object and its data (loop)
        {
            //Example of how to use the object
            Debug.Log("This products name is: " + product.ProductName); // will record errors about product name and display this message 
            //----------------------
            //YOUR CODE TO INSTANTIATE NEW PREFABS GOES HERE
            float perc = i / (float)totalCubes; // to get percentage
            float sin = Mathf.Sin(perc * Mathf.PI / 2); // to get the Sin of percentage

            float x = 1.8f + sin * totalDistance; // defines the X value for the gameobject
            float y = 5.0f; // defines the Y value for the gameobject
            float z = 0.0f; // defines the Z value for the gameobject

            var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity); // make a copy of spincube for each instance of data; set new sides for vector and set that instance with no rotation to align to world (Quarternion.identity)

            newCube.GetComponent<CubeScript>().SetSize(.45f * (1.0f - perc)); // accessing cube script to set cube size
            newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * 4.0f; // accessing cube script to set rotate speed
            newCube.transform.Find("New Text").GetComponent<TextMesh>().text = product.ProductName;// accessing cube script to set cube text; using transform to change the text into the product name which will be gotten by accessing the product data
            i++; // use i value and then increment by 1

            //----------------------
        }
	}
	
	// Update is called once per frame
	void Update () { // what would happen while in play (not as soon as it starts, during)
	
	}
}
