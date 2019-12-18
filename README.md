# 						GE1_Ass.Repo_2019

## 						Repository for 2019 assignment



## 						Concept Proposal:


	An infinitely generating hologram terrain with star particles and a central circular music beat and frequency visualizer.

	Visually it will be a combination of these: 

[![YouTube](http://img.youtube.com/vi/N8Ql7aYBQP8/0.jpg)](https://www.youtube.com/watch?v=N8Ql7aYBQP8)

[![YouTube](http://img.youtube.com/vi/vFvwyu_ZKfU/0.jpg)](https://www.youtube.com/watch?v=vFvwyu_ZKfU)

[![YouTube](http://img.youtube.com/vi/apPGPLQnOV8/0.jpg)](https://www.youtube.com/watch?v=apPGPLQnOV8)

### Stretch Goals: 

~~1. Randomly Rotating polyhedron in the center of the screen.~~


# 						Technical Report, 'Orbit Flyer'

### 						Game Engines 1 Assignment Submission

### 						Oisin Fitzpatrick


## 						Project Overview:

		'Orbit Flyer' is a concept music visualizer featuring a faux solar system of enemy planets. 
		The player can use an Xbox One Controller to fly around a limited area, avoiding enemy planets,
		and exploring the small environment. Orbiting planets will attack the player when they get too 
		close by shooting plasma balls. A central music visualizer surrounds the virtual sun, and the 
		sun and the planets respond to sound beats from the music. The player can randomly choose to play
		different music. There is a perlin noise terrain at the bottom of the play-space. 

### 						Credits:

	1. 	For Orbiting Motion this tutorial series was crucial:

[![YouTube](http://img.youtube.com/vi/mQKGRoV_jBc/0.jpg)](https://www.youtube.com/watch?v=mQKGRoV_jBc)


	2. 	For The Perlin noise generated terrain, this video by brackeys breaks down the process:

[![YouTube](http://img.youtube.com/vi/vFvwyu_ZKfU/0.jpg)](https://www.youtube.com/watch?v=vFvwyu_ZKfU)

	3. 	For the planet projectiles I used this tutorial to implement Ammo Pools

[![YouTube](http://img.youtube.com/vi/tdSmKaJvCoA/0.jpg)](https://www.youtube.com/watch?v=tdSmKaJvCoA)

	4. 	For the general idea on using a public class for audio visualization this tutorial was useful:

[![YouTube](http://img.youtube.com/vi/Ri1uNPNlaVs/0.jpg)](https://www.youtube.com/watch?v=Ri1uNPNlaVs) 
   
 		I also used the Game Engines 1 repo for its audio example for the beat sampling

	5. 	The Hologram shader code was created from this tutorial:
	
[![YouTube](http://img.youtube.com/vi/vlYGmVC_Qzg/0.jpg)](https://www.youtube.com/watch?v=vlYGmVC_Qzg)


###						Music References:

_Dynatron 'Pulse Power'_

_Eva 失望した_

_MK Ultra Tears in The Rain_

_Vodovoz Blue Journey_

_Vodovoz Drive By Night_


## 						Instructions:

**Right Trigger 		- Accelerate**

**Left Trigger  		- Brake**

**Right Analog (left-right)  	- Volume Control**

**Left Analog   		- Angle Ship**

**X Button 			- Start/Switch Music**


## 						Implementation

###	1. 					Player ship:

The player ship is constructed from basic unity shapes combined together. There are trail renderers on the engines. There is also
 a metallic material on the ship. The movement is created by simply adding a rigidbody to the player, disabling the gravity and constantly
adding force in the forward direction of the player. The player can boost and brake using the Xbox One controller. To steer, the input axis of the left analog stick
is utilised to rotate the player object. I also implemented a system to reduce manueverability of steering when the player boosts. The FOV of the camera also changes to 
enhance the sensation of speed of the player. The player is also able to alter the volume of the audiosource using the the right analog stick.

### 	2.					Central Visualizer

The central visualizer is created at runtime. Prefab segments are spawned in a circle. In order to only scale in one direction,
the actual cube to visualize the sound is childed to a parent object. By placing the parent at the base of a cube, when the parent object is scaled in its z axis,
it means the child cube is stretch around the center of the parent object, meaning it stretches in one direction. The Visualizer accesses a public class which gathers
the audio sampling data for the entire project. The scale of each of the 256 segments is altered by one of the samples from the first 64 samples in the samples array.
I decided to repeat the first 64 samples because it made the visualizer more interesting. If I used all 256 samples, most of the visualizer
would not be scaling much to the music, being rather boring. The Visualizer also rotates to  follow the player position, meaning that it can be viewed from 
any position when looked at.

