﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudents()
        {
            string[] students = new string[]
            {
                "Student 1",
                "Student 2",
                "Student 3"
            };
            return Ok(students);
        }
    }
}
