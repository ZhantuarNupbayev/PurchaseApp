import { ICategory } from "@/types/Category";
import axios from "axios";
import authHeader from "./auth-header";

export default class CategoryService {
    API_URL = process.env.VUE_APP_API_URL;

    async getCategories() {
        let result = await axios.get(`${this.API_URL}/categories`, { headers: authHeader() });
        return result.data;
    }

    async getCategoryById(id: string) {
        let result = await axios.get(`${this.API_URL}/categories/${id}`, { headers: authHeader() });
        return result.data;
    }

    async save(newCategory: ICategory) {
        let result = await axios.post(`${this.API_URL}/categories/`, newCategory, { headers: authHeader() });
        return result.data;
    }
}