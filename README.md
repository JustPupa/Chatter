Cozy Chatter
=====================
***Chatter*** is an application that combines the functions of a social network and a messenger
Chats support the transfer of various types of `media`, and the `author's toolset` allows you to analyze the popularity of posts in the feed
***
To run the application you need to start both Kestrel and Vite servers:
1) Start the Kestrel server by running it in the code editor or calling the corresponding `Cozy_Chatter.exe` file
2) Run Vite server by calling the ```npm run dev``` command in the terminal
3) Navigate to the web address specified when starting the Vite server
***
Main files description
=====================
File/Directory Name            | Content
-------------------------------|----------------------
Server/Controllers             | A controller for `processing incoming requests` and sending responses to the Vite server
Server/Models                  | Model classes representing `database entities`
Server/Repositories            | Intermediate classes dealing with the `business logic` of the transmitted data
Client/src/components          | React application `components`
Client/src/services            | The part of the React application that contains `requests` to the Kestrel server
