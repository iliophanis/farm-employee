export interface IUserResponse {
  token: string;
  expiration: Date;
  displayName: string;
  userId: number;
  role: string;
  picture: string;
}

export interface IGoogleAuthResponse {
  isNewUser: boolean;
}

export interface IAddRole {
  roleId: number;
  userName: string;
  contactInfo?: {
    address: string;
    city: string;
    tk: string;
    phoneNo: string;
    mobilePhoneNo: string;
  };
}
