# QuizLash
An ASP.Net Core 2.2 Web Application, which utilizes various Azure services to create an interactive speech based approach to studying

Live production version of this application can be found <a href="https://quizlash.azurewebsites.net">here</a>.

The intended purpose of this application is to implement and familiarize myself with as many Azure services as possible without over engineering the application or bogging users down with unnecessary features.

******************************************************************************************************************************************
<h2>REQUIREMENTS</h2>
<h5>1. Manage user accounts</h5>
<ol>
  <li>Secure user passwords</li>
  <li>Allow multi-factor authentication</li>
  <li>Confirm accounts via eMail</li>
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
  <li>Utterance Understanding</li>
      
  <li>Regional Language Support</li>
      
  <li>Intent Understanding</li>
      
  <li>Map all required intents</li>
      
  <li>Intent Execution</li>
</ol>

<h3>CURRENTLY IMPLEMENTED TECHNOLOGIES</h3>

<h5>Azure SQL Server with 2 databases</h5>
  <h6>1.Application Database</h6>
  <p>Used to store user created data such as questions and quizzes. Contains all data and tables required for business logic.</p>
  <i>Supports requirement : 2-2 | 2-3 | 5-1 | 5-2 </i>
  <h6>2.User Authentication Database</h6>
  <p>Contains all the ASP.NET Identity tables and user information, maintained separately from the application database to reduce how often user data is in transit.</p>
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
  <p>Used to provide hyperlinks to the documentation PDFs of some technologies used in this application, will also be used for image storage.</p>
  

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
 
<h1>Test Plan</h1>

