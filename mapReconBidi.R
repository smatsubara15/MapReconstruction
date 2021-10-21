# To use this script, open the file in R. 

# You'll need to install the BiDimRegression package:
#install.packages("BiDimRegression")
## load up the packages
## use install.packages() to install new packages
library(plyr)
library(dplyr)
library(tidyverse)
library(reshape2)
library(BiDimRegression)
## set working directory

setwd("~/MapReconstruction/UserData")
thePath = "~/MapReconstruction/UserData/"

# need data for the actual locations of the objects
pathToTemplate<-"~/MapReconstruction/objectCorrectLocations.csv"
## ---------------------------
  
## read csv files
csv_files_ls <-  list.files(
  path=thePath, 
  pattern = "*.csv")

csv_files_df <- lapply(csv_files_ls, 
                       function(x) 
                       { 
                         tmp <- try(read.csv(
                           file = x, 
                           header = TRUE))
                         if (!inherits(tmp, 
                                       'try-error')) 
                           tmp
                       }
)

## combine all csv files together
data <- do.call("rbind", 
                       lapply(csv_files_df, 
                              as.data.frame)
)
 
templateData <- read.csv(pathToTemplate)
  
# Rename and initialize a few variables
data$depV1 <- data$xPos
data$depV2 <- data$yPos
results <- data.frame()
  
# 12 objects to place.
observations <- 12
participants <- nrow(data)/observations
  
# Loop through each participant's data set, run it through the bidimensional regression,
# Save the data to the results. 
for (i in 1:nrow(data)){ 
    
  if (i%%observations==0){ 
      
    # Select the data to be analyzed
    tempData <- data[c((i-(observations-1)):i),]
    id <- tempData[1,'ParticipantID']
    tempData <- tempData[,c(5,6)]
    tempData$indepV1 <- templateData$indepV1
    tempData$indepV2 <- templateData$indepV2
      
    temp_results <- BiDimRegression(tempData)
  
    results[i/observations,'participant'] <- as.character(id)
    results[i/observations,'Overall_r'] <- temp_results$euclidean.r
    results[i/observations,'Overall_rsquared'] <- temp_results$euclidean.rsqr
    results[i/observations,'Overall_angle'] <- temp_results$euclidean.angleDEG
    results[i/observations,'Overall_x_scale'] <- temp_results$euclidean.scaleFactorX
    results[i/observations,'Overall_y_scale'] <- temp_results$euclidean.scaleFactorY
    results[i/observations,'Overall_distortion_index'] <- temp_results$euclidean.diABSqr
  }
}
write.csv(results,".\\silctonBidiOut.csv", row.names = FALSE)