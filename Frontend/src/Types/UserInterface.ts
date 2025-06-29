export interface UserLogin {
  email: string;
  password: string;
}

export interface UserLoginResponse {
  id: number;
  name: string;
  email: string;
  role: string;
  token: string;
}
export interface Users {
  id: number;
  name: string;
  email: string;
  password?: string;
  role: string;
}

export interface currentUsers{
  name: string;
  email: string;
  role: string;
}