export interface User {
  id: number;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  token?: string;
  roleId?: number;
}