<table>
  <caption>Testing Report</caption>
  <tr>
    <th>Requirement ID</th>
    <th>Requirement Description</th>
    <th>Test Method</th>
    <th>Test Procedure</th>
    <th>Current Status</th>
    <th>Timestamp</th>
  </tr>
  <tr>
    <td>1.1</td>
    <td>Secure user passwords</td>
    <td>Inspection</td>
    <td>Click "Login", enter email and password, query identityuser table in SQL DB, verify value is hashed</td>
    <td>Passing</td>
    <td>03/13/2019</td>
  </tr>
  <tr>
    <td>1.2</td>
    <td>Allow multi-factor authentication</td>
    <td>Demonstration</td>
    <td>Click "Login", enter email and password, click "Hello [yourUsername]!", click "Two Factor Authentication", click "Setup authenticator app", scan QR code, enter numerical code, click "verify"</td>
    <td>Passing</td>
    <td>03/14/2019</td>
  </tr>
  <tr>
    <td>1.3</td>
    <td>Confirm accounts via email</td>
    <td>Demonstration</td>
    <td>Click "Register", enter email and password, check email, click the return URL, brought back to your profile page</td>
    <td>Passing</td>
    <td>03/14/2019</td>
  </tr>
  <tr>
    <td>1.4</td>
    <td>Encrypt user data at rest</td>
    <td>Inspection</td>
    <td>Login to host SQL server on azure, verify encryption is selected</td>
    <td>Passing</td>
    <td>03/10/2019</td>
  </tr><tr>
    <td>2.1</td>
    <td>Create view forms for user specific questions</td>
    <td>Integration</td>
    <td>Test Filter from Domain interfaces</td>
    <td>Not Implemented</td>
    <td>04/29/2019</td>
  </tr>
  <tr>
    <td>2.2</td>
    <td>Have access to CRUD operations for their created content</td>
    <td>Demonstration</td>
    <td>Login, click "Material", click "Create a new question", Complete form, click "Create", Verify it exists on the material page, click "Edit", verify view exists</td>
    <td>Passing</td>
    <td>04/01/2019</td>
  </tr>
  <tr>
    <td>2.3</td>
    <td>Allow them to choose between studying their own material or all material</td>
    <td>Demonstration</td>
    <td>Verify that the take from only my questions switch displays different data then when it is not selected</td>
    <td>Passing</td>
    <td>04/13/2019</td>
  </tr>
  <tr>
    <td>3.1</td>
    <td>Create view for flashcards</td>
    <td>Inspection</td>
    <td>Login, Click "Material", Click "Start with Flashcards", complete setup form, verify flashcards are populating</td>
    <td>Passing</td>
    <td>04/10/2019</td>
  </tr>
  <tr>
    <td>3.2</td>
    <td>Create view for short answer quizzes</td>
    <td>Inspection</td>
    <td>Login, Click "Material", Click "Start a new quiz", complete setup form and select "Short Answer" under the "Type" dropdown, verify view format matches the type</td>
    <td>Passing</td>
    <td>04/15/2019</td>
  </tr>
  <tr>
    <td>3.3</td>
    <td>Create view for multiple choice quizzes</td>
    <td>Inspection</td>
    <td>Login, Click "Material", Click "Start a new quiz", complete setup form and select "Multiple Choice" under the "Type" dropdown, verify view format matches the type</td>
    <td>Not Implemented</td>
    <td>04/29/2019</td>
  </tr>
  <tr>
    <td>3.4</td>
    <td>Create view for true or false quizzes</td>
    <td>Inspection</td>
    <td>Login, Click "Material", Click "Start a new quiz", complete setup form and select "True or False" under the "Type" dropdown, verify view format matches the type</td>
    <td>Not Implemented</td>
    <td>04/29/2019</td>
  </tr>
  <tr>
    <td>4.1</td>
    <td>Grading method for short answer quizzes</td>
    <td>Unit Test</td>
    <td>Mock data for input dictionary and questions repository</td>
    <td>Not Implemented</td>
    <td>04/29/2019</td>
  </tr>
  <tr>
    <td>4.2</td>
    <td>Grading method for multiple choice quizzes</td>
    <td>Unit Test</td>
    <td>Mock data for selected input and questions repository</td>
    <td>Not Implemented</td>
    <td>04/29/2019</td>
  </tr>
  <tr>
    <td>4.3</td>
    <td>Grading method for true or false quizzes</td>
    <td>Unit Test</td>
    <td>Mock data for selected input and questions repository</td>
    <td>Not Implemented</td>
    <td>04/29/2019</td>
  </tr>
  <tr>
    <td>5.1</td>
    <td>Query database to average previous quiz scores for a given topic</td>
    <td>Inspection</td>
    <td>Current aggregate method returns correct decimal value</td>
    <td>Passing</td>
    <td>04/19/2019</td>
  </tr>
  <tr>
    <td>5.2</td>
    <td>Display the averages for all topics for quizzes they have taken in the user content index</td>
    <td>Inspection</td>
    <td>Displays the correct averages for all topics for quizzes they have taken in the quizs table for the topic of that quiz</td>
    <td>Passing</td>
    <td>04/19/2019</td>
  </tr>
  <tr>
    <td>6.1</td>
    <td>Recognive Utterances</td>
    <td>Unit Tests</td>
    <td>Ensure text to speech recognizes the given command</td>
    <td>Passing</td>
    <td>05/10/2019</td>
  </tr>
  <tr>
    <td>6.1.1</td>
    <td>Regional Language Support</td>
    <td>Unit Tests</td>
    <td>Ensure text to speech recognizes multiple languages</td>
    <td>Passing</td>
    <td>05/10/2019</td>
  </tr>
  <tr>
    <td>6.2</td>
    <td>Map Intent</td>
    <td>Unit Tests</td>
    <td>Ensure Speech to text understands the intent of the utterance</td>
    <td>Passing</td>
    <td>05/10/2019</td>
  </tr>
  <tr>
    <td>6.2.1</td>
    <td>Map all required intents</td>
    <td>Unit Tests</td>
    <td>Ensure Speech to text understands the intent of the custom utterances</td>
    <td>Not Implemented</td>
    <td>05/10/2019</td>
  </tr>
  <tr>
    <td>6.3</td>
    <td>Carry out the intent</td>
    <td>Unit Testing</td>
    <td>Ensure complete integration from voice input from user and data read back by the Assistant api</td>
    <td>Passing</td>
    <td>05/10/2019</td>
  </tr>
  <tr>
    <td>6.3.1</td>
    <td>Mapp Custom Intents</td>
    <td>Unit Testing</td>
    <td>Ensure complete integration and UI support</td>
    <td>Not Implemented</td>
    <td>05/10/2019</td>
  </tr>
</table>
