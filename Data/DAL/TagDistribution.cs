﻿using System;

namespace Data.DAL
{
    internal class TagDistribution
    {
        public long tagId { get; set; }
        public string filmId { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}
