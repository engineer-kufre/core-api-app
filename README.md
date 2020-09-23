# core-api-app
An ASP.NET Core API application with end-points to:
* Register a user https://localhost:44368/user/register
* Login a user https://localhost:44368/user/login/login
* Fetch logged-in user details https://localhost:44368/user/getloggedinuserdetails
* Fetch all registered users details paginated into 5 records per response https://localhost:44368/user/getallregisteredusers?page=1 https://localhost:44368/user/getallregisteredusers?page=2 etc.

All End-points except the Register and Login end points were protected JSON Web Token(JWT) authentication system.
