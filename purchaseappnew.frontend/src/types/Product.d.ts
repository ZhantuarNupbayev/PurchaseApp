import { ICategory } from '@/types/Category';
export interface IProduct {
    id: string;
    productName: string;
    price: number;
    category: ICategory;
}