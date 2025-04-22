using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {
    //メートルからフィートを求める
    public static class FeetConverter {

        public static double FromMeter(double meter) {
            return meter / 0.3048;
        }

        //フィートからメートルを求める
        public static double ToMeter(double feet) {
            return feet * 0.3048;
        }
    }
}
