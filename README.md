cheks
=====

CHEKS - CHaotic Encryption Key System

/* -------------------------------------------------------- */
/* ------------- Description ------------------------------ */
/* -------------------------------------------------------- */
This project is implementing a different way of generating an managing encryption keys.
It is using a very simple chaotic system generator to generate ever changing sets of private keys.

/* -------------------------------------------------------- */
/* ------------- Status of the project -------------------- */
/* -------------------------------------------------------- */

It is at the proof of concept stage and still need to be improved.

/* -------------------------------------------------------- */
/* -------------- Structure of the files ------------------ */
/* -------------------------------------------------------- */

You will find 4 different .net projects in this project.

1. CHEKSEngine : A very, very simple chaotic system simulator, also containing the key management and encryption classes.
2. CHEKSInitializer : Allows to randomly initialize system definitions according to specific parameters.
3. CHEKSVisualizer : Useless tool to visualize the current state of a system.
4. CHEKSClient : Simple test message exchange application to test the CHEKSEngine.
