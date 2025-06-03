import axios from "axios";
import type { UserLogin, UserLoginResponse } from "../Types/UserInterface";

const API = import.meta.env.VITE_API_URL;

export const login = async (user: UserLogin): Promise<UserLoginResponse> => {
  const respone = await axios.post<UserLoginResponse>(
    `${API}/Auth/login`,
    user
  );
  return respone.data;
};
