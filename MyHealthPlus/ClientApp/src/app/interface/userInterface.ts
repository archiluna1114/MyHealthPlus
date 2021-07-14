export interface IUser {
  userId: number;
  firstName: string;
  lastName: string;
  roldeId?: number;
  email: string;
  address: string;
  password: string;
  phoneNumber: string;
  isDeleted?: boolean;
}
