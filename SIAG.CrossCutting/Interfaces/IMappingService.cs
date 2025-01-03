﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.Interfaces
{
    public interface IMappingService
    {
        TDestination Map<TSource, TDestination>(TSource source);
        List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);
    }
}
