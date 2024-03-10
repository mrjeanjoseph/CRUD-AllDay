
// PMC-395 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7


// DSAP-01 Extra: Exercise - 98 Basic Declaration and Algorithm
// Create and display all prime numbers in strictly descending decimal digit order.
	// Method to check if a number is prime
	public static bool IsPrime(uint n) 	{
		if (n <= 1) 
			return false;		

		int ctr = 0;
		
		for (int i = 1; i <= n; i++) {
			if (n % i == 0)
				ctr++;			
			
			if (ctr > 2) 
				return false;			
		}

		return true;
	}
	
	
	static void PrimeNumberInAscOrder() {
		var Q = new Queue<uint>(); // Queue to store numbers
		var prime_nums = new List<uint>(); // List to store prime numbers

		// Enqueue initial numbers from 1 to 9 into the queue
		for (uint i = 1; i <= 9; i++) {
			Q.Enqueue(i);
		}

		// Continue while the queue is not empty
		while (Q.Count > 0) {
			// Dequeue a number
			uint n = Q.Dequeue();

			// Check if the dequeued number is prime and add it to the list of prime numbers
			if (IsPrime(n))
				prime_nums.Add(n);
			

			// Enqueue the next potential prime numbers formed by appending digits from 1 to 9
			for (uint i = n % 10 + 1; i <= 9; i++) {
				Q.Enqueue(n * 10 + i);
			}
		}

		// Display the generated prime numbers
		foreach (uint p in prime_nums) {
			Console.Write(p);
			Console.Write(", ");
		}
		
		Console.WriteLine();
	}

	static void PrimeNumberInDescOrder() {
		uint z = 0; // Counter variable for prime numbers
		int nc; // Variable to hold the count of numbers

		// Array of prime numbers
		var p = new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		var nxt = new uint[128]; // Array for next potential prime numbers

		while (true) {
			nc = 0; // Reset the count of numbers

			// Loop through each number in the array of prime numbers
			foreach (var x in p) {
				// Check if the number is prime and display it
				if (IsPrime(x))
					Console.Write("{0,8}{1}", x, ++z % 5 == 0 ? "\n" : " ");

				// Generate next potential prime numbers
				for (uint y = x * 10, l = x % 10 + y++; y < l; y++)
					nxt[nc++] = y;
			}

			// Check if there are more than one potential prime numbers
			if (nc > 1) {
				// Resize the prime number array and copy next potential primes
				Array.Resize(ref p, nc);
				Array.Copy(nxt, p, nc);
			} else {
				// Break the loop if there are no more potential primes
				break;
			}
		}

		// Output the number of descending primes found
		Console.WriteLine("\n{0} descending primes found", z);
	}//-------------------------------------------------------------------------------------------------

// DSAP-02 Extra: Exercise - 93 Basic Declaration and Algorithm
// Check and return true if all the characters in the string are the same, otherwise false.
	// Method to check if all characters in a string are the same
	public static bool StringWithSameCharacters(string text) {
		// Check if the length of the string is greater than 1
		if (text.Length > 1) {
			var b = text[0]; // Store the first character of the string

			// Loop through the characters starting from the second character
			for (int i = 1; i < text.Length; i++) {
				var c = text[i]; // Store the current character

				// If the current character is not equal to the first character, return false
				if (c != b) 
					return false;				
			}
		}
		
		return true; // Return true if all characters are the same or if the string has only one character
	}//-------------------------------------------------------------------------------------------------


// DSAP-03 Extra: Exercise - 93 Basic Declaration and Algorithm
// Check if string is valid or not. The input string will be valid when open brackets and closed brackets are same type of brackets.
	// Method to verify if a string contains valid parentheses
	public static bool ValidParentheses(string text) {
		string temp_text = string.Empty;

		// Loop continues until the text doesn't change anymore after replacements
		while (text != temp_text) {
			temp_text = text;

			// Replacing pairs of parentheses with an empty string in each iteration
			text = text.Replace("<>", "").Replace("()", "").Replace("[]", "").Replace("{}", "");
		}

		// If the final text is empty, all parentheses are balanced, return true; otherwise, return false
		return text == string.Empty ? true : false;
	}//-------------------------------------------------------------------------------------------------


// DSAP-04 Extra: Exercise - 93 Basic Declaration and Algorithm
// Find the longest common prefix from an array of strings
	// Method to find the longest common prefix among a string array
	public static string LongestCommonPrefix(string[] arr_strings) {
		// Checking for edge cases: empty array or array containing an empty string
		if (arr_strings.Length == 0 || Array.IndexOf(arr_strings, "") != -1)
			return "";

		// Initializing 'result' to the first string in the array
		string result = arr_strings[0];
		int i = result.Length;

		// Looping through each word in the array to find the longest common prefix
		foreach (string word in arr_strings) {
			int j = 0;

			// Comparing characters at each position in the words
			foreach (char c in word) {
				// Breaking if characters don't match or index 'j' exceeds the length of 'result'
				if (j >= i || result[j] != c)
					break;
				j += 1;
			}

			// Updating 'i' with the minimum value between 'i' and 'j'
			i = Math.Min(i, j);
		}

		// Returning the longest common prefix based on 'i'
		return result.Substring(0, i);
	}//-------------------------------------------------------------------------------------------------
		

