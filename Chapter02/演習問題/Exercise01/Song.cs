﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    //2.1.1
    public class Song {
        public string Title { get; private set; }
        public string ArtistName { get; private set; }
        public int Length { get; set; }


        //2.1.2
        public Song(String Title, String ArtistName, int Length) {
            this.Title = Title;
            this.ArtistName = ArtistName;
            this.Length = Length;
        }

    }



}
