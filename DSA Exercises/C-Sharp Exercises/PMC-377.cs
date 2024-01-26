
// PMC-377 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7


// DSAP-01 Extra: Exercise - 60 Basic Declaration and Algorithm
// Calculate the sum of all the integers of a rectangular matrix except those integers which are located below an intger of value 0
// Function to calculate the sum of matrix elements meeting certain conditions
    public static int sum_matrix_elements(int[][] my_matrix) {
        int x = 0; // Initialize a variable to store the sum of selected matrix elements

        // Loop through each column of the matrix
        for (int i = 0; i < my_matrix[0].Length; i++) {
            // Iterate through each row in the current column until a non-positive element is encountered
            for (int j = 0; j < my_matrix.Length && my_matrix[j][i] > 0; j++) {
                // Add the positive matrix element to the sum
                x += my_matrix[j][i];
            }
        }
        // Return the sum of matrix elements meeting the condition (positive elements)
        return x;
    } //=====================================================================================================
	
// DSAP-02 Extra: Exercise - 59 Basic Declaration and Algorithm
// Check whether it is possible to create a strictly increasing sequence from a given sequence of integers as an array
// Function to test if an array can be made into an increasing sequence with at most one element change
    public static bool test_Increasing_Sequence(int[] int_seq) {
        int x = 0; // Counter to track the number of required changes

        // Loop through the array to check for non-increasing elements and count the changes needed
        for (int i = 0; i < int_seq.Length - 1; i++) {
            // Check if the current element is greater than or equal to the next element
            if (int_seq[i] >= int_seq[i + 1])
                x++; // Increment the counter as a change is needed

            // Check for the possibility of a larger gap (by comparing current and next-next elements)
            if (i + 2 < int_seq.Length && int_seq[i] >= int_seq[i + 2])
                x++; // Increment the counter as a change is needed
        }

        // Return true if the number of required changes is at most 2 (i.e., the array can be made increasing with at most one change)
        return x <= 2;
    } //=====================================================================================================
	
// DSAP-03 Extra: Exercise - 58 Basic Declaration and Algorithm
// Function to count the number of elements required to make an array consecutive
    public static int consecutive_array(int[] input_Array) {
        // Sorting the input array in ascending order
        Array.Sort(input_Array);

        // Counter variable to track the number of elements needed to make the array consecutive
        int ctr = 0;

        // Loop through the sorted array to calculate the number of elements needed to fill the gaps
        for (int i = 0; i < input_Array.Length - 1; i++) {
            // Increment the counter by the difference between adjacent elements minus 1
            // This calculates the number of missing elements between consecutive elements
            ctr += input_Array[i + 1] - input_Array[i] - 1;
        }

        // Return the count of elements needed to make the array consecutive
        return ctr;
    } //=====================================================================================================

// DSAP-04 Extra: Exercise - 56 Basic Declaration and Algorithm
// Function to check if a string is a palindrome
    public static bool checkPalindrome(string inputString) {
        // Convert the input string into a character array
        char[] c = inputString.ToCharArray();

        // Reverse the character array
        Array.Reverse(c);

        // Check if the reversed string is equal to the original input string
        return new string(c).Equals(inputString);
    } //=====================================================================================================

// DSAP-05 Extra: Exercise - 57 Basic Declaration and Algorithm
// Function to find the maximum product of adjacent elements in an array
    public static int adjacent_Elements_Product(int[] input_Array) {
        // Initialize the maximum product with the product of the first two elements in the array
        int max = input_Array[0] * input_Array[1];

        // Loop through the array to find the maximum product of adjacent elements
        for (int x = 1; x <= input_Array.Length - 2; x++) {
            // Update the max variable with the maximum of the current max and the product of the current and next elements
            max = Math.Max(max, input_Array[x] * input_Array[x + 1]);
        }

        // Return the maximum product of adjacent elements
        return max;
    } //=====================================================================================================
	
