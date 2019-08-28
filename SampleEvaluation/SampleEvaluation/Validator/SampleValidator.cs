using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEvaluation.Dto;
using SampleEvaluation.Helper;

namespace SampleEvaluation.Validator
{
  internal class SampleValidator
  {
    #region Public Methods

    public void CheckSamples(IEnumerable<SampleDto> samples)
    {
      ExceptionHelper.CheckArgumentNullOrEmpty(samples, "samples");

      IEnumerable<SampleDto> negativeSamples = samples.Where(x => x.Concentration < 0 || x.Value < 0);
      if (negativeSamples.Any())
      {
        throw new Exception("There exist samples with negative concentration and/or negative value");
      }
      else
      {
        //ok
      }
    }

    public void CheckCalibrationSamples(IEnumerable<SampleDto> calibrationSamples, int calibrationSampleQuantity)
    {
      ExceptionHelper.CheckArgumentNull(calibrationSamples, "calibrationSamples");

      if(calibrationSampleQuantity < 2)
      {
        throw new Exception("The minimal calibration samples quantity must be greater than or equal to 2");
      }
      else
      {
        //ok
      }

      IEnumerable<double> concentrations = calibrationSamples.Select(x => x.Concentration).Distinct();
      if(concentrations.Count() == calibrationSamples.Count())
      {
        //ok
      }
      else
      {
        throw new Exception("Calibration samples have samples with same concentration");
      }

      IEnumerable<SampleDto> negativeSamples = calibrationSamples.Where(x => x.Concentration < 0 || x.Value < 0);
      if(negativeSamples.Any())
      {
        throw new Exception("Calibration samples have samples with negative concentration and/or negative value");
      }
      else
      {
        //ok
      }

      IEnumerable<SampleDto> zeroValueSamples = calibrationSamples.Where(x => x.Concentration > 0 && x.Value == 0);
      if (zeroValueSamples.Any())
      {
        throw new Exception("Calibration samples have samples with positive concentration and zero value");
      }
      else
      {
        //ok
      }

      IEnumerable<SampleDto> zeroConcentrationSamples = calibrationSamples.Where(x => x.Concentration == 0 && x.Value >= 0);
      if (zeroConcentrationSamples.Count() == 1)
      {
        //ok
      }
      else
      {
        throw new Exception("Calibration samples have no or more than one sample with zero concentration");
      }

      IEnumerable<SampleDto> orderedByDescendingConcentrationSamples = calibrationSamples.OrderByDescending(x => x.Concentration);
      double valueOfLastSample = orderedByDescendingConcentrationSamples.ElementAt(0).Value;
      double valueOfSecondLastSample = orderedByDescendingConcentrationSamples.ElementAt(1).Value;
      if (valueOfSecondLastSample < valueOfLastSample)
      {
        //ok
      }
      else
      {
        throw new Exception("The slope of the line between the last two calibration samples is negative");
      }

      if(calibrationSamples.Count() == calibrationSampleQuantity)
      {
        //ok
      }
      else
      {
        throw new Exception(string.Format("The quantity of the calibration samples is different from the predefined quantity {0}", calibrationSampleQuantity));
      }
    }

    #endregion Public Methods
  }
}
