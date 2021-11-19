export interface ProductsExpense {
    id: number,
    productId: number,
    userId: string,
    baseInfoId: number,
    purchasingDate: Date,
    isExpired: boolean
}