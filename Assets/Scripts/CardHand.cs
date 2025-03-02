﻿using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>(); //lista de cartas que tienes al jugar
    public GameObject card; //objeto card para instanciar
    public bool isDealer = false; //si es player o no
    public int points; //puntos de cada carta
    private int coordY; //coordenada de insertar carta
     
    private void Awake()
    {
        points = 0; //empieza por 0
        //Definimos dónde posicionamos las cartas de cada uno
        if (!isDealer) //si es jugador
            coordY = 3; //coor de las cartas
        else //si es player
            coordY = -1;
    }

    public void Clear() 
    {
        points = 0; //se limpia los puntos
        if (!isDealer)
            coordY = 3;
        else
            coordY = -1;
        foreach (GameObject g in cards) //destruir las cartas del tablero
        {
            Destroy(g);
        }
        cards.Clear();     //limpiar lista                    
    }        

    public void InitialToggle()
    {
        cards[0].GetComponent<CardModel>().ToggleFace(true);              
    }

    public void Push(Sprite front, int value)
    {
        //Creamos una carta y la añadimos a nuestra mano
        GameObject cardCopy = (GameObject)Instantiate(card);
        cards.Add(cardCopy);

        //La posicionamos en el tablero 
        float coordX = (float)1.4 * (float)(cards.Count - 4);
        Vector3 pos = new Vector3(coordX, coordY);               
        cardCopy.transform.position = pos;

        //Le ponemos la imagen y el valor asignado
        cardCopy.GetComponent<CardModel>().front = front;
        cardCopy.GetComponent<CardModel>().value = value;
        
        //La cubrimos si es la primera del dealer
        if (isDealer && cards.Count <= 1)
            cardCopy.GetComponent<CardModel>().ToggleFace(false);
        else
            cardCopy.GetComponent<CardModel>().ToggleFace(true);

        //Calculamos la puntuación de nuestra mano
        int val = 0;
        int aces = 0;
        foreach (GameObject f in cards)
        {            

            if (f.GetComponent<CardModel>().value != 11)
                val += f.GetComponent<CardModel>().value;
            else
                aces++;
        }

        for (int i = 0; i < aces; i++)
        {
            if (val + 11 <= 21)
            {
                val = val + 11;
            }
            else
            {
                val = val + 1;
            }
        }

        points = val;
       
    }
     

}
