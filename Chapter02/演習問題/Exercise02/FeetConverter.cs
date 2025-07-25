﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter{
   public class InchConverter {
        //定数
        private const double raito = 0.0254;

        //メートルからインチを求める
        public static double FromMeter(double meter) {
            return meter / raito;
    }

        //インチからメートルを求める
        public static double ToMeter(double feet) { 
            return feet * raito;
        }
    }
}
