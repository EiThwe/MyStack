using System;

namespace ArrayStackDemo
{
    public class MyStack
    {
        private int[] items;    // array to hold data
        private int top;        // index of top element

        public int Count => top + 1;

        // Constructor
        public MyStack(int size)
        {
            items = new int[size];
            top = -1; // empty
        }

        public bool IsEmpty() => top == -1;
        public bool IsFull() => top == items.Length - 1;

        public void Push(int value)
        {
            if (!IsFull())
            {
                items[++top] = value;
                
            } else
            {
                Console.WriteLine("❌ Stack overflow.");
            }
            

        }

        public int Pop()
        {
            if (!IsEmpty())
            {

               return items[top--];

            } else throw new InvalidOperationException("❌ Stack underflow.");

           
        }

        public int Search(int target)
        {
            for (int i = top; i >= 0; i--) // search from top to bottom
            {
                if (items[i] == target)
                    return i;
            }
            return -1;
        }


        public void Display()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty.");
                return;
            }

            Console.WriteLine("Stack (top to bottom):");
            for (int i = top; i >= 0; i--)
                Console.WriteLine($"  {items[i]}");
        }
    }

    class Program
    {
        static void Main()
        {
            int stackSize = ReadStackSize();

            // Create the stack
            MyStack stack = new MyStack(stackSize);

            // Get and validate numbers
            string[] parts = ReadValidUniqueNumbers(stackSize);

            // Push them onto the stack
            foreach (string part in parts)
            {
                int number = int.Parse(part);
                stack.Push(number);
            }

            // Display stack
            Console.WriteLine("\nYour Stack Contents are:");
            stack.Display();

            // Search and substitute
            HandleSearchAndSubstitute(stack);

            // Display updated stack
            Console.WriteLine("\nYour Current Stack Contents are:");
            stack.Display();
        }
        
        /*------------Helper Functions-------------*/

        static int ReadStackSize()
        {
            int stackSize;
            bool isStackSizeValid;
            string? input;

            do
            {
                Console.Write("Enter the stack size you would like to create (positive number only): ");
                input = Console.ReadLine();
                isStackSizeValid = int.TryParse(input, out stackSize) && stackSize > 0;

                if (!isStackSizeValid)
                {
                    Console.WriteLine("Invalid input. Please enter a positive whole number.\n");
                }

            } while (!isStackSizeValid);

            return stackSize;
        }

        static string[] ReadValidUniqueNumbers(int expectedCount)
        {
            string[] parts;
            bool isNumbersValid;

            do
            {
                isNumbersValid = true;

                Console.Write($"Enter {expectedCount} unique numbers (comma or space separated): ");
                string numbers = Console.ReadLine() ?? "";

                parts = numbers.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != expectedCount)
                {
                    Console.WriteLine($"Please enter exactly {expectedCount} numbers.\n");
                    isNumbersValid = false;
                }
                else
                {
                    var seen = new HashSet<int>();

                    foreach (var part in parts)
                    {
                        if (!int.TryParse(part, out int num))
                        {
                            Console.WriteLine($"'{part}' is not a valid number.\n");
                            isNumbersValid = false;
                        }

                        if (!seen.Add(num))
                        {
                            Console.WriteLine($"Duplicate value '{num}' detected.\n");
                            isNumbersValid = false;
                        }
                    }
                }

            } while (!isNumbersValid);

            return parts;
        }

        static void HandleSearchAndSubstitute(MyStack stack)
        {
            int target;
            int index;

         
            do
            {
                Console.WriteLine("\nEnter a number to search for.");
                Console.Write("All numbers above it in the stack will be removed and it will be replaced with a new number: ");

                target = int.Parse(Console.ReadLine() ?? "0");

                index = stack.Search(target);

                if (index == -1)
                {
                    Console.WriteLine("Your number isn't found. Please try again.");
                }

            } while (index == -1);

            int indexFromTop = stack.Count - index;

            if (indexFromTop == 1)
                Console.WriteLine("Your number is found at the top of the stack");
            else if (indexFromTop == stack.Count)
                Console.WriteLine("Your number is found at the bottom of the stack");
            else
                Console.WriteLine($"Your number is found at place : {indexFromTop} from top of the stack");

            int popsNeeded = indexFromTop - 1;

            for (int i = 0; i < popsNeeded; i++)
            {
                stack.Pop(); // remove items above the target
            }

            Console.Write("\nEnter a number to substitute: ");
            int subNum = int.Parse(Console.ReadLine() ?? "0");

            stack.Pop();         
            stack.Push(subNum);  
        }


    }
}
