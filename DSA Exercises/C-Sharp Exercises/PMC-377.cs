
// PMC-377 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic-algo/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7



// DSAP-07 Extra: Exercise - 28 Basic Declaration and Algorithm
// Reverse the words of a sentence
// // Method to reverse the entire sentence around.
	static string FlipTheSentence(string line) {

		string result = ""; // Initializing an empty string to store the reversed words
		List<string> wordsList = new List<string>(); // Creating a list to store reversed strings

		string[] words = line.Split(new[] { " " }, StringSplitOptions.None); // Splitting the string into individual words

		// Loop to reverse the words and create a new string
		for (int i = words.Length - 1; i >= 0; i--) {
			result += words[i] + " "; // Building the reversed string by adding words in reverse order
		}

		wordsList.Add(result); // Adding the reversed string to the list

		// Loop to print the reversed string from the list
		foreach (String s in wordsList) {
			result = "\nReverse String: " + s; // Displaying the reversed string

		}
		return result;
	}
	
	
// DSAP-08 Extra: Exercise - 26 Basic Declaration and Algorithm
// Check if a number is a Prime
    // Method to check if a number is prime
    public static bool isPrime(int n)
    {
        int x = (int)Math.Floor(Math.Sqrt(n)); // Calculating the square root of 'n'

        if (n == 1) return false; // 1 is not a prime number
        if (n == 2) return true; // 2 is a prime number

        // Loop to check if 'n' is divisible by any number from 2 to square root of 'n'
        for (int i = 2; i <= x; ++i)
        {
            if (n % i == 0) return false; // If 'n' is divisible by 'i', it's not a prime number
        }

        return true; // 'n' is prime if not divisible by any number except 1 and itself
    }
	
// DSAP-09 Extra: Exercise - 24 Basic Declaration and Algorithm
// Find the longest word in a string
	static string LongestWords(string line){

		// Splitting the string into words based on spaces and storing them in an array
		string[] words = line.Split(new[] { " " }, StringSplitOptions.None);

		string word = ""; // Initializing an empty string to store the word with the maximum length
		int ctr = 0; // Initializing a counter variable to keep track of the maximum length

		// Looping through each word in the words array
		foreach (String s in words){
		
			// Checking if the length of the current word is greater than the stored maximum length
			if (s.Length > ctr){
			
				word = s; // If the current word's length is greater, update the 'word' variable
				ctr = s.Length; // Update the maximum length counter
			}
		}

		return word; // Displaying the word with the maximum length
		
	} //=====================================================================================================

// DSAP-10 Extra: Exercise - 16 Basic Declaration and Algorithm
// Create a new string from a given string where the first and last characters will change their positions
    public static string FirstLastCharChangePositions(string ustr)
    {
        // Using the ternary operator to rearrange characters based on the length of the string
        return ustr.Length > 1
            ? ustr.Substring(ustr.Length - 1) + ustr.Substring(1, ustr.Length - 2) + ustr.Substring(0, 1)
            : ustr; // Returns the same character for a single-character string
    } //=====================================================================================================
