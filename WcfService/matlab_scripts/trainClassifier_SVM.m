function [trainedClassifier, validationAccuracy] = trainClassifier_SVM(trainingData,...
    kernel_function,polynomial_degree,kernel_scale,box_constraint,used_features)
% trainClassifier(trainingData)
%  returns a trained classifier and its accuracy.
%  This code recreates the classification model trained in
%  Classification Learner app.
%
%   Input:
%       trainingData: the training data of same data type as imported
%        in the app (table or matrix).
%
%   Output:
%       trainedClassifier: a struct containing the trained classifier.
%        The struct contains various fields with information about the
%        trained classifier.
%
%       trainedClassifier.predictFcn: a function to make predictions
%        on new data. It takes an input of the same form as this training
%        code (table or matrix) and returns predictions for the response.
%        If you supply a matrix, include only the predictors columns (or
%        rows).
%
%       validationAccuracy: a double containing the accuracy in
%        percent. In the app, the History list displays this
%        overall accuracy score for each model.
%
%  Use the code to train the model with new data.
%  To retrain your classifier, call the function from the command line
%  with your original data or new data as the input argument trainingData.
%
%  For example, to retrain a classifier trained with the original data set
%  T, enter:
%    [trainedClassifier, validationAccuracy] = trainClassifier(T)
%
%  To make predictions with the returned 'trainedClassifier' on new data T,
%  use
%    yfit = trainedClassifier.predictFcn(T)
%
%  To automate training the same classifier with new data, or to learn how
%  to programmatically train classifiers, examine the generated code.

% Auto-generated by MATLAB on 23-Jul-2019 23:01:18


% Extract predictors and response
% This code processes the data into the right shape for training the
% classifier.
inputTable = trainingData;
predictorNames = {'Left_Ski_Accelerometer_X_axis', 'Left_Ski_Accelerometer_Y_axis', 'Left_Ski_Accelerometer_Z_axis', 'Left_Ski_Gyroscope_X_axis', 'Left_Ski_Gyroscope_Y_axis', 'Left_Ski_Gyroscope_Z_axis', 'Right_Ski_Accelerometer_X_axis', 'Right_Ski_Accelerometer_Y_axis', 'Right_Ski_Accelerometer_Z_axis', 'Right_Ski_Gyroscope_X_axis', 'Right_Ski_Gyroscope_Y_axis', 'Right_Ski_Gyroscope_Z_axis'};
predictors = inputTable(:, predictorNames);
response = inputTable.ClassifiedTurn;
isCategoricalPredictor = [false, false, false, false, false, false, false, false, false, false, false, false];

% Data transformation: Select subset of the features
% This code selects the same subset of features as were used in the app.
includedPredictorNames = predictors.Properties.VariableNames(used_features);
predictors = predictors(:,includedPredictorNames);
isCategoricalPredictor = isCategoricalPredictor(used_features);

% Train a classifier
% This code specifies all the classifier options and trains the classifier.
template = templateSVM(...
    'KernelFunction', kernel_function, ...
    'PolynomialOrder', polynomial_degree, ...
    'KernelScale', kernel_scale, ...
    'BoxConstraint', box_constraint, ...
    'Standardize', true);
classificationSVM = fitcecoc(...
    predictors, ...
    response, ...
    'Learners', template, ...
    'Coding', 'onevsone', ...
    'ClassNames', {'No turn'; 'Left turn'; 'Right turn'});

% Create the result struct with predict function
predictorExtractionFcn = @(t) t(:, predictorNames);
featureSelectionFcn = @(x) x(:,includedPredictorNames);
svmPredictFcn = @(x) predict(classificationSVM, x);
trainedClassifier.predictFcn = @(x) svmPredictFcn(featureSelectionFcn(predictorExtractionFcn(x)));

% Add additional fields to the result struct
trainedClassifier.RequiredVariables = {'Left_Ski_Accelerometer_X_axis', 'Left_Ski_Accelerometer_Y_axis', 'Left_Ski_Accelerometer_Z_axis', 'Left_Ski_Gyroscope_X_axis', 'Left_Ski_Gyroscope_Y_axis', 'Left_Ski_Gyroscope_Z_axis', 'Right_Ski_Accelerometer_X_axis', 'Right_Ski_Accelerometer_Y_axis', 'Right_Ski_Accelerometer_Z_axis', 'Right_Ski_Gyroscope_X_axis', 'Right_Ski_Gyroscope_Y_axis', 'Right_Ski_Gyroscope_Z_axis'};
trainedClassifier.ClassificationSVM = classificationSVM;
trainedClassifier.About = 'This struct is a trained classifier exported from Classification Learner R2016a.';
trainedClassifier.HowToPredict = sprintf('To make predictions on a new table, T, use: \n  yfit = c.predictFcn(T) \nreplacing ''c'' with the name of the variable that is this struct, e.g. ''trainedClassifier''. \n \nThe table, T, must contain the variables returned by: \n  c.RequiredVariables \nVariable formats (e.g. matrix/vector, datatype) must match the original training data. \nAdditional variables are ignored. \n \nFor more information, see <a href="matlab:helpview(fullfile(docroot, ''stats'', ''stats.map''), ''appclassification_exportmodeltoworkspace'')">How to predict using an exported model</a>.');

% Extract predictors and response
% This code processes the data into the right shape for training the
% classifier.
inputTable = trainingData;
predictorNames = {'Left_Ski_Accelerometer_X_axis', 'Left_Ski_Accelerometer_Y_axis', 'Left_Ski_Accelerometer_Z_axis', 'Left_Ski_Gyroscope_X_axis', 'Left_Ski_Gyroscope_Y_axis', 'Left_Ski_Gyroscope_Z_axis', 'Right_Ski_Accelerometer_X_axis', 'Right_Ski_Accelerometer_Y_axis', 'Right_Ski_Accelerometer_Z_axis', 'Right_Ski_Gyroscope_X_axis', 'Right_Ski_Gyroscope_Y_axis', 'Right_Ski_Gyroscope_Z_axis'};
predictors = inputTable(:, predictorNames);
response = inputTable.ClassifiedTurn;
isCategoricalPredictor = [false, false, false, false, false, false, false, false, false, false, false, false];

% Perform cross-validation
partitionedModel = crossval(trainedClassifier.ClassificationSVM, 'KFold', 5);

% Compute validation accuracy
validationAccuracy = 1 - kfoldLoss(partitionedModel, 'LossFun', 'ClassifError');

% Compute validation predictions and scores
[validationPredictions, validationScores] = kfoldPredict(partitionedModel);