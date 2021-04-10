import { IProduct } from './Product.d';

export interface IPurchase {
    id: string;
    quantity: int;
    totalPrice: double;
    buyDate: date;
    dateCreated: date;
    dateUpdated: date;
    productId: string;
    product: IProduct;
}