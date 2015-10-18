setwd("~/Google Drive/CS 673/CS473-673 Project Shareables/Database")
SourceData <- read.csv(file="PlantSourceData.txt", header=T)

attach(SourceData)
View(SourceData)
# exclude variables
# Synonym.Symbol, Image.Gallery, Characteristics.Data
myvars <- names(SourceData) %in% c("Synonym.Symbol", "Image.Gallery", "Characteristics.Data") 
SourceData_modified <- SourceData[!myvars]
detach(SourceData)

# rename variables
names(SourceData_modified)
names(SourceData_modified)[names(SourceData_modified)=="Accepted.Symbol"] <- "plant_id"
names(SourceData_modified)[names(SourceData_modified)=="Scientific.Name"] <- "name"
names(SourceData_modified)[names(SourceData_modified)=="State.and.Province"] <- "us_state"
names(SourceData_modified)[names(SourceData_modified)=="Category"] <- "pattern"
names(SourceData_modified)[names(SourceData_modified)=="Growth.Habit"] <- "type"
names(SourceData_modified)[names(SourceData_modified)=="Flower.Color"] <- "color_flower"
names(SourceData_modified)[names(SourceData_modified)=="Foliage.Color"] <- "color_foliage"
names(SourceData_modified)[names(SourceData_modified)=="Fruit.Color"] <- "color_fruit"
names(SourceData_modified)[names(SourceData_modified)=="Foliage.Texture"] <- "texture_foliage"
names(SourceData_modified)[names(SourceData_modified)=="Shape.and.Orientation"] <- "shape"

# create separate tables
### "location" table
# select variables
myvars <- c("plant_id", "us_state")
TABLE_locations <- SourceData_modified[myvars]
attach(TABLE_locations)
states = c("AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "DC", "FM", "GU", "MH", "MP", "PW", "PR", "VI")

# indicate if a plant is present in a state with dummy vars
# takes approximately 20 min to run.
Sys.time()
for (i in 1:nrow(TABLE_locations)){
  for (j in states) {
    if (grepl(j, TABLE_locations[i,"us_state"])) TABLE_locations[i, paste0(j)] <- 1
    else TABLE_locations[i, paste0(j)] <- 0
  }
}
Sys.time()

# use package tidyr for reshape
View(TABLE_locations)
# install.packages("tidyr")
library(tidyr)

# reshape from wide to long ds
sapply(TABLE_locations, class)
TABLE_locations_long <- gather(TABLE_locations, state, present, AK:VI)
TABLE_locations_long <- TABLE_locations_long[TABLE_locations_long$present==1,]
TABLE_locations_long[["us_state"]] <- NULL
TABLE_locations_long[["present"]] <- NULL
detach(TABLE_locations)

# indicate if a plant is a certain plant type with dummy vars
# "plant_type" table
# select variables
myvars <- c("plant_id", "type")
TABLE_plant_type <- SourceData_modified[myvars]
attach(TABLE_plant_type)
plant_type= c("Forb/herb", "Graminoid", "Lichenous", "Nonvascular", "Shrub", "Subshrub", "Tree", "Vine")

# takes 3 minutes to run
Sys.time()
for (i in 1:nrow(TABLE_plant_type)){
  for (j in plant_type) {
    if (grepl(j, TABLE_plant_type[i,"type"])) TABLE_plant_type[i, paste0(j)] <- 1
    else TABLE_plant_type[i, paste0(j)] <- 0
  }
}
Sys.time()

# reshape from wide to long ds
sapply(TABLE_plant_type, class)
# rename "forb/herb" column before reshaping from wide to long
names(TABLE_plant_type)[names(TABLE_plant_type)=="Forb/herb"] <- "Forb_herb"
TABLE_plant_type_long <- gather(TABLE_plant_type, type1, present, Forb_herb:Vine)

View(TABLE_plant_type_long)
sapply(TABLE_plant_type_long, class)
TABLE_plant_type_long <- TABLE_plant_type_long[TABLE_plant_type_long$present==1,]
levels(TABLE_plant_type_long$type1)
levels(TABLE_plant_type_long$type1)[levels(TABLE_plant_type_long$type1)=="Forb_herb"] <- "Forb/herb"
TABLE_plant_type_long <- subset(TABLE_plant_type_long, select=-type)
TABLE_plant_type_long <- subset(TABLE_plant_type_long, select=-present)
names(TABLE_plant_type_long)[names(TABLE_plant_type_long)=="type1"] <- "type"
detach(TABLE_plant_type)


# "plant" table
# exclude variables: us_state, type
myvars <- names(SourceData_modified) %in% c("us_state", "type") 
TABLE_plant <- SourceData_modified[!myvars]

# print to csv file
write.table(SourceData_modified, "~/Google Drive/CS 673/CS473-673 Project Shareables/Database/SourceData_modified.txt", sep=",")
write.table(TABLE_locations_long, "~/Google Drive/CS 673/CS473-673 Project Shareables/Database/TABLE_location.txt", sep=",")
write.table(TABLE_plant_type_long, "~/Google Drive/CS 673/CS473-673 Project Shareables/Database/TABLE_plant_type.txt", sep=",")
write.table(TABLE_plant, "~/Google Drive/CS 673/CS473-673 Project Shareables/Database/TABLE_plant.txt", sep=",")
