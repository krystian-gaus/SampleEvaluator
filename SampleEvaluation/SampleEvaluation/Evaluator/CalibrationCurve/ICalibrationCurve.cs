using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEvaluation.Evaluator.CalibrationCurve
{
  internal interface ICalibrationCurve
  {
    #region Public Methods

    double GetValue(double concentration);

    #endregion Public Methods
  }
}
