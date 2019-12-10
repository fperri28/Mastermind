using System;
using System.Collections.Generic;

namespace mastermind
{
	public class Game 
	{
		List<string> clue = new List<string>();
		List<string> guessHistory = new List<string>();
		List<string> clueHistory = new List<string>();

		public void Run() 
		{
			char[] theSecretCode = GenerateSecretCode();
			int playCounter = 1;

			while(playCounter <= 10) 
			{
				char[] currentGuess = QueryUserGuess();
				ComputeClue(currentGuess, theSecretCode);
				DisplayProgress(currentGuess, clue);
				playCounter++;
			}
			if(playCounter > 10) 
			{
				string answer = new string(theSecretCode);
				Console.WriteLine("I'm sorry but you have run out of guesses./n");
				Console.WriteLine("The secret code was: " + answer);
				Console.WriteLine("\nWould you like to play again? (Y/N)");
				string response = Console.ReadLine().ToUpper();
				do 
				{
					if(response.Equals("Y")) 
					{
						Run();
					}
					if(response.Equals("N")) 
					{
						EndMethodProcessing();
					}
				} while (!response.Equals("Y") && (!response.Equals("N")));
			}
		}
		
		public char[] GenerateSecretCode() 
		{
			char[] secretCode = new char[4];
			secretCode = new char[secretCode.Length];
			
			for(int i = 0; i < secretCode.Length; i++) 
			{
				int secretNumber = GetRandomInteger(1, 6);
				secretCode[i] = (char) (secretNumber + 48);
				for (int k = 0; k < i; k++) 
				{
					if (secretCode[k] == secretNumber) 
					{
						i--;
						break;
					}
				}
			}
			return secretCode;
		}
		
		public static int GetRandomInteger(int maximum, int minimum) 
		{
			var rand = new Random();
			return rand.Next(1, 7);
		}
		
		public char[] QueryUserGuess() 
		{
			char[] userGuess = new char[4];
			int counter = 0;
				
			for(int i = 0; counter < userGuess.Length; i++) 
			{
				Console.WriteLine("\nPlease provide a number 1 through 6");
				int userNumber = Int32.Parse(Console.ReadLine());
				if(userNumber == 1 || userNumber == 2 || userNumber == 3 || userNumber == 4 || userNumber == 5 || userNumber == 6) 
				{
					userGuess[counter] = (char) (userNumber + 48);
					counter++;
				} else 
				{
					Console.WriteLine("You entered an invalid number. Please enter only a valid number (1 through 6).");
				}
			}
			return userGuess;
		}
		
		public List<string> ComputeClue(char[] guess, char[] code) 
		{
			clue.Clear();
			int plusCounter = 0;
			int minusCounter = 0;

			string testCode = new string(code);
			char[] tempCode = testCode.ToCharArray();
			
			string testGuess = new string(guess);
			char[] tempGuess = testGuess.ToCharArray();		
					
			for(int i = 0; i < code.Length; i++) 
			{
				if(guess[i] == code[i]) 
				{
					clue.Add("+");
					plusCounter++;
				}
				if(plusCounter == 4) 
				{
					string userPlayChoice;

					Console.WriteLine("Congratulations, you guessed the secret code!");
					Console.WriteLine("\nWould you like to play again? (Y/N)");
					do{
						userPlayChoice = Console.ReadLine().ToUpper();
						if(userPlayChoice.Equals("Y")) 
						{
							Run();
						}
						if(userPlayChoice.Equals("N"))
						{
							EndMethodProcessing();
						}

					} while (!userPlayChoice.Equals("Y") && (!userPlayChoice.Equals("N")));
				}
			}
			for(int i = 0; i < guess.Length; i++) 
			{
				if(tempGuess[i] == tempCode[i]) 
				{
					tempCode[i] = '7';
					tempGuess[i] = '0';
				}
				for(int j = 0; j < tempCode.Length; j++) 
				{
					if(tempGuess[i] == tempCode[j]) 
					{
						minusCounter++;
					}
				}
			}
			if(minusCounter > 0) 
			{
				for(int i = 0; i < minusCounter; i++) 
				{
					clue.Add("-");
				}
			}
			
			return clue;
		}

		public void DisplayProgress(char[] guess, List<string> clue) 
		{
			int counter = 1;
			int index = 0;
			string currentGuess = new string(guess);
			string delim = " ";
			string currentClue = string.Join(delim, clue);
			guessHistory.Add(currentGuess);
			clueHistory.Add(currentClue);
			
			Console.WriteLine();
			Console.Write($"{"ATTEMPTS", -20}");
			Console.Write($"{"GUESS", -20}");
			Console.Write($"{"CLUE", -20}");
			Console.WriteLine();
			Console.WriteLine("================================================");
			
			foreach (string attempts in guessHistory) 
			{

				Console.Write($"{counter, -20}");
				counter++;
				Console.Write($"{attempts, -20}");
				Console.Write($"{clueHistory[index], -20}");
				index++;
				Console.WriteLine();
			}	
		}
		
		public void PrintInstructions() 
		{
			Console.WriteLine("Welcome to Mastermind! Would you like a quick tutorial? Y/N");
			string userTutorialChoice;
			string userPlayChoice;

			do 
			{
				userTutorialChoice = Console.ReadLine().ToUpper();
				if(userTutorialChoice.Equals("Y")) 
				{
					Console.WriteLine(" You have 10 attempts to guess the secret code! \n " +
							"The secret code is 4 numbers, randomly chosen, between 1 and 6. \n " +
							"If you guess a number correctly a '-' will be displayed. \n " +
							"If you guess a number correctly and in the correct postition a '+' will be displayed. \n " +
							"The game will end if you guess all 4 numbers correctly and in the correct order. \n " +
							"Or the game will end if you run out of tries. \n " +
							"Are you ready to play? (Y/N)");
					do
					{
						userPlayChoice = Console.ReadLine().ToUpper();
						if(userPlayChoice.Equals("Y")) 
						{
							break;
						}
						if(userPlayChoice.Equals("N")) 
						{
							EndMethodProcessing();;
						}
					} while (!userTutorialChoice.Equals("Y") && (!userTutorialChoice.Equals("N")));
				}
				if(userTutorialChoice.Equals("N")) 
				{
					Console.WriteLine("Would you like to play(Y/N)");
					do 
					{
						userPlayChoice = Console.ReadLine().ToUpper();
						if(userPlayChoice.Equals("Y")) 
						{
							break;
						} 
						if(userPlayChoice.Equals("N")) 
						{
							EndMethodProcessing();
						}
					} while (!userPlayChoice.Equals("Y") && (!userPlayChoice.Equals("N")));
				}
			} while (!userTutorialChoice.Equals("Y") && (!userTutorialChoice.Equals("N")));
		}
		
		public static void EndMethodProcessing() 
		{
			Console.WriteLine("Thanks for playing.");
			Environment.Exit(0);
		}
	}
}