﻿using Core.DTOs;
using Core.Models;

namespace Core.Response
{
    public class CategoryApiResponse
    {
        public CategoryDtos data { get; set; }
        public object Errors { get; set; }
        public CategoryUpdateDtos updateDtosdata { get; set; }
        

    }
}
