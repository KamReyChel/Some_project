using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ListOfInstructions : MonoBehaviour
{
    public float Speed = 1.0f;
    public float SpeedRotation = 1.0f;
    public float SpeedScale = 1.0f;

    public List<BallInstruction> Instructions = new List<BallInstruction>();
    [SerializeField]
    private int CurrentInstruction = 0;
    //private float TimeInInstruction = 0.0f;
    [SerializeField]
    private float InstructionLenght = 1.0f;
    [SerializeField]
    private float TurnDistance = 1.0f;
    [SerializeField]
    private float ScaleDistance = 1.0f;

    private Vector3 VecRotation = Vector3.zero;
    private float CurrentDistance = 0.0f;
    private float CurrentScale = 0.0f;
    //private float CurrentRotation = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (CurrentInstruction < Instructions.Count)
        {
            //TimeInInstruction += Time.deltaTime;
            float RealSpeed = Speed * Time.deltaTime;
            float RealSpeedRotation = SpeedRotation * Time.deltaTime;
            float RealSpeedScale = SpeedScale * Time.deltaTime;

            switch (Instructions[CurrentInstruction])
            {
                case BallInstruction.MoveUp:
                    CurrentDistance += RealSpeed;
                    transform.position += Vector3.up * RealSpeed;
                    break;

                case BallInstruction.MoveDown:
                    CurrentDistance += RealSpeed;
                    transform.position += Vector3.down * RealSpeed;
                    break;

                case BallInstruction.MoveLeft:
                    CurrentDistance += RealSpeed;
                    transform.position += Vector3.left * RealSpeed;
                    break;

                case BallInstruction.MoveRight:
                    CurrentDistance += RealSpeed;
                    transform.position += Vector3.right * RealSpeed;
                    break;

                case BallInstruction.RotateLeft:
                    VecRotation += Vector3.forward * RealSpeedRotation;
                    transform.rotation = Quaternion.Euler(VecRotation);
                    break;

                case BallInstruction.RotateRight:
                    VecRotation += Vector3.back * RealSpeedRotation;
                    transform.rotation = Quaternion.Euler(VecRotation);
                    break;

                case BallInstruction.ScaleUp:
                    CurrentScale += RealSpeedScale;
                    transform.localScale += Vector3.one * RealSpeedScale;
                    break;

                case BallInstruction.ScaleDown:
                    CurrentScale += RealSpeedScale;
                    transform.localScale -= Vector3.one * RealSpeedScale;
                    break;

                default:
                    Debug.Log("Idle");
                    break;
            }


            if (CurrentDistance > InstructionLenght && (int)Instructions[CurrentInstruction] < 5)
            {
                CurrentDistance = 0.0f;
                ++CurrentInstruction;
            }

            if (Instructions.Count > CurrentInstruction)
            { 
                if (VecRotation.magnitude > TurnDistance && (int)Instructions[CurrentInstruction] >= 5 && (int)Instructions[CurrentInstruction] < 7)
                {
                    VecRotation = Vector3.zero;
                    ++CurrentInstruction;
                }
            }
            
            if (Instructions.Count > CurrentInstruction)
            {
                if (CurrentScale > ScaleDistance && (int)Instructions[CurrentInstruction] >= 7)
                {
                    VecRotation = Vector3.zero;
                    CurrentDistance = 0.0f;
                    CurrentScale = 0.0f;
                    ++CurrentInstruction;
                }
            }
            

        }
    }
}
