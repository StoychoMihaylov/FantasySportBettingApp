# Fantasy Sport Betting App

## Overview

The Fantasy Sport Betting App is a platform where users can place bets on their favorite teams. If the team wins, the user wins money based on the betting odds.

## Technologies Used

- **Backend:**
  - F# for business logic and application code
  - ASP.NET Core for web API
  - MongoDB for data storage

- **Frontend:**
  - Angular for the user interface

- **Containerization:**
  - Docker Compose for orchestration

## Prerequisites

Before you begin, ensure you have the following installed:

- [Docker](https://www.docker.com/get-started) and Docker Compose

## Running the Project

To run the project, follow these steps:

1. Clone the repository:
    ```sh
    git clone https://github.com/StoychoMihaylov/FantasySportBettingApp.git
    cd FantasySportBettingApp
    ```

2. Build and start the containers using Docker Compose:
    ```sh
    docker-compose build
    docker-compose up
    ```
3. Open your browser and navigate to http://localhost:4200 to access the frontend, and http://localhost:8000 or https://localhost:8001 for the backend API.

## Features
- View Matches: Users can view a list of matches and their details.
- Place Bets: Users can place bets on upcoming matches.
- Match Results: The system updates match results.
- Bets Results: The system updates bets and manages user winnings.

