﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code Has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code should be max of 3 char")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name should be max of 50 char")]
        public string Name { get; set; }
        public string RegionImageUrl { get; set; }
    }
}
