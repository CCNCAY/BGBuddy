# BGBbuddy - A personal board game database.

About.

I am a board game enthusiast with a collection of 100+ board games. I use boardgamesgeek.com's (BGG) collection manager to showcase my collection so my players can pick from there. 
However I needed a personal database of my games, that have some of the same stats as BGG (such as minimum and maximum player number), but I also wanted to add some remaraks I do not want to publish.

Additionally, I wanted to have a database of my players and their preferences because personal preferences can be very different from the average game rating. This cannot be done using a public database.

On top of that, I decided to save the game sessions in a database along with the players played (without results), so I can organize my group a bit easier. 

BGBuddy is Windows WPF application with features as:
- A mimimalistic SQLite database of games. The tables and queries etc. are fixed and hardcoded, and done via button clicks. Under the hood, SQLite commands run.
- Imports games from BGG database via API and adds them to the database.
