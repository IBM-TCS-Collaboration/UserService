using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Database.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DatabaseContext db;
        // GET: api/<UserController>


        public UserController()
        {
            db = new DatabaseContext();

        }
        
        [ActionName("ResetPassword")]
        [HttpPut("{UserId}")]
        public IActionResult ResetPassword([FromBody] User n)
        {
            var existingUser = db.Users.Where(s => s.UserId == n.UserId).FirstOrDefault<User>();


            if (existingUser != null)
            {
                existingUser.Password = n.Password;
                existingUser.ConfirmPassword = n.ConfirmPassword;
                if(n.Password==n.ConfirmPassword)
                { 
                db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [ActionName("Login")]
        [HttpGet("{id}/{pwd}")]
        public HttpResponseMessage GetLoginDetails(string id, string pwd)
        {
            // var returnedResult = (from Q in db.Users select Q.UserId).ToList();

            var returnedResult = (from Q in db.Users where Q.Username == id && Q.Password == pwd select Q.UserId);


            if (returnedResult.Count() > 0)
            {
                //return Ok("Login Successful");
                // return StatusCode(StatusCodes.Status200OK);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            else
            {
                //return 
                //return StatusCode(StatusCodes.Status500InternalServerError);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

 }

        [ActionName("GetUserRoles")]
        [HttpGet]
        public IActionResult GetUserRoles()
        {
            var UserRole = db.Users
                .Join(
                db.Roles,
                User => User.RoleID,
                Role => Role.RoleId,
                (User, Role) => new
                {
                    UserID = User.UserId,
                    AssignToAdmin = Role.RoleName,
                    Username = User.Username
                }
                ).ToList();

            return Ok(UserRole);
        }

        [ActionName("UserCount")]
        [HttpGet]
        public IActionResult GetUserCount()
        {
            var query = from User in db.Users
                        group User by 1 into a

                        select new {

                            UserCount = a.Count()

                        };
                
            
            return Ok(query);
        }

        [ActionName("AdminCount")]
        [HttpGet]
        public IActionResult GetAdminCount()
        {
            var count = from User in db.Users
                        where User.RoleID == 1
                        group User by 1 into g
                        select new
                        {
                            AdminCount = g.Count()
                        };

            return Ok(count);
        }

        


        [ActionName("UserDetails")]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return db.Users.ToList();
        }

        //[ActionName("GetUserRoles")]
        //[HttpGet("{UserID}")]
        //public UserRole GetUserRoles(int UserID)
        //{

        //    return UserRoles;
        //}

        [ActionName("GetAllRoles")]
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var allRoles = db.Roles.FromSql("SELECT RoleID,RoleName FROM userdb.Roles;");
            return Ok(allRoles);
        }



        [ActionName("GetAllAdmins")]
        [HttpGet]
        public IEnumerable<User> GetAllAdmins()
        {
            var allAdmins = db.Users.FromSql("SELECT * FROM userdb.Users where RoleID=1;");
            return allAdmins;
        }

        // GET api/<UserController>/5
        [ActionName("UserDetails")]
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        [ActionName("GetUserRole")]
        [HttpGet("{UserId}")]
        public IActionResult GetUserRole(int UserId)
        {
            var UserRole = db.Users
                .Join(
                db.Roles,
                User => User.RoleID,
                Role => Role.RoleId,
                (User, Role) => new
                {
                    UserID = User.UserId,
                    AssignToAdmin = Role.RoleName,
                    Username = User.Username
                }
                ).Where(s => s.UserID == UserId).ToList();

           // var UserRole = UserRol.Where(s => s.UserID == UserId);

            return Ok(UserRole);
        }



        // POST api/<UserController>
        [ActionName("CreateUser")]
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            try
            {
                db.Users.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<UserController>
        [ActionName("CreateRole")]
        [HttpPost]
        public IActionResult CreateRole([FromBody] Roles r)
        {
            try
            {
                db.Roles.Add(r);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [ActionName("UpdateUserRole")]
        [HttpPut("{UserId}")]
        public IActionResult Put([FromBody] User n)
        {
            var existingUser = db.Users.Where(s => s.UserId == n.UserId).FirstOrDefault<User>();


            if (existingUser != null)
            {
                existingUser.RoleID = n.RoleID;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        

        [ActionName("AssignUserRole")]
        [HttpPost]
        public IActionResult AssignUserAdmin([FromBody] AssignedRole r)
        {
            var AssignRole = db.AssignedRoles.Where(s => s.UserID == r.UserID).FirstOrDefault<AssignedRole>();
            try
            {

                db.AssignedRoles.Add(r);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }
    }
}


    
