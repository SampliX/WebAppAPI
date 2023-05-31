using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI1.Models;

namespace WebAppAPI1.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        public static User Admin = new User
        {
            Guid = Guid.NewGuid(),
            Login = "admin",
            Password = "1",
            Name = "Paul",
            Gender = 1,
            Birthday = new DateTime(1996, 09, 27),
            Admin = true,
            CreatedOn = new DateTime(2019, 03, 15),
            CreatedBy = " ",
            ModifiedOn = new DateTime(2019, 03, 15),
            ModifiedBy = " ",
            RevokedOn = new DateTime(),
            RevokedBy = " "
        };

        public static List<User> Users = Enumerable.Range(1, 10).Select(index => new User
        {
            Guid = Guid.NewGuid(),
            Login = $"user{index}",
            Password = $"password{index}",
            Name = $"us{index}",
            Gender = new Random().Next(0,3),
            Birthday = new DateTime(new Random().Next(1979, 2004), new Random().Next(1, 12), new Random().Next(1, 28)),
            Admin = false,
            CreatedOn = DateTime.Now,
            CreatedBy = " ",
            ModifiedOn = DateTime.Now,
            ModifiedBy = " ",
            RevokedOn = new DateTime(),
            RevokedBy = " "
        }).ToList();
        //Create
        //#1
        /// <summary>
        /// Method for creating a user
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">New user's login</param>
        /// <param name="Password">New user's password</param>
        /// <param name="Name">New user's name</param>
        /// <param name="Gender">New user's gender</param>
        /// <param name="Birthday">New user's birthday</param>
        /// <param name="Admin">New user's role</param>
        /// <returns>System response to user input</returns>
        [HttpPost("/Create/User/{Login}")]
        public IActionResult CreateUser(string AuthLogin, string AuthPass, string Login, string Password, string Name, int Gender, DateTime? Birthday, bool Admin)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            Users.Add(new User {
                Guid = Guid.NewGuid(),
                Login = Login,
                Password = Password,
                Name = Name,
                Gender = Gender,
                Birthday = Birthday,
                Admin = Admin,
                CreatedOn = DateTime.Now,
                CreatedBy = AuthLogin,
                ModifiedOn = DateTime.Now,
                ModifiedBy = " ",
                RevokedOn = new DateTime(),
                RevokedBy = " "
            });
            return Ok("Created successfully");
        }
        //Update-1
        //#2
        /// <summary>
        /// Method for changing the user name
        /// </summary>
        /// <param name="AuthLogin">User login</param>
        /// <param name="AuthPass">User password</param>
        /// <param name="NewName">New user name</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/User/Name")]
        public IActionResult UserUpdateName(string AuthLogin, string AuthPass, string NewName)
        {
            var user = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Name = NewName;
            return Ok("Name changed successfully");
        }
        /// <summary>
        /// Method for changing the user gender
        /// </summary>
        /// <param name="AuthLogin">User login</param>
        /// <param name="AuthPass">User password</param>
        /// <param name="NewGender">New user gender</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/User/Gender")]
        public IActionResult UserUpdateGender(string AuthLogin, string AuthPass, int NewGender)
        {
            var user = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Gender = NewGender;
            return Ok("Gender changed successfully");
        }
        /// <summary>
        /// Method for changing the user birthday
        /// </summary>
        /// <param name="AuthLogin">User login</param>
        /// <param name="AuthPass">User password</param>
        /// <param name="NewBirthday">New user birthday</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/User/Birthday")]
        public IActionResult UserUpdateBirthday(string AuthLogin, string AuthPass, DateTime NewBirthday)
        {
            var user = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Birthday = NewBirthday;
            return Ok("Birthday changed successfully");
        }
        /// <summary>
        /// Method for changing the user name
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <param name="NewName">New user name</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/Admin/Name")]
        public IActionResult AdminUpdateName(string AuthLogin, string AuthPass, string Login, string NewName)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Name = NewName;
            return Ok("Name changed successfully");
        }
        /// <summary>
        /// Method for changing the user gender
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <param name="NewGender">New user gender</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/Admin/Gender")]
        public IActionResult AdminUpdateGender(string AuthLogin, string AuthPass, string Login, int NewGender)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Gender = NewGender;
            return Ok("Gender changed successfully");
        }
        /// <summary>
        /// Method for changing the user birthday
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <param name="NewBirthday">New user birthday</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/Admin/Birthday")]
        public IActionResult AdminUpdateBirthday(string AuthLogin, string AuthPass, string Login, DateTime NewBirthday)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Birthday = NewBirthday;
            return Ok("Birthday changed successfully");
        }
        //#3
        /// <summary>
        /// Method for changing the user password
        /// </summary>
        /// <param name="AuthLogin">User login</param>
        /// <param name="AuthPass">User password</param>
        /// <param name="NewPassword">New user password</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/User/Password/{AuthLogin}")]
        public IActionResult UserUpdatePassword(string AuthLogin, string AuthPass, string NewPassword)
        {
            var user = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Password = NewPassword;
            return Ok("Password changed successfully");
        }
        /// <summary>
        /// Method for changing the user password
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <param name="NewPassword">New user password</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/Admin/Password/{Login}")]
        public IActionResult AdminUpdatePassword(string AuthLogin, string AuthPass, string Login, string NewPassword)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Password = NewPassword;
            return Ok("Password changed successfully");
        }
        //#4
        /// <summary>
        /// Method for changing the user login
        /// </summary>
        /// <param name="AuthLogin">User login</param>
        /// <param name="AuthPass">User password</param>
        /// <param name="NewLogin">New user login</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/User/Login/{Login}")]
        public IActionResult UserUpdateLogin(string AuthLogin, string AuthPass, string NewLogin)
        {
            var user = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (Users.Where(u => u.Login == NewLogin).Count() != 0)
            {
                return UnprocessableEntity("The login is already occupied");
            }
            user.Login = NewLogin;
            return Ok("Name changed successfully");
        }
        /// <summary>
        /// Method for changing the user login
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">Old user login</param>
        /// <param name="NewLogin">New user login</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update/Admin/{Login}")]
        public IActionResult AdminUpdateLogin(string AuthLogin, string AuthPass, string Login, string NewLogin)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (Users.Where(u => u.Login == NewLogin).Count() != 0)
            {
                return UnprocessableEntity("The login is already occupied");
            }
            user.Login = NewLogin;
            return Ok("Name changed successfully");
        }
        //Read
        //#5
        /// <summary>
        /// Method for viewing all active users
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <returns>System response to user input</returns>
        [HttpGet("/Read/AllActive")]
        public IActionResult GetAllActive(string AuthLogin, string AuthPass)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            return Ok(Users.FindAll(u => u.RevokedOn == new DateTime()).OrderBy(u => u.CreatedOn));
        }
        //#6
        /// <summary>
        /// Method for viewing users by login
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <returns>System response to user input</returns>
        [HttpGet("/Read/ByLogin")]
        public IActionResult GetByLogin(string AuthLogin, string AuthPass, string Login)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok($"{user.Name}, {user.Gender}, {user.Birthday}, {user.RevokedOn}");
        }
        //#7
        /// <summary>
        /// Method for viewing user data
        /// </summary>
        /// <param name="AuthLogin">User login</param>
        /// <param name="AuthPass">User password</param>
        /// <returns>System response to user input</returns>
        [HttpGet("/Read/User")]
        public IActionResult GetUser(string AuthLogin, string AuthPass)
        {
            var user = Users.SingleOrDefault(u => u.RevokedOn == new DateTime() && u.Login == AuthLogin && u.Password == AuthPass);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
        //#8
        /// <summary>
        /// A method for viewing age-appropriate user data
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Age">User age</param>
        /// <returns>System response to user input</returns>
        [HttpGet("/Read/AllUsersByAge")]
        public IActionResult GetAllUsersByAge(string AuthLogin, string AuthPass, int Age)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            return Ok(Users.FindAll(u => u.Birthday < DateTime.Now.AddYears(-Age)));
        }
        //Delete
        //#9
        /// <summary>
        /// Method for soft removal of the user
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <returns>System response to user input</returns>
        [HttpDelete("/Delete/Light/{Login}")]
        public IActionResult DeleteLight(string AuthLogin, string AuthPass, string Login)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("The user being deleted was not found");
            }
            user.RevokedOn = DateTime.Now;
            user.RevokedBy = AuthLogin;
            return Ok(new { Message = "Deactivated successfully" });
        }
        /// <summary>
        /// Method for completely deleting a user
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <returns>System response to user input</returns>
        [HttpDelete("/Delete/Heavy/{Login}")]
        public IActionResult DeleteHeavy(string AuthLogin, string AuthPass, string Login)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("The user being deleted was not found");
            }
            Users.Remove(user);
            return Ok(new { Message = "Deleted successfully" });
        }
        //Update-2
        //#10
        /// <summary>
        /// User recovery method after soft deletion
        /// </summary>
        /// <param name="AuthLogin">Administrator login</param>
        /// <param name="AuthPass">Administrator password</param>
        /// <param name="Login">User login</param>
        /// <returns>System response to user input</returns>
        [HttpPut("/Update2/UserRecovery/{Login}")]
        public IActionResult UserRecovery(string AuthLogin, string AuthPass, string Login)
        {
            var admin = Users.SingleOrDefault(u => u.Login == AuthLogin && u.Password == AuthPass);
            if (admin == null)
                return NotFound("Incorrect user data");
            else if (!admin.Admin)
                return Forbid("You don't have permission to do this");

            var user = Users.SingleOrDefault(u => u.Login == Login);
            if (user == null)
            {
                return NotFound("The selected user was not found");
            }
            user.RevokedOn = new DateTime();
            user.RevokedBy = string.Empty;
            return Ok("Restored successfully");
        }
    }
}
