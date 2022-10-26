using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public GameObject population;
    public GameObject model;

    public Toggleas red;
    public Toggleas yellow;
    public Toggleas green;

    public Toggleas tall;

    public Toggleas small;

    public Toggleas medium;
    public float y = 0.97f;


    void Start()
    {

    initalizePopulation();


    StartCoroutine(loop());
    }
        
    IEnumerator loop()
    {
        yield return new WaitForSeconds(2);
        evolution();
        StartCoroutine(loop());
    }


    void initalizePopulation()
    {

        for (var i = 0; i < 10; i++)
        {
            var height = Random.Range(0.5f,2.4904f);
            GameObject s = Instantiate(model);
            s.transform.position = new Vector3(model.transform.position.x + (3f*i), y, model.transform.position.z );
            s.transform.localScale += new Vector3(0f,height,0f);
            var sc = s.transform.GetChild(0);
            var render = sc.gameObject.GetComponent<Renderer>();

            render.material.SetColor("_Color", Color.green);

            s.transform.SetParent(population.transform);
        }

    }

    void evolution() //no puedo hacer arrays mixtos hasta que yo sepa, asi que edito cada elemento del cromosoma en esta funcion
    {
        bool[] input = new bool[] {red.selected(),yellow.selected(),green.selected(),tall.selected(),medium.selected(),small.selected()}; //User input

        float[] possibleParents = new float[10];

        float parent1 = 0f;
        float parent2 = 0f;
        

        //saca parents usando modelo elitista
        for (var i = 0; i < 10; i++)
        {
            var obj = population.transform.GetChild(i);
            var d = obj.transform.localScale;
            
            possibleParents[i] = d.y/3;

            if (possibleParents[i] > parent1)
            {
                parent1 = possibleParents[i];
            }
        }
        for (var f = 0; f < 10; f++)
        {
            if (possibleParents[f] > parent2 && parent2 != parent1)
            {
                parent2 = possibleParents[f];
            }
        }



        for (var x = 0; x < 10; x++)
        {
            var obj = population.transform.GetChild(x);
            var d = obj.transform.localScale;
            var rend = obj.gameObject.GetComponent<Renderer>();

            //height determination
            if (d.y > 4f)
            {
                obj.transform.localScale = new Vector3(d.x,Random.Range(2.2f,3.5f),d.z); //Mutaciones si es muy alto
            }
            else if (input[3] == true)
            {
                
                obj.transform.localScale += new Vector3(0f,d.y/3f + (parent1/parent2) + Random.Range(-1,1),0);
            }
            else if (input[4] == true)
            {
                obj.transform.localScale += new Vector3(0f,d.y/2f + (parent1/parent2)+ Random.Range(-1,1),0);
            }
            else if (input[5] == true)
            {
                obj.transform.localScale += new Vector3(0f,d.y/1f + (parent1/parent2)+ Random.Range(-1,1),0);
            }


            //color determination
            if (input[0] == true)
            {
                rend.material.SetColor("_Color", Color.red);
            }
            else if (input[1] == true)
            {
                rend.material.SetColor("_Color", Color.yellow);
            }
            else if (input[2] == true)
            {
                rend.material.SetColor("_Color", Color.green);
            }
            else
            {
                rend.material.SetColor("_Color", Color.yellow);
            }

            possibleParents[x] = d.y/3;

        }



        
    }
}
