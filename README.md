# BPPV Assistant for Unity

Unity (C#) app used to treat BPPV (vertigo) using a gyroscope

## About

*BPPV Assistant for Unity* is a desktop application developed using Unity for NeuroEquilibrium Diagnostics. It is the first technological
solution for assisting doctors while treating Benign Paroxysmal Positional Vertigo (BPPV), the most common type of vertigo disorder.

The application makes use of a Pololu gyrosocope to guide doctors in performing the Epley, Barbeque, Semont, and Head Hanging Maneuver, 
a series of head movements performed on the patient to treat various forms of BPPV. It supports guidance for both left and right ear canals.

#### Features
* Head Model: Shows a live 3-D model of the patient's head that tracks the head's movement in realtime
* Angles: Displays the angles at which the head is held
* Timer: Tracks time for each step of the maneuvers and resets accordingly throughout each step
* Messages: Shows appropriate messages for guiding the maneuver, including correting the doctor at incorrect angles, providing "Hold for 'x' seconds" alerts, etc.
each step of the maneuver. 

## Preview
#### Start Menu
<img src="/screenshot-1.png" width="80%">

#### Maneuver Screen
<img src="/screenshot-2.png" width="80%">

### Stack
**Platform:** Unity

**Language:** C#
