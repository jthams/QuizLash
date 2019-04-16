# StudyBuddy
An ASP.Net Core 2.2 Web Application, which utilizes various Azure services to create an interactive speech based approach to studying

Live production version of this application can be found on my custom domain <a href="http://studybuddy.hamelot.net/">here</a>.

The intended purpose of this application is to implement and familiarize myself with as many Azure services as possible without over engineering the application or bogging users down with unnecessary features.

******************************************************************************************************************************************
<h2>REQUIREMENTS</h2>
<h5>1. Manage user accounts</h5>
<ol>
  <li>Secure user passwords</li>
  <li>Allow multi-factor authentication</li>
  <li>Confirm accounts via e-Mail</li>
  <li>Encrypt user data at rest</li>
</ol>
<h5>2. Allow users to create content</h5>
<ol>
  <li>Create view forms for user specific questions</li>
  <li>Have access to CRUD operations for their created content</li>
  <li>Allow them to choose between studying their own material or all material</li>
</ol>
<h5>3. Establish study material format</h5>
<ol>
  <li>Create view for flashcards</li>
  <li>Create view for short answer quizzes</li>
  <li>Create view for multiple choice quizzes</li>
  <li>Create view for true or false quizzes</li>
</ol>
<h5>4. Implement grading on server-side</h5>
<ol>
  <li>Grading method for short answer quizzes</li>
  <li>Grading method for multiple choice quizzes</li>
  <li>Grading method for true or false quizzes</li>
</ol>
<h5>5. Display topic based performance history</h5>
<ol>
  <li>Query database to average previous quiz scores for a given topic</li>
  <li>Display the averages for all topics for quizzes they have taken in the user content index</li>
</ol>
<h5>6. Implement language understanding</h5>
<ol>
  <li>Text to speech API</li>
  <li>Speech to text API</li>
  <li>Integrate above services with Microsoft LUIS</li>
</ol>

<h3>CURRENTLY IMPLEMENTED TECHNOLOGIES</h3>

<h5>Azure SQL Server with 2 databases</h5>
  <h6>1.Application Database</h6>
  <p>Used to store user created data such as questions and quizzes. Contains all data and tables required for buisness logic.</p>
  <i>Supports requirement : 2-2 | 2-3 | 5-1 | 5-2 </i>
  <h6>2.User Authentication Database</h6>
  <p>Contains all the ASP.NET Identity tables and user information, maintained seperately from the application database to reduce how often user data is in transit.</p>
  <i>Supports requirement : 1-1 | 1-4 | 4-1 | 4-2 | 4-3 | 5-1 </i>
  
<h5>Entity Framework Core</h5>
  <p>Code first model scaffolding to keep precise entity relationships.
  Grants users permission to utilize CRUD operations in order to store their customized questions and quizzes through the use of forms.</p>
  <i>Supports requirement : 2-2 </i>
  

<h5>User-Secrets Manager</h5>
 <p>Protect application secrets in the development environment.</p>
  

<h5>Azure Key Vault</h5> 
  <p>Protect application secrets in the production environment.</p>
  
  
<h5>ASP.NET IDENTITY (full UI scaffolded to add further constraints to account behavior)</h5>
  <p>Safe user account management as to avoid writing my own hash algorithms and improperly storing user passwords.
  The full suite of functions is not currently being utilized, future implementations may occur as needed.</p>
  <i>Supports requirement : 1-1 | 1-2 | 2-2 | 2-3 </i>
 
<h5>SendGrid API</h5>
  <p>Allows accounts to be confirmed through email confirmation.</p>
  <i>Supports requirement : 1-3</i>
  
<h5>Two Factor Authentication</h5> 
  <p>If a user desires they may attach an authenticator to their account through a JavaScript generated QR code.</p>
  <i>Supports requirement : 1-2 </i>

<h5>Azure BLOB Storage</h5>
  <p>Used to provide hyper links to the documentation PDFs of some technologies used in this application, will also be used for image storage.</p>
  

<h5>BootStrap 4</h5>
  <p>Utilized for the basic stylizing and animations of the application.</p>
  <i>Supports requirement : 2-1 | 3-1 | 3-2 | 3-3 | 3-4 | 5-2</i>
  
<h3>DATABASE DESIGN</h3>
 <h5>Application Database Diagram</h5>
 <i><b>Classes for this database can be found in the "DomainModels" folder</b></i>
 <img src="https://theblobthestorageaccount.blob.core.windows.net/myblob/ApplicationDatabaseDiagram.PNG" width="75%" height="75%" title="Application Data">
 
 <h5>Authentication Database Diagram</h5>
 <i><b>Classes for this database can be found in the Areas/Identity folder</b></i>
 <img src="https://theblobthestorageaccount.blob.core.windows.net/myblob/IdentityDatabaseDiagram.PNG" width="75%" height="75%" title="Authentication Data">
 
 
