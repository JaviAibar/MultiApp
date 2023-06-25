using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataManagingModule.Model
{
    public class Mill
    {
        public string CaseName { get; set; }
        public double Run { get;  set;  }
        public double VB1 { get;  set;  }
        public double Time { get;  set;  }
        public double DOC1 { get;  set;  }
        public double Feed { get;  set;  }
        public double Material { get;  set;  }
        public double SmcAC { get;  set;  }
        public double SmcDC { get;  set;  }
        public double Vib_table { get;  set;  }
        public double Vib_spindle { get;  set;  }
        public double AE_table1 { get;  set;  }
        public double AE_spindle1 { get;  set;  }

        public Mill()
        {
            
        }
        public Mill(string caseName, double run, double VB, double time, double DOC, double feed, double material, double smcAC, double smcDC, double vib_table, double vib_spindle, double AE_table, double AE_spindle)
        {
            CaseName = caseName;
            Run = run;
            VB1 = VB;
            Time = time;
            DOC1 = DOC;
            Feed = feed;
            Material = material;
            SmcAC = smcAC;
            SmcDC = smcDC;
            Vib_table = vib_table;
            Vib_spindle = vib_spindle;
            AE_table1 = AE_table;
            AE_spindle1 = AE_spindle;
        }

        public override string ToString()
        {
            return $"{CaseName},{Run},{VB1},{Time},{DOC1},{Feed},{Material},{SmcAC},{SmcDC},{Vib_table},{Vib_spindle},{AE_table1},{AE_spindle1}";
        }
    }
}