// DSAP-06 Extra: Exercise - 55 Basic Declaration and Algorithm
// Function to calculate the maximum product of adjacent elements in an array
    public static int array_adjacent_elements_product(int[] input_array) {
        // Initializing variables
        int array_index = 0;

        // Calculating the product of the first two elements in the array
        int product = input_array[array_index] * input_array[array_index + 1];

        // Moving to the next element in the array
        array_index++;

        // Loop to calculate the maximum product of adjacent elements
        while (array_index + 1 < input_array.Length) {
            // Checking if the product of the current adjacent elements is greater than the existing product
            product = ((input_array[array_index] * input_array[array_index + 1]) > product) ?
                       (input_array[array_index] * input_array[array_index + 1]) :
                        product;

            // Moving to the next pair of adjacent elements in the array
            array_index++;
        }

        // Returning the maximum product of adjacent elements in the array
        return product;
    } //=====================================================================================================

// DSAP-07 Extra: Exercise - 28 Basic Declaration and Algorithm
// Reverse the words of a sentence
// // Method to reverse the entire sentence around.
	static string FlipTheSentence(string line) {

		string result = ""; // Initializing an empty string to store the reversed words
		List<string> wordsList = new List<string>(); // Creating a list to store reversed strings

		string[] words = line.Split(new[] { " " }, StringSplitOptions.None); // Splitting the string into individual words

		// Loop to reverse the words and create a new string
		for (int i = words.Length - 1; i >= 0; i--)
			result += words[i] + " "; // Building the reversed string by adding words in reverse order
		

		wordsList.Add(result); // Adding the reversed string to the list

		// Loop to print the reversed string from the list
		foreach (String s in wordsList) 
			result = "\nReverse String: " + s; // Displaying the reversed string
		
		return result;
	} //=====================================================================================================
	
	
// DSAP-08 Extra: Exercise - 26 Basic Declaration and Algorithm
// Check if a number is a Prime
    // Method to check if a number is prime
    public static bool isPrime(int n) {
		
        int x = (int)Math.Floor(Math.Sqrt(n)); // Calculating the square root of 'n'

        if (n == 1) return false; // 1 is not a prime number
        if (n == 2) return true; // 2 is a prime number

        // Loop to check if 'n' is divisible by any number from 2 to square root of 'n'
        for (int i = 2; i <= x; ++i)
            if (n % i == 0) return false; // If 'n' is divisible by 'i', it's not a prime number
        

        return true; // 'n' is prime if not divisible by any number except 1 and itself
    } //=====================================================================================================
	
// DSAP-09 Extra: Exercise - 24 Basic Declaration and Algorithm
// Find the longest word in a string
	static string LongestWords(string line) {

		// Splitting the string into words based on spaces and storing them in an array
		string[] words = line.Split(new[] { " " }, StringSplitOptions.None);

		string word = ""; // Initializing an empty string to store the word with the maximum length
		int ctr = 0; // Initializing a counter variable to keep track of the maximum length

		// Looping through each word in the words array
		foreach (String s in words) {
		
			// Checking if the length of the current word is greater than the stored maximum length
			if (s.Length > ctr) {
			
				word = s; // If the current word's length is greater, update the 'word' variable
				ctr = s.Length; // Update the maximum length counter
			}
		}

		return word; // Displaying the word with the maximum length
		
	} //=====================================================================================================

// DSAP-10 Extra: Exercise - 16 Basic Declaration and Algorithm
// Create a new string from a given string where the first and last characters will change their positions
    public static string FirstLastCharChangePositions(string ustr) {
		
        // Using the ternary operator to rearrange characters based on the length of the string
        return ustr.Length > 1
            ? ustr.Substring(ustr.Length - 1) + ustr.Substring(1, ustr.Length - 2) + ustr.Substring(0, 1)
            : ustr; // Returns the same character for a single-character string
    } //=====================================================================================================
