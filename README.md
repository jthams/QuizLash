# StudyBuddy
An ASP.Net Core 2.2 Web Application, which utilizes various Azure services to create an interactive speech based approach to studying

The live production version of this application can be found as a sub domain to my custom domain at studybuddy.hamelot.net

The intended purpose of this application is for me to implement and familiarize myself with as many Azure services as possible without over engineering the application or bogging users down with unnecessary features.

******************************************************************************************************************************************
CURRENTLY IMPLEMENTED TECHNOLOGIES

**Azure SQL Database - 
  Encrypted data at rest to safeguard application data.
  
**Entity Framework Core - 
  Database first model scaffolding to keep precise entity relationships.
  Grants users permission to utilize CRUD operations in order to store their customized questions and quizzes through the use of forms.

**User-Secrets Manager - 
  Protect application secrets in the development environment.

**Azure Key Vault - 
  Protect application secrets in the production environment. 
  
**ASP.NET IDENTITY (full UI scaffolded to add further constraints to account behavior) - 
  Safe user account management as to avoid writing my own hash algorithms and improperly storing user passwords.
  The full suite of functions is not currently being utilized, future implementations may occur as needed.
 
**SendGrid API - 
  Allows accounts to be confirmed through email confirmation.
  
**Two Factor Authentication - 
  If a user desires they may attach an authenticator to their account through a JavaScript generated QR code.

**Azure BLOB Storage - 
  Used to provide hyper links to the documentation PDFs of some technologies used in this application, will also be used for image storage.
