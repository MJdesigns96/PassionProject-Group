# Musical Card App

Hi there 🖐️ this is the group Passion Project assignment for HTTP 5226 during the Fall 2024 term

This is a project that will work with the ASP.Net 6 framework to build a DB that mixes a card collection and a musci library together.

To initalize the app please run "update-database" to run the migrations then build the app.

In its current form, the app allows users to:

- View the names of the entries in the Card Collection and the Albums in the music library
- Allows the user to Create, Read, Update, and Delete Cards
- Allows the user to Create, Read, Update, and Delete Albums
- Allows the user to Create, Read, Update, and Delete Card Artists
- Allows the user to Create, Read, Update, and Delete Album Artists
- Allows the user to Read the different Card Colors
- Allows the user to Create, Read, Update, and Delete Tracks
- See the relationships between albums and cards
- CRUD functionality for the relationships

## Authorization
This app has admin authentication, server and client side, where if a user is registered and an admin they will have access to CRUD features otherwise they will not be able to.

In the future there will be more updates and features added to this project such as:
- a tally of how many cards are in each color
- a tally of how many cards each artist has drawn
- a section for an album associated with each card
- feature to select random album based on card/color association
- ability to create playlists for cards
- streamable links for album pages
