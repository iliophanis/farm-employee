import {
  UserRequestFarmer,
  UserRequestLocation,
} from '@/modules/home/models/IUserRequest';

enum PaymentMethod {
  bankTransfer = 'bankTransfer',
  paypal = 'paypal',
  ebanking = 'ebanking',
}

enum PaymentStatus {
  pendingPayment = 'pendingPayment',
  processing = 'processing',
  onHold = 'onHold',
  completed = 'completed',
  canceled = 'canceled',
  refunded = 'refunded',
  failed = 'failed',
}

export type IFarmerRequestResponse = {
  data: IFarmerRequestItem[];
  totalSize: number;
  totalPages: number;
};

export type SubEmployeeRequestDto = {
  name: string;
  email: string;
  contactInfo: string;
};

export type EmployeeRequestDto = {
  messageSent?: boolean;
  paymentMethod?: PaymentMethod;
  paymentStatus: PaymentStatus;
  employeeId: number;
  name: string;
  email: string;
  contactInfo: string;
  avgRate?: number;
  avgJobQuality?: number;
  avgContactQuality?: number;
  avgPrice?: number;
  subEmployees: SubEmployeeRequestDto[];
};

export type IFarmerRequestItem = {
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
  employeeRequests: EmployeeRequestDto[];
};

export const EmptyEmployeeRequestRes = (): IFarmerRequestResponse => {
  return {
    data: [],
    totalPages: 0,
    totalSize: 0,
  };
};
