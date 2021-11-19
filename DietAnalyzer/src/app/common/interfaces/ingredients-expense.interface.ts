export interface IngredientsExpense {
    id: number,
    ingredientId: number,
    userId: string,
    baseInfoId: number,
    purchasingDate: Date,
    isExpired: boolean
}