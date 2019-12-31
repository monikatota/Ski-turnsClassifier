%Data filtration and classification
function [Left_Ski_Gyroscope_X_axis,...
          Left_Ski_Gyroscope_Y_axis,...
          Left_Ski_Gyroscope_Z_axis,...
		  Right_Ski_Gyroscope_X_axis,...
          Right_Ski_Gyroscope_Y_axis,...
          Right_Ski_Gyroscope_Z_axis,...
          classified_labels, COUNT_LEFT_TURN, COUNT_RIGHT_TURN] = classify_turns( file )

%LOAD TRAINED MODEL
load('SVM_model');

%PREPARE DATA TO CLASSIFICATION
dataset_to_classify=binParser(file);
Left_Ski_Accelerometer_X_axis=movmean(dataset_to_classify(:,12),50);
Left_Ski_Accelerometer_Y_axis=movmean(dataset_to_classify(:,13),50);
Left_Ski_Accelerometer_Z_axis=movmean(dataset_to_classify(:,14),50);
Left_Ski_Gyroscope_X_axis=movmean(dataset_to_classify(:,15),50);
Left_Ski_Gyroscope_Y_axis=movmean(dataset_to_classify(:,16),50);
Left_Ski_Gyroscope_Z_axis=movmean(dataset_to_classify(:,17),50);
Right_Ski_Accelerometer_X_axis=movmean(dataset_to_classify(:,21),50);
Right_Ski_Accelerometer_Y_axis=movmean(dataset_to_classify(:,22),50);
Right_Ski_Accelerometer_Z_axis=movmean(dataset_to_classify(:,23),50);
Right_Ski_Gyroscope_X_axis=movmean(dataset_to_classify(:,24),50);
Right_Ski_Gyroscope_Y_axis=movmean(dataset_to_classify(:,25),50);
Right_Ski_Gyroscope_Z_axis=movmean(dataset_to_classify(:,26),50);
SkiTurns_dataset_to_classify=table(Left_Ski_Accelerometer_X_axis,Left_Ski_Accelerometer_Y_axis,Left_Ski_Accelerometer_Z_axis,...
    Left_Ski_Gyroscope_X_axis,Left_Ski_Gyroscope_Y_axis,Left_Ski_Gyroscope_Z_axis,...
    Right_Ski_Accelerometer_X_axis,Right_Ski_Accelerometer_Y_axis,Right_Ski_Accelerometer_Z_axis,...
    Right_Ski_Gyroscope_X_axis,Right_Ski_Gyroscope_Y_axis,Right_Ski_Gyroscope_Z_axis);


%CLASSIFY TURNS
predictions = SVM_model.predictFcn(SkiTurns_dataset_to_classify);
%write labels in numerical form to display results on the plot
classified_labels=zeros(size(predictions,1),1);
for i=1:size(predictions,1)
    if strcmp(predictions(i),'Right turn')
        classified_labels(i,1)=-1;
    elseif strcmp(predictions(i),'Left turn')
        classified_labels(i,1)=1;
    elseif strcmp(predictions(i),'No turn')
        classified_labels(i,1)=0;
    end
end

% filtration of predictions and calculating number of detected ski-turns

COUNT_LEFT_TURN = 0;
COUNT_RIGHT_TURN = 0;

for i=2:size(classified_labels,1)
    % turn left
    if(classified_labels(i)==1 && classified_labels(i-1)~=1 || classified_labels(i)==1 && i==2 )
        length_of_turn = 1;
        j=i+1;
        while(classified_labels(j)==1)
            length_of_turn = length_of_turn + 1;
            j=j+1;
        end
        if(length_of_turn < 30)
            % remove short turns
            for k=i:(i+length_of_turn-1)
                classified_labels(k)=0;
            end
            if(i==2)
               classified_labels(1)=0;
            end
        else
            COUNT_LEFT_TURN = COUNT_LEFT_TURN + 1;
        end
    % turn right  
    elseif(classified_labels(i)==-1 && classified_labels(i-1)~=-1 || classified_labels(i)==-1 && i==2 )
        length_of_turn = 1;
        j=i+1;
        while(classified_labels(j)==-1)
            length_of_turn = length_of_turn + 1;
            j=j+1;
        end

        if(length_of_turn < 30)
            % remove short turns
            for k=i:(i+length_of_turn-1)
                classified_labels(k)=0;
            end
            if(i==2)
               classified_labels(1)=0;
            end
        else
            COUNT_RIGHT_TURN = COUNT_RIGHT_TURN + 1;
        end
    end
end

end
