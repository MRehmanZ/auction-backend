﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuctionBackend.Models

{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public ApplicationUser() {
            Auctions = new List<Auction>();
            Comments = new List<Comment>();
            AuctionRecords = new List<AuctionRecord>();
            Bids = new List<Bid>();
        }

    [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property for Auctions created by the user
        public virtual ICollection<Auction> Auctions { get; set; }

        // Navigation property for Bids placed by the user
        public virtual ICollection<Bid> Bids { get; set; }

        // Navigation property for Comments posted by the user
        public virtual ICollection<Comment> Comments { get; set; }

        // Navigation property for AuctionRecords associated with the user
        public virtual ICollection<AuctionRecord> AuctionRecords { get; set; }

        [JsonIgnore]
        [Precision(18, 2)]
        public decimal Balance { get; set; } = decimal.Zero;
    }
}
