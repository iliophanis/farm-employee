export type ISubEmployeeContactInfo = {
  address: string;
  city: string;
  tk: string;
  phoneNo: string;
  mobilePhoneNo: string;
};

export type ISubEmployee = {
  firstName: string;
  lastName: string;
  email: string;
  contactInfo: ISubEmployeeContactInfo;
};

export type ISubmitRequest = {
  requestId: number;
  subEmployees: ISubEmployee[];
};
