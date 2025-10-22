using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Learning;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    /// <summary>
    /// State machine for tracking calculator input flow
    /// CRITICAL: Prevents bugs like pressing multiple operations in a row and when user type "53" when user type "5 + 3"
    /// </summary>
    public enum CalculatorState
    {
        EnteringFirstNumber,    // initial state, building first operand      
        OperationSelected,      // operation chose, waiting for second number
        EnteringSecondNumber,   // building second operand
        ResultDisplayed         // calculation complete, ready for new operation
    }

    /// <summary>
    /// Calculator mode determines which features are available
    /// </summary>
    public enum CalculatorMode
    {
        Basic,
        Scientific,
        Programmer
    }   

    public partial class MainWindow : Window
    {
        // MEMORY MANAGEMENT: Keep all calculator instances alive
        // Reason: Switching modes preserves user's calculation history
        // Memory cost: ~20KB total (negligible for modern systems)

        private BasicCalculator basicCalculator;
        private ScientificCalculator scientificCalculator;
        private ProgrammerCalculator programmerCalculator;
        private NumberConverter numberConverter;

        //current active calculator reference
        private BasicCalculator activeCalculator;

        //STATE MANAGEMENT: Critical for proper button behavior
        private CalculatorState currentState;
        private CalculatorMode currentMode;

        //DISPLAY MANAGEMENT
        private string displayString;
        private const int MAX_DISPLAY_LENGTH = 16; // Limit: 1 quadrillion (10^15) plus sign


        public MainWindow()
        {
            InitializeComponent();
            InitializeCalculators();
            InitializeEventHandlers();
        }

        private void InitializeCalculators() {
            basicCalculator = new BasicCalculator();
            scientificCalculator = new ScientificCalculator();
            programmerCalculator = new ProgrammerCalculator();
            numberConverter = new NumberConverter();

            // Start in Basic mode
            activeCalculator = basicCalculator;
            currentMode = CalculatorMode.Basic;
            currentState = CalculatorState.EnteringFirstNumber;
            displayString = "0";
            UpdateDisplay();
        }

        /// <summary>
        /// wire up event handlers for all buttons
        /// MEMORY SAFETY: No need to unsubscribe because buttons live as long as window
        /// When window is destroyed, all button references are also destroyed automatically
        /// </summary>

        private void InitializeEventHandlers()
        {
            // Number buttons (0 - 9) - Single event handler for all
            Button0.Click += NumberButton_Click;
            Button1.Click += NumberButton_Click;
            Button2.Click += NumberButton_Click;
            Button3.Click += NumberButton_Click;
            Button4.Click += NumberButton_Click;
            Button5.Click += NumberButton_Click;
            Button6.Click += NumberButton_Click;
            Button7.Click += NumberButton_Click;
            Button8.Click += NumberButton_Click;
            Button9.Click += NumberButton_Click;

            // Decimal point button
            ButtonDecimal.Click += DecimalButton_Click;

            // Operation buttons (+, -, *, /) - Single event handler for all
            ButtonAdd.Click += OperationButton_Click;
            ButtonSubstract.Click += OperationButton_Click;
            ButtonMultiply.Click += OperationButton_Click;
            ButtonDivide.Click += OperationButton_Click;

            // Special function buttons
            ButtonEqual.Click += ButtonEqual_Click;
            ButtonClear.Click += ButtonClear_Click;
            //ButtonBackspace.Click += ButtonBackspace_Click;
            ButtonClearEntry.Click += ButtonClearEntry_Click;

        }


        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string digit = clickedButton.Content.ToString();

            //STATE MACHINE: Determine behavior based on current state
            switch (currentState) {
                case CalculatorState.EnteringFirstNumber:
                    // Building first number
                    AppendDigit(digit);
                    break;
                case CalculatorState.OperationSelected:
                    // Operation was just selected, start fresh with second number
                    displayString = digit;
                    currentState = CalculatorState.EnteringSecondNumber;
                    UpdateDisplay();
                    break;
                case CalculatorState.EnteringSecondNumber:
                    AppendDigit(digit);
                    break;
                case CalculatorState.ResultDisplayed:
                    // Result was shown, start completely new calculation
                    displayString = digit;
                    currentState = CalculatorState.EnteringFirstNumber;
                    UpdateDisplay();
                    break;
            }
        }

        /// <summary>
        /// Appends digit to display with length validation
        /// PERFORMANCE : Prevents string overflow (max 16 chars)
        /// </summary>
        private void AppendDigit(string digit)
        {
            //INPUT VALIDATION: Limit display length
            if (displayString.Length >= MAX_DISPLAY_LENGTH)
            {
                return; //Silently ignore if too long
            }

            //Remove leading zero for cleaner display
            if (displayString == "0" && digit != ".")
            {
                displayString = digit;
            }
            else 
            {
                displayString += digit;
            }

            UpdateDisplay();

        }
        
        /// <summary>
        /// Updates the display TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDisplay()
        {
            displayTextBox.Text = displayString;
        }

        /// <summary>
        /// Decimal button click handler 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            switch (currentState)
            {
                case CalculatorState.OperationSelected:
                    //User just pressed an operator ( + - * , etc)
                    displayString = "0.";
                    currentState = CalculatorState.EnteringSecondNumber;
                    break;
                case CalculatorState.ResultDisplayed:
                    //User just saw a result and now presses "."
                    displayString = "0.";
                    currentState = CalculatorState.EnteringFirstNumber;
                    break;
                case CalculatorState.EnteringFirstNumber:
                case CalculatorState.EnteringSecondNumber:
                    // only add "." if it doesn't already exist
                    if (!displayString.Contains("."))
                        displayString += ".";
                    break;
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Handles operation button clicks (+, -, *, /)
        /// CRITICAL: Support chaining operations (5 + 3 + 2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string operation = clickedButton.Content.ToString();

            // Convert display symbols to internal operation strings
            string internalOperation = ConvertOperationSymbol(operation);

            try
            {
                switch (currentState)
                {
                    case CalculatorState.EnteringFirstNumber:
                        // First operation in sequence
                        long firstNum = ParseDisplayToLong();
                        activeCalculator.SetFirstNumber(firstNum);
                        activeCalculator.SetOperation(internalOperation);
                        currentState = CalculatorState.OperationSelected;
                        break;
                    case CalculatorState.EnteringSecondNumber:
                        // User pressed: 5 + 3 + (chaining operations)
                        // Calculate current operation first
                        long secondNum = ParseDisplayToLong();
                        activeCalculator.SetSecondNumber(secondNum);

                        long result = CalculateWithExceptionHandling();
                        // Use result as first number for next operation
                        displayString = result.ToString();
                        UpdateDisplay();
                        activeCalculator.SetFirstNumber(result);
                        activeCalculator.SetOperation(internalOperation);
                        currentState = CalculatorState.OperationSelected;
                        break;
                    case CalculatorState.ResultDisplayed:
                        // Continue from previous result
                        long resultNum = ParseDisplayToLong();
                        activeCalculator.SetFirstNumber(resultNum);
                        activeCalculator.SetOperation(internalOperation);
                        currentState = CalculatorState.OperationSelected;
                        break;
                }
            }
            catch (FormatException)
            {
                ShowError("Invalid number format");
                ResetCalculator();
            }
        }

        /// <summary>
        /// Converts display symbols to internal operation strings
        /// Example: "x" to "MULTIPLY", "/" to "DIVIDE"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string ConvertOperationSymbol(string symbol)
        {
            switch (symbol)
            {
                case "+": return "ADD";
                case "-": return "SUBTRACT";
                case "X":
                case "*": return "MULTIPLY";
                case "/": return "DIVIDE";
                default: return symbol.ToUpper();
            }
        }

        /// <summary>
        /// Parses display string to integer
        /// Handles decimal truncation for integer-only calculators
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private long ParseDisplayToLong()
        {
            //Remove any decimal portion for integer calculator
            string intPart = displayString.Split('.')[0];
            // Handle edge case: ".5" becomes ""
            if (string.IsNullOrEmpty(intPart))
                intPart = "0"; // Treat ".5" as "0.5"
            return long.Parse(intPart);
        }


        /// <summary>
        /// Shows error message to user
        /// </summary>
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Calculator Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Resets calculator to initial state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetCalculator()
        {
            displayString = "0";
            currentState = CalculatorState.EnteringFirstNumber;
            UpdateDisplay();
        }

        /// <summary>
        /// Performs calculation with exception handling
        /// SEPARATION OF CONCERNS: Exception handling in UI layer, not calculator class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private long CalculateWithExceptionHandling()
        {
            try
            {
                return activeCalculator.Calculate();
            }
            catch (DivideByZeroException)
            {
                ShowError("Cannot divide by zero");
                ResetCalculator();
                return 0;
            }
            catch (OverflowException)
            {
                ShowError("Result is too large");
                ResetCalculator();
                return 0;
            }
            catch (Exception ex)
            {
                ShowError($"Error: {ex.Message}");
                ResetCalculator();
                return 0;
            }
        }

        /// <summary>
        /// Display base conversion in Progammer mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowBaseConversions()
        {
            long current = ParseDisplayToLong();
            string binary = numberConverter.ToBinary(current);
            string hex = numberConverter.ToHexadecimal(current);
            string octal = numberConverter.ToOctal(current); 

        }

        /// <summary>
        /// Handles equal button click
        /// SPECIAL CASE: if second number missing, repeat first number (5 + = -> 5 + 5)
        /// </summary>
        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (currentState)
                {
                    case CalculatorState.EnteringSecondNumber:
                        // normal case: 5 + 3 =
                        long secondNum = ParseDisplayToLong();
                        activeCalculator.SetSecondNumber(secondNum);
                        break;
                    case CalculatorState.OperationSelected:
                        // SPECIAL CASE: User pressed: 5 + =
                        // Behavior: Treat as "5 + 5 = 10" ( repeat first number)
                        long firstNum = ParseDisplayToLong();
                        activeCalculator.SetSecondNumber(firstNum);
                        break;
                    case CalculatorState.ResultDisplayed:
                        // Already showing result, ignore
                        return;
                    case CalculatorState.EnteringFirstNumber:
                        // No operation selected yet, just show the number
                        return;
                }
                long result = CalculateWithExceptionHandling();

                displayString = result.ToString();
                UpdateDisplay();

                // Show base conversions in Programmer mode
                if (currentMode == CalculatorMode.Programmer)
                {
                    ShowBaseConversions();
                }
                currentState = CalculatorState.ResultDisplayed;
            }
            catch (FormatException)
            {
                ShowError("Invalid number format");
                ResetCalculator();
            }
        }


        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ResetCalculator();
        }

        private void ButtonBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (displayString.Length > 1) {
                displayString = displayString.Substring(0, displayString.Length - 1);
            }
            else {
                displayString = "0";
            }

            UpdateDisplay();
        }

        private void ButtonClearEntry_Click(object sender, RoutedEventArgs e)
        {
            displayString = "0";
            UpdateDisplay();
        }

        /// <summary>
        /// Button 0 - 9 click handlers
        /// PERFORMANCE: Single event handler for all number buttons 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Handles keyboard input for the entire calculator
        /// </summary>  
        private void Window_Keydown(object sender, KeyEventArgs e) {

            //Prevent TextBox from handling keys (It's ReadOnly)
            e.Handled = true;

            switch (e.Key)
            {
                //Number keys (top row)
                case Key.D0:
                case Key.NumPad0:
                    SimulateButtonClick(Button0);
                    break;
                case Key.D1:
                case Key.NumPad1:
                    SimulateButtonClick(Button1);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    SimulateButtonClick(Button2);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    SimulateButtonClick(Button3);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    SimulateButtonClick(Button4);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    SimulateButtonClick(Button5);
                    break;
                case Key.D6:
                case Key.NumPad6:
                    SimulateButtonClick(Button6);
                    break;
                case Key.D7:
                case Key.NumPad7:
                    SimulateButtonClick(Button7);
                    break;
                case Key.D8:
                case Key.NumPad8:
                    SimulateButtonClick(Button8);
                    break;
                case Key.D9:
                case Key.NumPad9:
                    SimulateButtonClick(Button9);
                    break;

                //Decimal point
                case Key.Decimal:
                case Key.OemPeriod:
                    SimulateButtonClick(ButtonDecimal);
                    break;
                case Key.Add:
                case Key.OemPlus:
                    SimulateButtonClick(ButtonAdd);
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    SimulateButtonClick(ButtonSubstract);
                    break;
                case Key.Multiply:
                    SimulateButtonClick(ButtonMultiply);
                    break;
                case Key.Divide:
                case Key.OemQuestion:
                    SimulateButtonClick(ButtonDivide);
                    break;

                // Special keys
                case Key.Enter:
                    SimulateButtonClick(ButtonEqual);
                    break;

                case Key.Back:
                    SimulateButtonClick(ButtonBackspace);
                    break;

                case Key.Escape:
                    SimulateButtonClick(ButtonClear);
                    break;

                case Key.Delete:
                    SimulateButtonClick(ButtonClearEntry);
                    break;
                default:
                    e.Handled = false; // Let unhandled keys propagate
                    break;
            }
        }

        /// <summary>
        /// helper method to simulate button click
        /// this triggers the existing click event handlers
        /// </summary>  
        private void SimulateButtonClick(Button button) {
            // Option 1: Raise Click event (cleaner)
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Optional: Visual feedback (button press animation)
            AnimateButtonPress(button);
        }

        /// <summary>
        /// Optional: Animate button to show it was "pressed"
        /// </summary>
        private async void AnimateButtonPress(Button button) {
            //Store original color
            var originalBackground = button.Background;

            // Flash the button 
            button.Background = new SolidColorBrush(Colors.LightBlue);

            // Wait 100ms
            await Task.Delay(100);

            // Restore original color
            button.Background = originalBackground;
        
        }

    }
}
