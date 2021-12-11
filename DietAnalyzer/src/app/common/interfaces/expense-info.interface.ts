export interface ExpenseInfo {
    name: string,
    price: number,
    purchasingDate: string | null,
    expirationDate: string | null,
    isExpired: boolean;
}