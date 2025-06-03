import axios from "axios";
import type { currentUsers, Users } from "../Types/UserInterface";


const API = import.meta.env.VITE_API_URL;

export const GetAllUsers = async (): Promise<Users[]> => {
    const token = localStorage.getItem("token");
    const response = await axios.get<Users[]>(`${API}/User`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
}

export const GetCurrentUser = async (): Promise<currentUsers> => {
  const token = localStorage.getItem("token");
  const response = await axios.get<currentUsers>(`${API}/User/me`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  return response.data;
};
// Add a new user
export const addUser = async (user: Users): Promise<void> => {
  await axios.post(`${API}/Auth/register`, user)
}

// Delete a user
export const deleteUser = async (id: number): Promise<void> => {
  await axios.delete(`${API}/User/${id}`);
};