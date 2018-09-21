using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class epley : MonoBehaviour {


	private float Gx, Gy, Gz;

	private GameObject face;

	private GameObject canvas1, canvas2, canvas3, canvas4, canvas5;

	private VideoPlayer video1, video2, video3, video4, video5;

	private float temp;
	bool _continue;

	public bool flag1=false, flag2=false, flag3=false, flag4=false, flag5=false, range1=false, range2=false, range3=false, range4=false, hasEnd=false, videodone=false;

	private Text todo;

	private Text time;
	private Text step;
	private Text xangle;
	private Text yangle;
	private Text zangle;



	public float timer;
	public int inttime;

	private int stepnum;

	private int duration=5;


	// Use this for initialization
	void Start ()
	{

		face = GameObject.Find ("face");

		canvas1 = GameObject.Find ("player1");
		canvas2 = GameObject.Find ("player2");
		canvas3 = GameObject.Find ("player3");
		canvas4 = GameObject.Find ("player4");
		canvas5 = GameObject.Find ("player45deg");

		// Accessing videos from canvases
		video1 = canvas1.GetComponent<UnityEngine.Video.VideoPlayer> (); 
		video2 = canvas2.GetComponent<UnityEngine.Video.VideoPlayer> ();
		video3 = canvas3.GetComponent<UnityEngine.Video.VideoPlayer> ();
		video4 = canvas4.GetComponent<UnityEngine.Video.VideoPlayer> ();
		video5 = canvas5.GetComponent<UnityEngine.Video.VideoPlayer> ();

		video1.Play();

		todo.text = "Turn head by 45 degrees to right";

		timer = duration;
		inttime = duration;

		stepnum = 1;

		pause = 0;
	}

	
	// Update is called once per frame
	void Update ()
	{

		gyroscope GyroScript = face.GetComponent<gyroscope>(); //The gyroscope script is a component of the GameObject
		Gx = GyroScript.Gx;
		Gy = GyroScript.Gy;
		Gz = GyroScript.Gz;

		face.transform.Rotate (Gx * Time.deltaTime, Gy * Time.deltaTime, Gz * Time.deltaTime);

		var angles = face.transform.eulerAngles;
		// face.transform.eulerAngles refers to the x-y-z Euler Angles of the face

		xangle.text = ((int)angles.x).ToString();
		yangle.text = ((int)angles.y).ToString();
		zangle.text = ((int)angles.z).ToString();

		//___________________________________________________________________________

//	*** Step 1 ***
		if ((angles.y > 35) && (angles.y < 65) && (!flag1)) { 

			range1 = true;

			timer -= Time.deltaTime; // Timer decreases only when angle is within range
			inttime = (int)timer;
			time.text = inttime.ToString ();  // Changes content of timer's text box
			step.text = stepnum.ToString ();  // Changes content of step number's text box

			todo.text = "Hold for 5 seconds";

			if (inttime == 0) {		// Condition for completion of a step

				canvas1.SetActive(false);
				if (video1.isPlaying) {
					video1.Stop();
				}
				video2.Play();

				flag1 = true;	// Flag that indicates Step 1 is complete
				todo.text = "Step 1 complete. Move the head vertically down by 120 degrees";

				stepnum++;
				timer = duration;		// Resetting timer
				inttime = duration;		

				time.text = inttime.ToString ();
				step.text = stepnum.ToString ();  // Updating contents of text boxes

			}
		}
									
		if ((range1) && ((angles.y < 35) || (angles.y > 65))) { // && means 'and' and || means 'or'
			if (angles.y < 35) {
				todo.text = "Move head slightly more to right";
			} else if (angles.y > 65) {
				todo.text = "Move head slightly back to left";
			}
		}

//  *** Step 2 Walkthrough ***

		if ((timer == duration) && (flag1) && (!range2) && (!range3) && (!range4) && ((angles.x > 275) && (angles.x < 285))) {
			todo.text = "Tilt head to the right";
		}

		if ((flag1) && (!flag2) && ((angles.x > 295 && angles.x < 360) || (angles.x > 0 && angles.x < 20)) && ((angles.y > 80) && (angles.y < 140))) {

			range2 = true;
			timer -= Time.deltaTime;
			inttime = (int)timer;
			time.text = inttime.ToString ();
			step.text = stepnum.ToString ();
			todo.text = "Hold for " + duration + " seconds";

			if (inttime == 0) {
				canvas2.SetActive (false);
				if (video2.isPlaying) {
					video2.Stop ();
				}
				video3.Play ();

				flag2 = true;
				todo.text = "Step 2 complete. Move the head anti-cw by 90 degrees";
				stepnum++;
				timer = duration;
				inttime = duration;
				time.text = inttime.ToString ();
				step.text = stepnum.ToString ();

			}

		}

		if ((timer != duration) && (flag1) && (!range3) && (!range4) && (((angles.x > 275) && (angles.x < 295)) || ((angles.x > 20) && (angles.x < 40)) || ((angles.y < 80) || (angles.y > 140)))) {
			if ((angles.x > 275) && (angles.x < 295)) {
				todo.text = "Tilt head slightly more to the right";
			} else if ((angles.x > 20) && (angles.x < 40)) {
				todo.text = "Tilt head slightly back to left";
			}

			if ((angles.y < 80) && ((angles.x > 295 && angles.x < 360) || (angles.x > 0 && angles.x < 20))) {
				todo.text = "Move head slightly down";
			} else if ((angles.y > 140) && ((angles.x > 295 && angles.x < 360) || (angles.x > 0 && angles.x < 20))) {
				todo.text = "Move head slightly up";
			}

		}

// *** Step 3 Walkthrough ***

		if ((timer == duration) && (flag2) && (!range3) && (!range4) && (angles.x > 285) && (angles.y > (190)) && (angles.y < 220)) {
			todo.text = "Tilt head more to the left";
		}
		if ((timer == duration) && (flag2) && (!range3) && (!range4) && (angles.x < 285) && (angles.y > (220))) {
			todo.text = "Lower head slightly";
		}

		if ((flag2) && (!flag3) && ((angles.x > 285) && (angles.x < 330)) && ((angles.y > 220) && (angles.y < 260))) {

			range3 = true;

			timer -= Time.deltaTime;
			inttime = (int)timer;
			time.text = inttime.ToString ();
			step.text = stepnum.ToString ();
			todo.text = "Hold for " + duration + " seconds";

			if (inttime == 0) {
				canvas3.SetActive (false);
				if (video3.isPlaying) {
					video3.Stop ();
				}
				video4.Play ();

				flag3 = true;

				todo.text = "Step 3 complete. Move head anti-cw by another 90 degrees";
				stepnum++;
				timer = duration;
				inttime = duration;
				time.text = inttime.ToString ();
				step.text = stepnum.ToString ();

			}

		}

		if ((flag1) && (flag2) && (!range4) && (timer != duration) && (((angles.x < 285) || (angles.x > 330)) || ((angles.y < 220) || (angles.y > 260)))) {
			if (angles.x < 285) {
				todo.text = "Tilt head slightly more to left";
			} else if (angles.x > 330) {
				todo.text = "Tilt head slightly back to right";
			}

			if ((angles.y < 220) && ((angles.x > 285) && (angles.x < 330))) {
				todo.text = "Move head slightly up";
			} else if ((angles.y > 260) && ((angles.x > 285) && (angles.x < 330))) {
				todo.text = "Move head slightly down";
			}
			flag3 = false;
//					todo.text = "Third step incomplete";
		}

// *** Step 4 Walkthrough ***

		if ((timer == duration) && (flag3) && (!range4) && ((angles.x > 0) && (angles.x < 20))) {
			todo.text = "Tilt head more towards the ground";
		}

		if ((timer == duration) && (flag3) && (!range4) && ((angles.x > 40) && (angles.x < 70))) {
			todo.text = "Look up";
		}

		if ((flag3) && (!flag4) && ((angles.z > 25) && (angles.z < 70)) && ((angles.x > 10) && (angles.x < 40))) {

			range4 = true;

			timer -= Time.deltaTime;
			inttime = (int)timer;
			time.text = inttime.ToString ();
			step.text = stepnum.ToString ();
			todo.text = "Hold for " + duration + " seconds";
			if (inttime == 0) {
				player3.SetActive (false);
				if (video3.isPlaying) {
					video3.Stop ();
				}
				video4.Play ();

				flag4 = true;
				todo.text = "Step 4 complete. Rise up vertically";
				stepnum++;
				timer = duration;
				inttime = duration;
				time.text = inttime.ToString ();
				step.text = stepnum.ToString ();

			}
		}

		if ((flag1) && (flag2) && (flag3) && (range4) && (timer != duration) && (((angles.z < 25) || (angles.z > 70)) || ((angles.x < 10) || (angles.x > 40)))) {
			if (angles.x < 10) {
				todo.text = "Tilt head slightly towards the ground";
			} else if (angles.x > 40) {
				todo.text = "Tilt head back in opposite direction";
			}

			if ((angles.z < 25) && ((angles.y > 220) && (angles.y < 260))) {
				todo.text = "Move head slightly downwards";
			} else if ((angles.z > 70) && ((angles.y > 220) && (angles.y < 260))) {
				todo.text = "Move head slightly upwards";
			}
			flag4 = false;
//					todo.text = "Fourth step incomplete";
		}


// *** Completion message ***

		if ((flag1) && (flag2) && (flag3) && (flag4) && ((angles.z > 0) && (angles.z < 10)) && (angles.x < 10 || angles.x > 340)) {
			todo.text = "Epley's Maneuver Complete";
			timer = duration;
			inttime = duration;
			flag5 = true;
		}

		if ((flag1) && (flag2) && (flag3) && (flag4) && (flag5)) { // Flags for completion of each step
			hasEnd=true;
		}

		if (hasEnd) {
			pause += Time.deltaTime;  // A pause of four seconds added after Maneuver ends
			if (pause>=4) {
				SceneManager.LoadScene("end");
			}
		}


	}


}