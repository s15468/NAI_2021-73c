﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace Lab4_SvmClassification
{
    public partial class BanknoteData
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"Variance", @"Variance"),new InputOutputColumnPair(@"Skewness", @"Skewness"),new InputOutputColumnPair(@"Kurtosis", @"Kurtosis"),new InputOutputColumnPair(@"Entropy", @"Entropy")})      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Variance",@"Skewness",@"Kurtosis",@"Entropy"}))      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(@"Class", @"Class"))      
                                    .Append(mlContext.MulticlassClassification.Trainers.LightGbm(new LightGbmMulticlassTrainer.Options(){NumberOfLeaves=18,MinimumExampleCountPerLeaf=12,NumberOfIterations=47,MaximumBinCountPerFeature=149,LearningRate=0.720257144209729F,LabelColumnName=@"Class",FeatureColumnName=@"Features",Booster=new GradientBooster.Options(){SubsampleFraction=0.616048876948395F,FeatureFraction=0.782742139436296F,L1Regularization=1.96622188243019E-08F,L2Regularization=213354.762004385F}}))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(@"PredictedLabel", @"PredictedLabel"));

            return pipeline;
        }
    }
}
