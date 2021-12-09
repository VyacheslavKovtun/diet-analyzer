export interface Value {
    id: number,
    servings: number,
    title: string,
    image: string,
    imageType: string;
}

export interface SavingRecipe {
    date: number,
    slot: number,
    position: number,
    type: string,
    value: Value;
}