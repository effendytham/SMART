using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Module.Services
{
    public interface IElasticSeed
    {
        ServiceResponse Seed();
    }
}
