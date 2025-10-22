# 🧮 WPF Calculator 
A modern calculator built with WPF and C#, featuring state machine architecture and full keyboard support.

✨ Features

✅ Basic Operations - Add, Subtract, Multiply, Divide\
✅ Operation Chaining - 5 + 3 + 2 = evaluates left-to-right\
✅ Large Numbers - Supports up to 9.2 quintillion (64-bit long)\
🎹 Keyboard Support - Full keyboard + mouse input\
🔄 State Machine - Prevents input bugs, clean UX flow\
🛡️ Error Handling - Division by zero protection with MessageBox alerts\
📦 Modular Design - Separate UI and logic layers

#### 🚀 Quick Start
Prerequisites
Visual Studio 2019+,
 .NET Framework 4.7.2+

## Installation
```
git clone https://github.com/fahmimee03/wpf-calculator.git 
cd wpf-calculator
#Open Learning.sln in Visual Studio
#Press F5 to run
```
## **Example - Your Project Structure Section:**

### Project Structure

Learning/\
├── Calculator/              # WPF UI Project \
│   ├── MainWindow.xaml      # UI Layout \
│   └── MainWindow.xaml.cs   # UI Logic + State Machine \
│ \
└── Learning/                # Class Library \
----├── BasicCalculator.cs   # Core logic \
----├── ScientificCalculator.cs \
----├── ProgrammerCalculator.cs \
----└── NumberConverter.cs   # Binary/Hex/Octal 

## State Machine 
EnteringFirstNumber → OperationSelected → EnteringSecondNumber → ResultDisplayed \
         ↑----------------------------------------------------------------------------------------↓ \
         └────────────────── (New Calculation) ───────────────────┘ \
Why?
Prevents bugs like displaying "53" when user types 5 + 3 (should show "3"). 
#### Key Design Patterns

- State Pattern - Manages calculator input flow 
- Inheritance - Scientific/Programmer extend BasicCalculator 
- Separation of Concerns - UI (WPF) ↔ Logic (DLL) 
- Event-Driven - Single event handler for multiple buttons

## 🤝 Contributing
Contributions welcome! Areas for improvement:

🔬 Scientific mode UI (logic exists) \
💻 Programmer mode UI (Binary/Hex/Octal display) \
📜 Calculation history panel \
💾 Memory functions (M+, M-, MR, MC) \
🎨 Themes/skins support 

Please:

Fork the repository \
Create feature branch (git checkout -b feature/amazing) \
Commit changes (git commit -m 'Add amazing feature') \
Push to branch (git push origin feature/amazing) \
Open Pull Request


## 🙏 Acknowledgments

Design inspired by Windows Calculator\
Built as part of C# WPF learning journey\
State machine pattern from software engineering best practices

<div align="center">
⭐ Star this repo if you found it helpful!
Made with ❤️ using C# and WPF
</div>
