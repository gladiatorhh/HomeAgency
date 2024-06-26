﻿using HomeAgency.Domain.Entities;
using System.Linq.Expressions;

namespace HomeAgency.Application.Common.Interfaces;

public interface IVillaRepository : IRepository<Villa>
{
    void Update(Villa villa);
    void SaveChanges();
}