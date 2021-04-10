import { IProduct } from "@/types/Product";
import axios from "axios";
import authHeader from "./auth-header";

export default class ProductService {
    API_URL = process.env.VUE_APP_API_URL;

    async getProducts(categoryId: string) {
        let result = await axios.get(`${this.API_URL}/categories/${categoryId}/products`,
        { headers: authHeader() });
        return result.data;
    }

    async getProductById(categoryId: string, productId: string) {
        let result = await axios.get(`${this.API_URL}/categories/${categoryId}/products/${productId}`,
        { headers: authHeader() });
        return result.data;
    }

    async save(categoryId: string, newProduct: IProduct) {
        let result = await axios.post(`${this.API_URL}/categories/${categoryId}/products`, newProduct,
        { headers: authHeader() });
        return result.data;
    }
}