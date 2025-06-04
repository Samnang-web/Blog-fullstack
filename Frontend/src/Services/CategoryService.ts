import axios from "axios";
import type { Category } from "../Types/CategoryInterface";



const API = import.meta.env.VITE_API_URL;

export const getCategories = async (): Promise<Category[]> => {
  const token = localStorage.getItem("token");
    const respones = await axios.get<Category[]>(`${API}/Categories`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return respones.data;
    
}

export const addCategory = async (name: string): Promise<Category> => {
  const response = await axios.post<Category>(`${API}/Categories`, { name });
  return response.data;
};

export const deleteCategory = async (id: number): Promise<void> => {
  await axios.delete(`${API}/Categories/${id}`);
};