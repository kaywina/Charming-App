using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticleColorFromCharm : MonoBehaviour
{
    public ParticleSystem ps;
   
    public void SetColor(string charmName)
    {
        var main = ps.main;

        switch (charmName)
        {
            case "Love":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.magenta, Color.magenta);
                break;
            case "Grace":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.cyan, Color.cyan);
                break;
            case "Patience":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.green);
                break;
            case "Wisdom":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.yellow);
                break;
            case "Joy":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.magenta, Color.blue);
                break;
            case "Focus":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.white);
                break;
            case "Will":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.yellow);
                break;
            case "Guile":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.green, Color.cyan);
                break;
            case "Force":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.red);
                break;
            case "Honor":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.white);
                break;
            case "Faith":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.grey, Color.white);
                break;
            case "Vision":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.blue, Color.red);
                break;
            case "Balance":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.cyan, Color.magenta);
                break;
            case "Harmony":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.blue, Color.cyan);
                break;
            case "Regard":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.green, Color.blue);
                break;
            case "Insight":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.grey);
                break;
            case "Plenty":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.white);
                break;
            case "Influence":
                main.startColor = new ParticleSystem.MinMaxGradient(Color.magenta, Color.red);
                break;
            default:
                Debug.Log("This is not the case you are looking for");
                break;
        }
    }
}
