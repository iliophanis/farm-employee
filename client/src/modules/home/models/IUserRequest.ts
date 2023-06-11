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
  actions: string[];
};

export type UserRequestLocation = {
  longitude: number;
  latitude: number;
  displayName: string;
};

export type UserRequestFarmer = {
  id: number;
  name: string;
  email: string;
  contactInfo: string;
  avgRate: number;
  avgWorkPlaceRate: number;
  avgPaymentConsequenceRate: number;
};
