# 💗 Wedding Seating Planner

<p align="center">
  <img src="https://img.shields.io/badge/C%23-Windows%20Forms-DB91A5?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-Desktop%20App-C58AA5?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Design%20Patterns-Strategy-BB8297?style=for-the-badge" />
</p>

<p align="center">
  <em>Because every beautiful wedding starts with a little organization. ♡</em>
</p>

---

## 🌷 About the Project

**Wedding Seating Planner** is a Windows Forms desktop application developed in C# to make organizing wedding guests and creating seating arrangements easier and more enjoyable.

Planning a wedding involves many little details, and deciding where everyone should sit can quickly become complicated. This application provides a simple and elegant way to manage families, register guests, create tables and automatically generate seating arrangements.

Designed with a romantic aesthetic and built using object-oriented programming principles and software design patterns, the application combines functionality with a user-friendly experience.

## 🎀 Features

| Feature | Description |
|---|---|
| 💌 Family Management | Create and manage wedding guest families. |
| 👥 Guest Management | Add individual guests to their respective families. |
| 🪑 Table Management | Create and organize wedding reception tables. |
| ✨ Automatic Seating | Automatically assign guests to available tables. |
| 🚫 Seating Restrictions | Define families that should not be seated together. |
| 🌸 User-Friendly Interface | Enjoy a clean, elegant and easy-to-use desktop interface. |

## 🧁 Technologies Used

- **C#** – Main programming language.
- **Windows Forms** – Graphical user interface.
- **.NET** – Application framework.
- **Object-Oriented Programming (OOP)** – Organized and reusable application structure.
- **Design Patterns** – Implementation of the Strategy Pattern.

## 💎 Software Design

One of the main goals of this project is to demonstrate how software design principles and patterns can be applied to a practical application.

### ✨ Strategy Pattern

The project implements the **Strategy Design Pattern** to separate the seating algorithm from the rest of the application.

Instead of directly embedding the seating logic inside the user interface, the application uses an abstraction:

- `ISeatingStrategy` – Defines the seating strategy contract.
- `SequentialSeatingStrategy` – Provides a concrete implementation for sequential guest assignment.

This approach makes the application easier to maintain and allows different seating algorithms to be introduced in the future without significantly changing the existing code.

### 🌷 Object-Oriented Structure

The application uses separate classes to represent its main entities:

- `Family` – Represents a guest family and its members.
- `Guest` – Represents an individual wedding guest.
- `Table` – Represents a wedding reception table.
- `Form1` – Handles the graphical interface and user interactions.

## 🖥️ Getting Started

### Prerequisites

Before running the project, make sure you have:

- Windows operating system.
- Visual Studio.
- .NET SDK compatible with the project configuration.
- Windows Forms development support.

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/desislavaTsocheva/SoftwarePatternsTU.git
   ```

2. Open the repository folder.

3. Locate the `WeddingSeatingApp` project.

4. Open the solution or project file in Visual Studio.

5. Build and run the application.

## 💕 Future Improvements

There is always room for a little more magic!

Some ideas for future development include:

- [ ] Custom table capacities.
- [ ] Drag-and-drop guest assignment.
- [ ] More seating strategies.
- [ ] Guest search and filtering.
- [ ] Editing and removing guests.
- [ ] Saving and loading wedding arrangements.
- [ ] Exporting seating plans.
- [ ] A visual representation of the wedding reception layout.

## 🌸 Project Purpose

This project was created as part of **Software Design Patterns** coursework at the Technical University.

Its purpose is to demonstrate the practical application of object-oriented programming, clean code organization and design patterns through a real-world inspired desktop application.

## 🎀 Author

**Desislava Tsocheva**

<p align="center">
  <a href="https://github.com/desislavaTsocheva">
    <img src="https://img.shields.io/badge/GitHub-Visit%20My%20Profile-C58AA5?style=for-the-badge&logo=github&logoColor=white" />
  </a>
</p>

---

<p align="center">
  Made with ♡, C# and a little bit of pink magic.
  <br/>
  <em>Making wedding planning a little more beautiful, one table at a time. 🌷</em>
</p>
