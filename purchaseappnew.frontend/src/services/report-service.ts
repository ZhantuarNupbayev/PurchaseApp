import axios from 'axios';
import authHeader from "./auth-header";

export default class ReportService {
    API_URL = process.env.VUE_APP_API_URL;

    async getPurchasesByCategory() {
        let result = await axios.get(`${this.API_URL}/reports`, { headers: authHeader() });

        return result.data;
    }

    async getMaximumPurchase() {
        let result = await axios.get(`${this.API_URL}/reports/max`, { headers: authHeader() });

        return result.data;
    }

    async getMinimumPurchase() {
         let result = await axios.get(`${this.API_URL}/reports/min`, { headers: authHeader() });

        return result.data;
    }
}