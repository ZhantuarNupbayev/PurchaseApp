import { IPurchase } from "@/types/Purchase";
import axios from "axios";
import authHeader from "./auth-header";
import TimeChangeService from "@/utils/time-change-service";

const timeChangeService = new TimeChangeService();

export default class PurchaseService {
    API_URL = process.env.VUE_APP_API_URL;
    
    async getPurchasesByUser() {
        
        let result = await axios.get(`${this.API_URL}/purchases`, { headers: authHeader() });
        
        (result.data as Array<IPurchase>).map(purchase => {
            purchase.dateCreated = timeChangeService.formatDate(purchase.dateCreated);
            purchase.buyDate = timeChangeService.formatDate(purchase.buyDate);
        });
       
        return result.data;
    }

    async getPurchasesByDate(startDate: string, endDate: string) {
        let result = await axios.get(`${this.API_URL}/purchases?StartDate=${startDate}&endDate=${endDate}`,
         { headers: authHeader() });

         (result.data as Array<IPurchase>).map(purchase => {
            purchase.dateCreated = timeChangeService.formatDate(purchase.dateCreated);
            purchase.buyDate = timeChangeService.formatDate(purchase.buyDate);
        });

        return result.data;
    }

    async getPurchaseById(id: string) {
        let result = await axios.get(`${this.API_URL}/purchases/${id}`, { headers: authHeader() });

        return result.data;
    }

    async save(newPurchase : IPurchase) {
        newPurchase.dateCreated = timeChangeService.getCurrentDate();

        let result = await axios.post(`${this.API_URL}/purchases`, newPurchase, { headers: authHeader() });

        return result.data;
    }

    async update(editPurchase: IPurchase) {
        let newPurchase = {
            "productId": editPurchase.productId,
            "buyDate": editPurchase.buyDate,
            "dateCreated": editPurchase.dateCreated,
            "dateUpdated": timeChangeService.getCurrentDate(),
            "quantity": editPurchase.quantity
        };
        let result = await axios.put(`${this.API_URL}/purchases/${editPurchase.id}`, newPurchase, { headers: authHeader() });

        return result.data;
    }

}