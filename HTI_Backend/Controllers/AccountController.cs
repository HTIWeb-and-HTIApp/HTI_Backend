using HTI.Core;
using HTI.Repository.Data;
using HTI.Service;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace HTI_Backend.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        #region Old_Login
        //private readonly StoreContext _context;

        //public AccountController(StoreContext context)
        //{
        //    _context = context;
        //}

        //[HttpPost]
        //public IActionResult Login([FromBody] LoginDto model)
        //{
        //    if (model.Email == "Admin" && model.Password == "hti_cs")
        //    {
        //        // Department management
        //        return Ok("This is the Admin user");
        //    }

        //    var student = _context.Students.FirstOrDefault(s => s.Email == model.Email);
        //    if (student != null)
        //    {
        //        // Student
        //        var studentId = student.Email.Split('@')[0];
        //        return Ok($"This is the student user {studentId}");
        //    }


        //    var doctor = _context.Doctors.FirstOrDefault(d => d.Email == model.Email);
        //    if (doctor != null)
        //    {
        //        // Doctor
        //        var doctorName = doctor.Email.Split('@')[0];
        //        return Ok($"This is the Doctor user {doctorName}");
        //    }

        //    var teacherAssistant = _context.TeachingAssistants.FirstOrDefault(ta => ta.Email == model.Email);
        //    if (teacherAssistant != null)
        //    {
        //        // Teacher assistant
        //        var teacherAssistantName = teacherAssistant.Email.Split('@')[0];
        //        return Ok($"This is the Teacher Assistant user {teacherAssistantName}");
        //    }

        //    return Unauthorized();
        //} 
        #endregion

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
        }


        
        [HttpPost("Login")] 

        // email >>> pasword X

        public async Task<ActionResult<UserToReturnDto>> Login(LoginModel model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);

            if (User is null) return Unauthorized(new ApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));

            var role = await _userManager.GetRolesAsync(User);
            return Ok(new UserToReturnDto()
            {
                Id = User.Email.Split("@")[0],

                Role = role,

                Token = await _authService.CreateTokenAsync(User, _userManager)

            }) ;
        }

        [HttpPost("CreateUser")]

        public async Task<ActionResult> CreateUser(RegisterModel model)
        {
            if (CheckEmail(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "The Email Is Already Exist"));
            }
            
            var checkRole = await _roleManager.RoleExistsAsync(model.Role);

            if (!checkRole)    
                return NotFound(new ApiResponse(404,"Role doesn't Exist"));

            var user = new IdentityUser()
            {
                UserName = model.Email.Split("@")[0],
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);  

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok(new
            {
                UserName = model.Email.Split("@")[0],
                Email = model.Email,
                phoneNumber = model.PhoneNumber,
                Role = model.Role,
                Token = await _authService.CreateTokenAsync(user,_userManager)
            });
            


        }

        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {

            return await _userManager.FindByEmailAsync(email) is not null;

        }




        #region AddRole
        [HttpPost("AddRole")]
        public async Task<ActionResult> CreateRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (roleExist) return BadRequest(new ApiResponse(400, "Role already exit"));


            var rolseResult = await _roleManager.CreateAsync(new IdentityRole(name));
            return Ok(new ApiResponse(200, "Role added successfully"));

        }
        #endregion

    }
}
