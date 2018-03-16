This github repository was created for the Creating Reality hackathon at USC in March 2018.  

It was contributed to by:
David Geisert
J Peter Jones
Ryan Lebar
Cindy Reichel
Nick Roewe

To set up the project you will need to clone the repositry and then set up the Watson and Photon with your own keys. 

Watson keys can be found in Assets/Watson/Examples/ServiceExamples/Scripts/ExampleStreaming.cs
you will need to change the values for _username, _password, and _url
you can get a free watson account by going to console.bluemix.net then signing up with ibm.  You will need to create a resource, then choose speech to text.  after creating the resource you will need to create credentials for it and those credentials are what go into the ExampleStreaming.cs script

You will also need to sign up for Photon.  This can be done at https://www.photonengine.com/en/PUN.  You can then use the pun setup wizard in Unity to complete the photon setup.

To run the project as intended you should open the scene Intro found in the Custom folder in the Unity project.