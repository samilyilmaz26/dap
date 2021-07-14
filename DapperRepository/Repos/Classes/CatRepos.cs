using DapperRepository.Core;
using DapperRepository.Data;
using DapperRepository.Repos.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperRepository.Repos.Classes
{
    public class CatRepos:BaseRepository<Categories>,ICatRepos
    {
        public CatRepos(IConfiguration conf ):base( conf  )
        {

        }
        
    }
}
