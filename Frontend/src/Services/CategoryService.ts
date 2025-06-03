import axios from "axios";
import type { Category } from "../Types/CategoryInterface";



const API = import.meta.env.VITE_API_URL;

export const getCategories = async (): Promise<Category[]> => {
    const respones = await axios.get<Category[]>(`${API}/Categories`);
    return respones.data;
}

export const addCategory = async (name: string): Promise<Category> => {
  const response = await axios.post<Category>(`${API}/Categories`, { name });
  return response.data;
};