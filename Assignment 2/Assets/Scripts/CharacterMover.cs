using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class CharacterMover : MonoBehaviour
{
    // Allows us to hookup calls to when the final destination is reached
    public UnityEvent OnFinalDestinationReached;

    // These can be set in the inspector in the Unity editor
    // For this sample I've used the DoorTarget object inside each Place object
    public List<Transform> Destinations;

    public DefaultTrackableEventHandler ScenarioImageTracker;

    public float MoveSpeed = 0.1f;

    private int CurrentDestinationIndex = 0;
    private bool DoMovement = false;
    private Transform MyTransform;

    private bool voiceMovement = true;

    // https://youtu.be/Xau3hFEcn0U and https://lightbuzz.com/speech-recognition-unity/ for speech recongnition
    private Dictionary<string, UnityAction> keywordActions = new Dictionary<string, UnityAction>();
    private KeywordRecognizer keywordrecog;
    private ConfidenceLevel confidence = ConfidenceLevel.Low;
    private string[] actions1 = new string[] { "resume", "pause", "jump" };

    public Animator animator;

    public GameObject tiltObj;
    public Transform tiltTrans;
    public Transform originalTransform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        keywordActions.Add("resume", resumeFoward);
        keywordActions.Add("pause", pauseAction);
        keywordActions.Add("jump", jumpAction);
        
        //keywordActions.Keys.CopyTo(actions1, 0);
        
        keywordrecog = new KeywordRecognizer(actions1,confidence);
        keywordrecog.OnPhraseRecognized += OnKeywordsRecongnized;
        keywordrecog.Start();
        MyTransform = transform;
        
        // Will break of this has not been set up in the editor
        ScenarioImageTracker.OnTargetFound.AddListener(TurnMoveToDestinationsOn);
        ScenarioImageTracker.OnTargetLost.AddListener(TurnMoveToDestinationsOff);

        tiltObj = GameObject.Find("Base-ImageTarget");
        


    }

    private void OnKeywordsRecongnized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        tiltTrans = tiltObj.transform;
        var trotation = tiltTrans.rotation.eulerAngles;
        Debug.Log(trotation.z);
        if(trotation.z > 300 && trotation.z < 345)
        {
            Debug.Log("Move left");
            if(MoveSpeed > 0)
            {
                MoveSpeed -= 0.001f;
            }
            
            
        }else if (trotation.z < 40 && trotation.z > 15)
        {
            Debug.Log("Move right");
            if (MoveSpeed < 0.2)
            {
                MoveSpeed += 0.001f;
            }
            
        }
        else
        {
            Debug.Log("Stay");
        }

        if (voiceMovement &&DoMovement && CurrentDestinationIndex < Destinations.Count) {
            // Based on Unity documenation: https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
            float step = MoveSpeed * Time.deltaTime;

            var target = Destinations[CurrentDestinationIndex];

            // Moves us a step closer to the object. 
            // We will rely on collision triggers to determine when the object has been reached instead of checking
            // the positions here
            MyTransform.position = Vector3.MoveTowards(MyTransform.position, target.position, step);

            // This rotation code from http://answers.unity.com/answers/867743/view.html
            float distanceToPlane = Vector3.Dot(MyTransform.up, target.position - MyTransform.position);
            Vector3 pointOnPlane = target.position - (MyTransform.up * distanceToPlane);

            MyTransform.LookAt(pointOnPlane, MyTransform.up);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entered:" + other.name);
        //Debug.Log("Action: " + actions1[0].ToString());
        if (Destinations[CurrentDestinationIndex].gameObject == other.gameObject)
        {
            // There are probably cleaner and less coupled ways of doing this
            var DoorTarget = other.gameObject.GetComponent<DoorTarget>();
            if (DoorTarget != null)
            {
                Debug.Log("Door hit");
                DoorTarget.CloseDoor();
            }
            CurrentDestinationIndex++;

            if (CurrentDestinationIndex >= Destinations.Count)
            {
                TurnMoveToDestinationsOff();
                if (OnFinalDestinationReached != null)
                {
                    OnFinalDestinationReached.Invoke();
                }

                MyTransform.localPosition = Vector3.zero;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if(keywordrecog != null && keywordrecog.IsRunning)
        {
            keywordrecog.OnPhraseRecognized -= OnKeywordsRecongnized;
            keywordrecog.Stop();
        }
    }

    public void TurnMoveToDestinationsOn() {
        DoMovement = true;
        originalTransform = tiltObj.transform;
    }

    public void TurnMoveToDestinationsOff() {
        DoMovement = false;
    }

    public void resumeFoward()
    {
        Debug.Log("Resume");
        voiceMovement = true;
    }

    public void pauseAction()
    {
        Debug.Log("Pause");
        voiceMovement = false;
    }

    public void jumpAction()
    {
        Debug.Log("Jump");
        animator.SetTrigger("Jump");
        var particle = GameObject.Find("Character Particle");
        particle.SetActive(true);
    }
}
