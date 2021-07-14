using DapperRepository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperRepository.Core;
using DapperRepository.Repos.Interfaces;
using System.Reflection;

namespace DapperRepository.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        ICatRepos _catRepos;
        public CategoriesController( ICatRepos catRepos )
        {
            _catRepos = catRepos;
        }
        [HttpGet]
        public List<Categories> List()
        {
            return _catRepos.GetAll();
        }
        [HttpPost]
        public void  Insert([FromBody] Categories cat)
        {
             
            _catRepos.Insert(cat, "Categories");
        }
        [HttpPost("{id:int}")]
        public void Update([FromBody] Categories cat,int id)
        {

            _catRepos.Update(cat, "Categories",id);
        }
    }
}