// DSAP-05 Extra: Exercise - 93 Basic Declaration and Algorithm
// Calculate the square root of a given number. Return the integer part of the result instead of using any built-in functions.
	// Method to calculate the square root of a given number 'n'
	public static int SquareRootOfGivenNumber(double n) {
		int sq = 1;

		// Loop to find the square root using an iterative approach
		while (sq < n / sq)
			sq++;		

		// Checking if the square is greater than 'n/square'. If so, returns the square - 1
		if (sq > n / sq) 
			return sq - 1;
		return sq; // Returning the square root of 'n'
	}//-------------------------------------------------------------------------------------------------		


// DSAP-06 Extra: Exercise - 92 Basic Declaration and Algorithm
// Find the next prime number of a given number. If the given number is a prime number, return the number.
	// Method to find the next prime number or the current prime number of a given number 'n'
	public static int NextPrimeNumber(int n) {
		for (int i = 2; i < n; i++) {
			// Checking if 'n' is divisible by 'i', if true, increment 'n' and reset 'i' to 2
			if (n % i == 0) { 
				n++;
				i = 2;
			}
		}
		
		return n; // Returning the next or current prime number
	}//-------------------------------------------------------------------------------------------------


// DSAP-07 Extra: Exercise - 86 Basic Declaration and Algorithm
// Return the number of letters and digits in a given string
	// Method to count the number of letters and digits in a string
	public static string NumberOfLettersAndDigits(string text) {
		// Counting the number of letters in the given 'text' using LINQ
		int ctr_letters = text.Count(char.IsLetter);

		// Counting the number of digits in the given 'text' using LINQ
		int ctr_digits = text.Count(char.IsDigit);

		// Returning the count of letters and digits as a string
		return "Number of letters: " + ctr_letters + "  Number of digits: " + ctr_digits;
	}//-------------------------------------------------------------------------------------------------


// DSAP-08 Extra: Exercise - 85 Basic Declaration and Algorithm
// Cumulatively sum of an array of numbers
	// Method to calculate cumulative sum of an array of doubles
	public static double[] CumulativelySumNumbers(double[] nums) {
		// Loop to compute cumulative sum by adding current element to the previous element
		for (int i = 1; i < nums.Length; i++) {
			
			nums[i] = nums[i] + nums[i - 1];
		}

		return nums; // Returning the array with cumulative sum values
    }//-------------------------------------------------------------------------------------------------


// DSAP-09 Extra: Exercise - 78 Basic Declaration and Algorithm
// Sum of squares of elements of a given array of numbers
	// Method to calculate the sum of squares of elements in an array using a for loop
	public static int FindSumSquares(int[] nums) {
		int squaresSum = 0; // Variable to hold the sum of squares

		// Looping through the elements of the array
		for (int i = 0; i < nums.Length; i++) {
			// Calculating the square of each element and adding it to the sum
			squaresSum = squaresSum + (int)Math.Pow(nums[i], 2);
		}

		return squaresSum; // Returning the sum of squares
    }//-------------------------------------------------------------------------------------------------
	

// DSAP-10 Extra: Exercise - 74 Basic Declaration and Algorithm
// Check the length of a given string is odd or even
	// Function to determine if the length of a string is even or odd
	public static string LengthOddOrEven(string word) {
		int length = word.Length; // Get the length of the input string

		// Check if the length is even or odd using the modulus operator (%)
		if (length % 2 == 0) // If the remainder is 0, the length is even
			return "Even length";
		else // If the remainder is not 0, the length is odd
			return "Odd length";
		
    }//-------------------------------------------------------------------------------------------------
	

// DSAP-11 Extra: Exercise - 71 Basic Declaration and Algorithm
// Check if a given string contains two similar consecutive letters
	// Function to test if a string contains consecutive similar letters
	public static bool TwoSimilarConsecutiveLetters(string text) {
		// Iterates through the characters of the string except the last character
		for (int i = 0; i < text.Length - 1; i++) {
			// Checks if the current character is the same as the next character
			// If consecutive similar letters are found, returns true
			if (text[i] == text[i + 1]) 
				return true;			
		}
		
		// Returns false if no consecutive similar letters are found in the string
		return false;
		
		// Another option would be to:
		// 	Uses Regex.IsMatch to check if the input string matches a pattern
		// 	The pattern (.)\1 matches any character (captured by the group (.) represented by \1) that repeats consecutively
		// 	If a match is found, it returns true, indicating consecutive similar letters
		// 	return Regex.IsMatch(text, @"(.)\1");
    }//-------------------------------------------------------------------------------------------------


