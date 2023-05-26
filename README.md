# codechallenge

Readiness:
- For the Fake FX API Server, I reused the backend code and implemented a hard-coded FX Rate response.
- The code requires line-by-line code reviews, which I haven't completed yet.
- I'm currently facing difficulties in setting up the Unit Test flow for continuous integration (CI). 
- I configured Jenkins to implement CI/CD by fetching code from SVN, executing testing scripts, and deploying the application.
- My technology stack includes React for the front end, DotNet for the back end, and SQL Server for the database.
- I primarily use VS Code as my development tool, so the project structure and appearance might differ when opened in other IDEs.
- I generally follow standard exception handling practices, but for this time, I focusing on throwing and handling exceptions up to the controller layer. I ensure that user-friendly exception messages are displayed.
- When it comes to web development, my focus is not primarily on creating visually stunning websites. There are various external templates available for creating visually appealing designs.
- Sometimes, I create a single class to include multiple functionalities, but I understand the importance of adhering to the Single Responsibility Principle. The approach may vary depending on the team's practices.
- Regarding password policies, I prefer to enforce them on the server side as users have the ability to change their passwords.
- The backend is capable of sending emails without requiring an API key.
- In the front end, I currently perform basic password matching validation and have not implemented features such as ReCAPTCHA or obtained any licenses.
- I used a global search-and-replace approach to remove sensitive information such as tokens and password details from the codebase.		



#########################################



**Backend:**
Prerequisites: 
  install dotnet-sdk-6.0.408-win-x64
  run db.scipt in SMSS

Project folder (api):
  Configurations - appsetting.json
  Cotrollers - API Entry point and Services Class
  Helper - Constant.class
  Middleware - JWT , Error Handling
  Model - Normal DTO class
   
Run Operation: dotnet run

api project
project structure 

api swagger: https://localhost:7135/swagger/index.html

**Frontend:**
Prerequisites: 
install node.exe

Project folder (userapp):
src 
  types - 1 files contains all types 
  action - contain user action which call APU
  component - contain page level
  reducers - support central management
  service - support JWT
  store - support middleware   
cert - contain certs

Installation: 
npm install
Start Operation: 
npm start

Web URL (User):
https://localhost:3000/

