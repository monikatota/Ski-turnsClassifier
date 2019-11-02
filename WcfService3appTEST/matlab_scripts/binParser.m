% Parser compatible with both Matlab and Octave.
% Data format:
% 1 - timestamp
% 2 - flag
% 3:11 - central
% 12:20 - left tag
% 21:29 - right tag
% 30:32 - connection status
% 33:35 - GPS coords
% 36:37 - GPS position errors [m]
% 38:39 - heading [rad]
% 40:41 - velocity horizontal and vertical
% 42 - velocity horizontal error
% 43 - temperature
% 44 - pressure

function [ret] = binParser(file)
  
  fid = fopen(file, 'r');
  
  finalData = [0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0];
  
   i = 1;
   while (~feof(fid))
     
    central = [0; 0; 0; 0; 0; 0; 0; 0; 0];
    left = [0; 0; 0; 0; 0; 0; 0; 0; 0];
    right = [0; 0; 0; 0; 0; 0; 0; 0; 0];     
    connect = [0; 0; 0];
    gps = [0; 0; 0];
    posErrors = [0; 0];
    heading = [0;0];
    velHorizontal = 0;
    velVertical = 0;
    velHorizErr = 0;
    press = 0;
    temp = 0;
    
    timestamp = fread(fid, 1, 'uint32');
    if (size(timestamp) == 0)
      break;
    end;
    
    flag = fread(fid, 1, 'uint32');
    
    binaryFlag = dec2bin(flag, 6);
    
    if (binaryFlag(6) == '1') %central
      central = fread(fid, 9, 'float32');
    end
    if (binaryFlag(5) == '1')%left tag
      left = fread(fid, 9, 'float32');
    end;
    if (binaryFlag(4) == '1')%right tag
      right = fread(fid, 9, 'float32');
    end;
    if (binaryFlag(3) == '1')%gps
      gps = fread(fid, 3, 'float32');
      posErrors = fread(fid, 2, 'float32');
      heading = fread(fid, 2, 'float32');
      velHorizontal = fread(fid, 1, 'float32');
      velVertical = fread(fid, 1, 'float32');
      velHorizErr = fread(fid, 1, 'float32');
    end;
    if (binaryFlag(2) == '1')%pressure
      press = fread(fid, 1, 'float32');
    end;
    if (binaryFlag(1) == '1')%temperature
      temp = fread(fid, 1, 'float32');
    end;
      
    data = vertcat(timestamp, flag, central, left, right, connect, gps, ... 
        posErrors, heading, velHorizontal, velVertical, velHorizErr, temp, press);
    data = data';
    finalData(i, 1:44) = data;
    i = i + 1;
    end
  
  fclose(fid);
  
  fileID = fopen('output.txt','w');
          formatSpec = '%d %d %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f %0.10f\n';
          fprintf(fileID, formatSpec, finalData.');
          fclose(fileID);
          
  ret = finalData;
end