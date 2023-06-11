import {
  UserRequestFarmer,
  UserRequestLocation,
} from '@/modules/home/models/IUserRequest';

export type IEmployeeRequestResponse = {
  data: IEmployeeRequestItem[];
  totalSize: number;
  totalPages: number;
};

export type IEmployeeRequestItem = {
  employeeRequestId: number;
  requestId: number;
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

export const EmptyEmployeeRequestRes = (): IEmployeeRequestResponse => {
  return {
    data: [],
    totalPages: 0,
    totalSize: 0,
  };
};
