using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class Program
    {
        static void Main(string[] args)
        {

            String[] arrayOfSecretWords = File.ReadAllLines("H:listOfWords.txt");
            List<Char> listOfInvalidGuesses = new List<Char>();
            List<Char> listOfValidGuesses = new List<Char>();

            int lives = 3;

            while (lives > 0) { 
            gameInit(arrayOfSecretWords, lives, listOfValidGuesses, listOfInvalidGuesses);
            }

        }

          static string ReplaceAll(string input, char target)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(target);
            }

            return sb.ToString();
        }

        static void printGuessedWordWithTabs(String word)
        {
            for (int character = 0; character <= word.Length - 1; character++)
            {
                Console.Write(word[character] + " ");
            }
        }

        static void printInvalidGuesses(List<Char> list)
        {

            foreach (Char x in list)
            {
                Console.Write(x + ", ");
            }

        }


        static void gameInit(String[] arrayOfSecretWords, int lives, List<Char> listOfValidGuesses, List<Char> listOfInvalidGuesses) 
        {
            Boolean win = false;
            // Choose randomly a secret word from the array
            String randomSecretWord = arrayOfSecretWords[new Random().Next(0, arrayOfSecretWords.Length)];
            String wordGuessedTillNow = ReplaceAll(randomSecretWord, '_');
            int totalTries = randomSecretWord.Length * 2;

            Console.WriteLine("Welcome to Hangman ver 0.1.\n");
            Console.WriteLine("You have " + lives + " lives.\n");
            Console.WriteLine("Secret word is: ");

            while (win == false && (totalTries != 0) && (lives >= 0))
            {

                printGuessedWordWithTabs(wordGuessedTillNow);

                Console.WriteLine("\n\nInvalid guesses:"); printInvalidGuesses(listOfInvalidGuesses);

                Console.WriteLine("\n\nPlease enter a letter:\n"); 
                Char inputString = Console.ReadKey().KeyChar;

                if (inputString.Equals("!")) { break; }


                Console.WriteLine("\nYou wrote '" + inputString + "'.\n");

                if (listOfInvalidGuesses.Contains(inputString))
                {
                    Console.WriteLine("You already entered this letter and its not correct !\n");
                }
                else if (listOfValidGuesses.Contains(inputString))
                {
                    Console.WriteLine("You already entered this letter and its correct !\n");
                }

                if (randomSecretWord.Contains(inputString))
                {
                    Console.WriteLine("'" + inputString + "' exists in the word!\n");
                    Console.WriteLine("\nTries left: " + totalTries);
                    listOfValidGuesses.Add(inputString);

                    for (var i = 0; i < randomSecretWord.Length; i++)
                    {
                        if (inputString == randomSecretWord[i])
                        {
                            StringBuilder sb = new StringBuilder(wordGuessedTillNow);
                            sb[i] = inputString;
                            wordGuessedTillNow = sb.ToString();

                        }
                    }

                    if (!wordGuessedTillNow.Contains('_'))
                    {
                        win = true;
                    }

                }
                else
                {
                    Console.WriteLine("'" + inputString + "' does not exist in the word!\n");
                    totalTries--;
                    Console.WriteLine("\nTries left: " + totalTries);
                    if (!listOfInvalidGuesses.Contains(inputString))
                    {
                        listOfInvalidGuesses.Add(inputString);
                    }
                }

            }

            printGuessedWordWithTabs(wordGuessedTillNow);
            if (win == true) { Console.WriteLine("\n\nYou Won!!!!! Press Enter to exit.");
            gameInit(arrayOfSecretWords, lives, new List<Char>(), new List<Char>());
            }
            else if(win == false)
            {

                Console.WriteLine("\n\nYou lost! The word was '" + randomSecretWord + "'. \n.");
                lives--;
                if (lives == 0) 
                {
                    Console.WriteLine("Game over!");
                    System.Environment.Exit(1);
                }
                gameInit(arrayOfSecretWords, lives, new List<Char>(), new List<Char>());

            }
            Console.ReadLine();
        }

    }
}