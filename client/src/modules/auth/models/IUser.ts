export interface IUserResponse {
  token: string;
  expiration: Date;
  displayName: string;
  userId: number;
  roles: string[];
}

export interface IGoogleAuthResponse {
  isNewUser: boolean;
}
