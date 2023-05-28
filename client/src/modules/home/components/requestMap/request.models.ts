export type UserRequest = {
  id: number;
  jobType: string;
  startJobDate: string;
  estimatedDuration?: number;
  price?: number;
  stayAmount?: number;
  travelAmount?: number;
  foodAmount?: number;
  cultivationName: string;
  farmer: UserRequestFarmer;
  location: UserRequestLocation;
};

type UserRequestLocation = {
  longitude: number;
  latitude: number;
  displayName: string;
};

type UserRequestFarmer = {
  id: number;
  name: string;
  email: string;
};
