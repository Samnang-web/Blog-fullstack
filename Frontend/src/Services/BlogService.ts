import axios from "axios"
import type { Blog } from "../Types/BlogInterface";


const API = import.meta.env.VITE_API_URL;

// export const getBlog = () => axios.get<Blog[]>(`${API}/Blog`);  (option 1)
export const getBlog = async (): Promise<Blog[]> => {
    const respone = await axios.get<Blog[]>(`${API}/Blog`);
    return respone.data;
}

export const getBlogById = async (id: number): Promise<Blog> => {
  const response = await axios.get<Blog>(`${API}/Blog/${id}`)
  return response.data
}

export const addBlog = async (formData: FormData) => {
  const token = localStorage.getItem('token') // or however you store it
  return await axios.post(`${API}/Blog`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
      Authorization: `Bearer ${token}`
    }
  })
}



export const updateBlog = async (id: number, blog: Blog): Promise<Blog> => {
  const token = localStorage.getItem('token');
  if (!token) throw new Error('No token found');

  const response = await axios.put<Blog>(`${API}/Blog/${id}`, blog, {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

  return response.data;
};

export const deleteBlogId = async (id: number) => {
  const token = localStorage.getItem('token');
  return await axios.delete(`${API}/Blog/${id}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
}















