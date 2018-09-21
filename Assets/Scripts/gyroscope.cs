using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;		// Used for serial ports
using System.Linq;
using System.Threading;		// Used to add threading functionality

public class gyroscope : MonoBehaviour {

	public float Gx, Gy, Gz;
	SerialPort SerialPort;
    string read_data;
	string[] values;

	void Start () {

		SerialPort = new SerialPort();		// Declares Serial Port
		SerialPort.PortName = "/dev/tty.SLAB_USBtoUART";
		SerialPort.BaudRate = 230400;
        SerialPort.Open();					// Opens Serial Port


  		Thread thread = new Thread(new ThreadStart(dataread));
        thread.IsBackground = true;	// Specifies the thread is a Background thread running concurrently
		thread.Start();	// Starts the thread
	}

	public void dataread()
		{
			read_data = SerialPort.ReadLine(); 
		}

	// Update is called once per frame
	void Update ()
	{
			values = read_data.Split (':');
			int length = values.ToList().Count(); // Calculates length of the array of numbers
			if (length == 6) {		// Data is read only if length of array is 6 (no values missing)
				float[] float_values = Array.ConvertAll (values, s => float.Parse (s)); // Use of LINQ
				print("--------------------------");
				 foreach (float num in float_values) {
				 	print (num);
				 }
				print("--------------------------");
				Gx = float_values [3];
				Gy = float_values [4];  
				Gz = float_values [5];
			}
	}
}