// DSAP-12 Extra: Exercise - 65 Basic Declaration and Algorithm
// Multiply all of elements of a given array of numbers by the array length
	// Function to multiply each element of the input array by the array length
	public static int[] MultiplyAllElement(int[] nums) {
		// Get the length of the input array
		var arr_len = nums.Length;

		// Using LINQ's Select method to multiply each element by the array length
		// and converting it back to an array using ToArray()
		return nums.Select(el => el * arr_len).ToArray();
	}
	
	// Function to multiply each element of the input array by the array length
	public static int[] MultiplyAllElement_OptionTwo(int[] nums) {
		// Loop through each element of the input array
		for (int i = 0; i < nums.Length; i++) {
			nums[i] *= nums.Length; // Multiply each element by the length of the array
		}

		return nums; // Return the modified array
    }//-------------------------------------------------------------------------------------------------


// DSAP-13 Extra: Exercise - 62 Basic Declaration and Algorithm
// Reverse the strings contained in each pair of matching parentheses in a given string.
		//It should also remove the parentheses from the given string.
// Function to reverse and remove parentheses from a string
    public static string ReverseRemoveParentheses(string str) {
        // Find the last index of opening parenthesis '('
        int lid = str.LastIndexOf('(');

        // If no '(' is found, return the original string
        if (lid == -1) {
            return str;
        } else {
            // Find the corresponding closing parenthesis ')' for the found '('
            int rid = str.IndexOf(')', lid);

            // Recursively process the substring inside the parentheses and reverse it
            return reverse_remove_parentheses(
                str.Substring(0, lid) +
                new string(str.Substring(lid + 1, rid - lid - 1).Reverse().ToArray()) +
                str.Substring(rid + 1)
            );
        }
    }//-------------------------------------------------------------------------------------------------


// DSAP-14 Extra: Exercise - 61 Basic Declaration and Algorithm
// Sort the integers in ascending order without moving the number -5
// Function to calculate the sum of matrix elements meeting certain conditions
    public static int[] SortNumbersInASC(int[] arra) {
        // Extract and sort non-negative numbers (excluding -5)
        int[] num = arra.Where(x => x != -5).OrderBy(x => x).ToArray();

        int ctr = 0; // Counter for non-negative numbers used in sorting

        // Map sorted non-negative numbers back to the array while preserving -5 values
        return arra.Select(x => x >= 0 ? num[ctr++] : -5).ToArray();
    }//-------------------------------------------------------------------------------------------------
	
	
// DSAP-15 Extra: Exercise - 100 Basic Declaration and Algorithm
// Check the equality comparison (value and type) of two parameters. Return true if they are equal otherwise false.
	// Method to test equality of two objects
	public static bool EqualityComparison(object x, object y) {
		// Check if the objects are equal
		if (!x.Equals(y))
			return false;		

		// Check if the objects are of the same type
		if (!x.GetType().Equals(y.GetType()))
			return false;		

		// If both checks pass, return true
		return true;
	}//-------------------------------------------------------------------------------------------------
	
// DSAP-16 Extra: Exercise - 100 Basic Declaration and Algorithm
// Create a identity matrix
	static void CreateAnIdentityMatrix() {
		int n;

		// Prompt the user to input a number
		Console.WriteLine("Input a number:");

		// Read the input from the user and convert it to an integer
		n = Convert.ToInt32(Console.ReadLine());

		// Create a square matrix of size n x n filled with zeroes except for diagonal elements (identity matrix)
		var M = Enumerable.Range(0, n)
				.Select(i =>
					Enumerable.Repeat(0, n)
						.Select((z, j) => j == i ? 1 : 0) // Sets diagonal elements to 1, rest to 0
						.ToList()
				)
				.ToList();

		// Display the matrix (identity matrix) generated above
		foreach (var row in M) {
			foreach (var element in row) {
				Console.Write(" " + element); // Print each element of the matrix row
			}
			Console.WriteLine(); // Move to the next line after printing each row
		}
	}//-------------------------------------------------------------------------------------------------
	
// DSAP-16 Extra: Exercise - 103 Basic Declaration and Algorithm
// Sort characters in a string
	// Method to sort and concatenate characters and digits in a string
	public static string SortCharacters(string text) {
		
		// Check if the input string is null, empty, or consists only of whitespace characters
		bool flag = string.IsNullOrWhiteSpace(text);

		// If the string is blank, return "Blank string!"
		if (flag)
			return "Blank string!";

		// Extract digits and sort them in ascending order
		var text_nums = text.Where(char.IsDigit).OrderBy(el => el).ToList();

		// Extract letters, convert them to lowercase, sort them alphabetically, then by descending original character order
		var text_chars = text.Where(char.IsLetter)
			.Select(el => new { l_char = char.ToLower(el), _char = el })
			.OrderBy(el => el.l_char)
			.ThenByDescending(el => el._char)
			.ToList();

		// Concatenate the sorted characters and digits and return the resulting string
		return new string(text_chars.Select(el => el._char).Concat(text_nums).ToArray());
	}//-------------------------------------------------------------------------------------------------